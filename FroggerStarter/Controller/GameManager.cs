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
        private DispatcherTimer lifeTimer;
        private int timeLeft;
        private readonly RoadManager roadManager;
        private readonly PlayerManager playerManager;
        private readonly FrogHomeManager frogHomeManager;
        private readonly CollisionDetection collisionDetection;

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
            this.frogHomeManager = new FrogHomeManager();
            this.collisionDetection = new CollisionDetection();
            this.timeLeft = 20;
            this.setupGameTimer();
            this.setupLifeTimer();
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

        private void setupLifeTimer()
        {
            this.lifeTimer = new DispatcherTimer();
            this.lifeTimer.Tick += this.lifeTimerTick;
            this.lifeTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            this.lifeTimer.Start();
        }

        private void animationTimer()
        {

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
            this.createAndPlaceFrogHomes();
        }

        private void createAndPlaceFrogHomes()
        {
            this.addFrogHomesToManager(5);
            this.frogHomeManager.SetFrogHomes();
        }

        private void addFrogHomesToManager(int numberOfHomes)
        {
            for (var i = 0; i < numberOfHomes; i++)
            {
                var home = new FrogHome();
                this.frogHomeManager.AddFrogHome(home);
                this.gameCanvas.Children.Add(home.Sprite);
            }
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

        private void lifeTimerTick(object sender, object e)
        {
            this.decrementTimer();
        }

        private void handleGameOperations()
        {
            this.roadManager.MoveVehiclesInRoad();
            this.handleCarCollision();
            this.handleReachingHouse();
            GamePage.ScoreTextBlock.Text = "Score: " + this.playerManager.Score; //TODO oof
            GamePage.LivesTextBlock.Text = "Lives: " + this.playerManager.RemainingLives;
            GamePage.TimerTextBlock.Text = "Lives: " + this.timeLeft;
            this.roadManager.WrapRoad();
        }

        private void decrementTimer()
        {
            if (this.timeLeft == 0)
            {
                this.playerManager.DecrementLives();
                this.timeLeft = 20;
            }
            else
            {
                this.timeLeft--;
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
            if (!this.isPlayerOnTopBoundary() && !this.playerManager.IsGameOverConditionMet())
            {
                this.player.MoveUp();
            }
        }

        private bool isPlayerOnTopBoundary()
        {
            if (this.player.Y < this.player.Height * 3 && !this.isPlayerUnderneathFrogHome()) //TODO agic number
            {
                this.handlePlayerLosingLife();
                return true;
            }

            return false;
        }

        private bool isPlayerUnderneathFrogHome()
        {
            if (this.player.X % 150 == 0 || this.player.X == 0)
            {
                return true;
            }

            return false;
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

        private void handleCarCollision()
        {
            foreach (Lane lane in this.roadManager)
            {
                foreach (Vehicle vehicle in lane)
                {
                    if (this.collisionDetection.CheckForVehicleOnPlayerCollision(this.player, vehicle))
                    {
                        this.handlePlayerLosingLife();
                    }
                }
            }
        }

        private void handlePlayerLosingLife()
        {
            this.playerManager.DecrementLives();
            this.setPlayerToCenterOfBottomLane();
        }

        private void playDeathAnimation(Object sender, Object e)
        {

        }

        private void handleReachingHouse()
        {
            if (this.isPlayerOnAFrogHome())
            {
                this.playerManager.IncrementHousesOccupied();
                this.playerManager.IncrementScore(this.timeLeft);
                this.timeLeft = 20;
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private bool isPlayerOnAFrogHome()
        {
            foreach (FrogHome home in this.frogHomeManager)
            {
                if (this.collisionDetection.CheckForPlayerOnFrogHomeCollision(this.player, home)) //TODO magic number
                {
                    home.MarkFrogHomeOccupied();
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}