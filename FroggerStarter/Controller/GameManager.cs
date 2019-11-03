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
        private const int FrogHomeOffset = 150;
        private const int TopBoundaryY = 3;
        private const int NumberOfHomes = 5;
        private const int InitialTimeLeft = 20;

        /// <summary>
        ///     The game over
        /// </summary>
        public EventHandler<GameOverEventArg> GameOver;

        /// <summary>
        ///     The update score
        /// </summary>
        public EventHandler<UpdateScoreEventArg> UpdateScore;

        /// <summary>
        ///     The update timer
        /// </summary>
        public EventHandler<UpdateTimerEventArg> UpdateTimer;

        /// <summary>
        ///     The update lives
        /// </summary>
        public EventHandler<UpdateLivesEventArgs> UpdateLives;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private Canvas gameCanvas;
        private Frog player;
        private DispatcherTimer timer;
        private DispatcherTimer lifeTimer;
        private DispatcherTimer animationTimer;

        private int timeLeft;
        private int animationIndex;

        private readonly RoadManager roadManager;
        private readonly PlayerManager playerManager;
        private readonly FrogHomeManager frogHomeManager;
        private readonly CollisionDetection collisionDetection;
        private readonly AnimationManager deathAnimation;
        private readonly SoundManager soundManager;

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
            this.frogHomeManager = new FrogHomeManager(FrogHomeOffset);
            this.collisionDetection = new CollisionDetection();
            this.deathAnimation = new AnimationManager();
            this.soundManager = new SoundManager();

            this.timeLeft = InitialTimeLeft;
            this.animationIndex = 0;

            this.setupGameTimer();
            this.setupLifeTimer();
            this.setupAnimationTimer();
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

        private void setupAnimationTimer()
        {
            this.animationTimer = new DispatcherTimer();
            this.animationTimer.Tick += this.animationTimerTick;
            this.animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 650);
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
            this.createAnimationSprites();
        }

        private void createAndPlaceFrogHomes()
        {
            this.addFrogHomesToManager(NumberOfHomes);
            this.frogHomeManager.PlaceFrogHomes();
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

        private void createAnimationSprites()
        {
            foreach (AnimationFrame frame in this.deathAnimation)
            {
                this.gameCanvas.Children.Add(frame.Sprite);
            }
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
                this.setGameOverScreen();
                this.soundManager.PlayGameOverSound();
                this.timer.Stop();
            }
        }

        private void setGameOverScreen()
        {
            var isGameOver = new GameOverEventArg {GameOver = true};
            this.soundManager.PlayGameOverSound();
            this.GameOver?.Invoke(this, isGameOver);
        }

        private void lifeTimerTick(object sender, object e)
        {
            if (!this.playerManager.IsGameOverConditionMet() && !this.isDeathAnimationRunning())
            {
                this.decrementTimer();
            }
        }

        private bool isDeathAnimationRunning()
        {
            return this.animationTimer.IsEnabled;
        }

        private void animationTimerTick(object sender, object e)
        {
            if (this.animationIndex > 0)
            {
                this.deathAnimation.HideCurrentFrame(this.animationIndex - 1);
            }

            if (this.animationIndex == 4)
            {
                this.handleStopAnimation();
            }
            else
            {
                this.deathAnimation.PlayNextFrame(this.animationIndex);
                this.animationIndex++;
            }
        }

        private void handleStopAnimation()
        {
            this.animationTimer.Stop();
            if (!this.playerManager.IsGameOverConditionMet())
            {
                this.player.Sprite.Visibility = Visibility.Visible;
            }

            this.animationIndex = 0;
        }

        private void setAnimationFramesToPlayerLocation()
        {
            foreach (AnimationFrame frame in this.deathAnimation)
            {
                frame.SetFrameLocation(this.player.X, this.player.Y);
            }
        }

        private void handleGameOperations()
        {
            this.roadManager.MoveVehiclesInRoad();
            this.handleAllCarCollisions();
            this.handleReachingHouse();
            this.updateScore();
            this.updateLives();
            this.updateTimer();
            this.roadManager.WrapRoad();
        }

        private void updateScore()
        {
            var updateScore = new UpdateScoreEventArg {Score = this.playerManager.Score};
            this.UpdateScore?.Invoke(this, updateScore);
        }

        private void updateTimer()
        {
            var updateTimer = new UpdateTimerEventArg {LifeTimer = this.timeLeft};
            this.UpdateTimer?.Invoke(this, updateTimer);
        }

        private void updateLives()
        {
            var updateLives = new UpdateLivesEventArgs {RemainingLives = this.playerManager.RemainingLives};
            this.UpdateLives?.Invoke(this, updateLives);
        }

        private void decrementTimer()
        {
            if (this.timeLeft == 0)
            {
                this.playerManager.DecrementLives();
                this.soundManager.PlayTimeOutSound();
                this.handleStartAnimation();
                this.timeLeft = InitialTimeLeft;
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
                                                       && !this.isDeathAnimationRunning())
            {
                this.player.MoveLeft();
            }
            else if (this.isPlayerAdjacentToLeftBoundary() && !this.playerManager.IsGameOverConditionMet()
                                                           && !this.isDeathAnimationRunning())
            {
                this.soundManager.PlayWallCollisionSound();
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
            if (!this.isPlayerAdjacentToRightBoundary() && !this.playerManager.IsGameOverConditionMet() &&
                !this.isDeathAnimationRunning())
            {
                this.player.MoveRight();
            }
            else if (this.isPlayerAdjacentToRightBoundary() && !this.playerManager.IsGameOverConditionMet() &&
                     !this.isDeathAnimationRunning())
            {
                this.soundManager.PlayWallCollisionSound();
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
            if (!this.isPlayerOnTopBoundary() && !this.playerManager.IsGameOverConditionMet() &&
                !this.isDeathAnimationRunning())
            {
                this.player.MoveUp();
            }
        }

        private bool isPlayerOnTopBoundary()
        {
            if (this.player.Y < this.player.Height * TopBoundaryY && !this.isPlayerUnderneathFrogHome())
            {
                this.handlePlayerLosingLife();
                return true;
            }

            return false;
        }

        private void handlePlayerOnOccupiedFrogHome()
        {
            foreach (FrogHome frogHome in this.frogHomeManager)
            {
                if (frogHome.IsFrogHomeOccupied() &&
                    this.collisionDetection.CheckForPlayerOnFrogHomeCollision(this.player, frogHome))
                {
                    this.handlePlayerLosingLife();
                }
            }
        }

        private bool isPlayerUnderneathFrogHome()
        {
            return Math.Abs(this.player.X % FrogHomeOffset) <= 0 || Math.Abs(this.player.X) <= 0;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!this.isPlayerAdjacentToBottomBoundary() && !this.playerManager.IsGameOverConditionMet() &&
                !this.isDeathAnimationRunning())
            {
                this.player.MoveDown();
            }
        }

        private bool isPlayerAdjacentToBottomBoundary()
        {
            return this.player.Y >= this.backgroundHeight - this.player.Height * 2;
        }

        private void handleAllCarCollisions()
        {
            foreach (Lane lane in this.roadManager)
            {
                foreach (Vehicle vehicle in lane)
                {
                    this.handleSingleCarCollision(vehicle);
                }
            }
        }

        private void handleSingleCarCollision(Vehicle vehicle)
        {
            if (this.collisionDetection.CheckForVehicleOnPlayerCollision(this.player, vehicle))
            {
                this.handlePlayerLosingLife();
            }
        }

        private void handlePlayerLosingLife()
        {
            this.playerManager.DecrementLives();
            this.soundManager.PlayVehicleCollisionSound();
            this.handleStartAnimation();
            this.timeLeft = InitialTimeLeft;
        }

        private void handleStartAnimation()
        {
            this.animationTimer.Start();
            this.deathAnimation.PlayNextFrame(0);
            this.setAnimationFramesToPlayerLocation();
            this.player.Sprite.Visibility = Visibility.Collapsed;
            this.setPlayerToCenterOfBottomLane();
        }

        private void handleReachingHouse()
        {
            if (this.isPlayerOnAFrogHome())
            {
                this.playerManager.IncrementHousesOccupied();
                this.playerManager.IncrementScore(this.timeLeft);
                this.soundManager.PlayHomeSound();
                this.timeLeft = InitialTimeLeft;
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private bool isPlayerOnAFrogHome()
        {
            foreach (FrogHome home in this.frogHomeManager)
            {
                if (this.collisionDetection.CheckForPlayerOnFrogHomeCollision(this.player, home))
                {
                    this.handlePlayerOnOccupiedFrogHome();
                    home.MarkFrogHomeOccupied();
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}