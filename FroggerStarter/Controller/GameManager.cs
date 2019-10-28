using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;
using FroggerStarter.View;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int BottomLaneOffset = 5;
        private const int LaneHeight = 50;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Frog player;
        private DispatcherTimer timer;
        private readonly RoadManager roadManager;
        private readonly PlayerManager playerManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     backgroundHeight &lt;= 0
        ///     or
        ///     backgroundWidth &lt;= 0
        /// </exception>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.roadManager = new RoadManager((int) this.backgroundWidth, (int) this.backgroundHeight, LaneHeight);
            this.playerManager = new PlayerManager();
            this.setupGameTimer();
        }

        #endregion

        #region Methods

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        /// <summary>
        ///     Initializes the game working with appropriate classes to play frog
        ///     and vehicle on game screen.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage"></param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));
            this.createAndPlacePlayer();
            this.createAndPlaceVehicles();
        }

        private void createAndPlaceVehicles()
        {
            this.roadManager.ConstructRoad();
            foreach (Lane lane in this.roadManager)
            {
                foreach (Vehicle vehicle in lane)
                {
                    this.gameCanvas.Children.Add(vehicle.Sprite);
                }
            }
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            if (!this.playerManager.IsGameOverConditionMet())
            {
                this.handleGameOperations();
            }
            else
            {
                GamePage.GameOverTextBlock.Visibility = Visibility.Visible; //TODO oof
            }
        }

        private void handleGameOperations()
        {
            this.roadManager.MoveVehiclesInRoad();
            this.checkForCollision();
            this.handleWin();
            GamePage.ScoreTextBlock.Text = $"Score: " + this.playerManager.Score; //TODO oof
            GamePage.LivesTextBlock.Text = $"Lives: " + this.playerManager.RemainingLives; 
            this.roadManager.WrapRoad();
        }

        private void handleWin()
        {
            if (this.isPlayerAdjacentToTopBoundary())
            {
                this.playerManager.IncrementScore();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: none
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            if (!this.isPlayerAdjacentToLeftBoundary() && !this.playerManager.IsGameOverConditionMet()
            ) 
            {
                this.player.MoveLeft();
            }
        }

        private bool isPlayerAdjacentToLeftBoundary()
        {
            return this.player.X < this.player.Width;
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            if (!this.isPlayerAdjacentToRightBoundary() && !this.playerManager.IsGameOverConditionMet())
            {
                this.player.MoveRight();
            }
        }

        private bool isPlayerAdjacentToRightBoundary()
        {
            return this.player.X >= this.backgroundWidth - this.player.Width;
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (!this.isPlayerAdjacentToTopBoundary() && !this.playerManager.IsGameOverConditionMet())
            {
                this.player.MoveUp();
            }
        }

        private bool isPlayerAdjacentToTopBoundary()
        {
            return this.player.Y <= this.player.Height * 2;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!this.isPlayerAdjacentToBottomBoundary() && !this.playerManager.IsGameOverConditionMet())
            {
                this.player.MoveDown();
            }
        }

        private bool isPlayerAdjacentToBottomBoundary()
        {
            return this.player.Y >= this.backgroundHeight - this.player.Height * 2;
        }

        private void checkForCollision()
        {
            foreach (Lane lane in this.roadManager)
            {
                foreach (Vehicle vehicle in lane)
                {
                    if (this.player.BoundingBox.IntersectsWith(vehicle.BoundingBox))
                    {
                        this.handleCollision();
                    }
                }
            }
        } //TODO could still be in wrong place

        private void handleCollision()
        {
            this.playerManager.DecrementLives();
            this.setPlayerToCenterOfBottomLane();
        }

        #endregion
    }
}