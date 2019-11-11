using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using FroggerStarter.Model;
using FroggerStarter.View;
using FroggerStarter.View.ContentDialogs;
using FroggerStarter.View.Sprites;
using FroggerStarter.View.Sprites.Vehicles;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        private const int SecondRound = 2;

        #region Data members

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

        /// <summary>
        ///     The update lives
        /// </summary>
        public EventHandler<UpdateLevelEventArg> UpdateLevel;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private Canvas gameCanvas;
        private Frog player;
        private DispatcherTimer timer;
        private DispatcherTimer lifeTimer;
        private DispatcherTimer vehicleTimer;

        private int timeLeft;
        private int vehicleIndex;

        private readonly RoadManager roadManager;
        private readonly PlayerManager playerManager;
        private readonly FrogHomeManager frogHomeManager;
        private readonly CollisionDetection collisionDetection;
        private readonly AnimationManager deathAnimation;
        private readonly AnimationFrame playerMovingFrame;
        private readonly SoundManager soundManager;
        private readonly PowerUpManager powerUpManager;
        private readonly ScoreBoardManager scoreBoard;
        private readonly GameSettings gameSettings;
        private readonly HighScoreContentDialog highScoreContentDialog;
        private readonly AddScoreContentDialog addScoreContentDialog;

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
            this.soundManager = new SoundManager();
            this.gameSettings = new GameSettings();
            this.roadManager = new RoadManager((int) this.backgroundWidth, (int) this.backgroundHeight,
                this.gameSettings.LaneHeight);
            this.playerManager = new PlayerManager();
            this.scoreBoard = new ScoreBoardManager();
            this.frogHomeManager = new FrogHomeManager(this.gameSettings.FrogHomeOffset);
            this.collisionDetection = new CollisionDetection();
            this.deathAnimation = new AnimationManager();
            this.powerUpManager =
                new PowerUpManager(this.soundManager, (int) this.backgroundWidth, (int) this.backgroundHeight);
            this.highScoreContentDialog = new HighScoreContentDialog();
            this.addScoreContentDialog = new AddScoreContentDialog();
            this.timeLeft = this.gameSettings.InitialTimeLeft;
            this.vehicleIndex = 0;
            this.playerMovingFrame = new AnimationFrame(new FrogMovingSprite());

            this.setupGameTimer();
            this.setupLifeTimer();
            this.setupVehicleTimer();
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

        private void setupVehicleTimer()
        {
            this.vehicleTimer = new DispatcherTimer();
            this.vehicleTimer.Tick += this.vehicleTimerTick;
            this.vehicleTimer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            this.vehicleTimer.Start();
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
            this.createAndPlacePowerUps();
        }

        private void createAndPlacePowerUps()
        {
            this.gameCanvas.Children.Add(this.powerUpManager.TimePowerUp.Sprite);
            this.gameCanvas.Children.Add(this.powerUpManager.InvincibilityPowerUp.Sprite);
        }

        private void createAndPlaceFrogHomes()
        {
            this.addFrogHomesToManager(this.gameSettings.NumberOfHomes);
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
            foreach (var vehicle in this.roadManager)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }

            this.makeVehicleVisible();
        }

        private void deleteCurrentVehicles()
        {
            var count = 0;
            var childrenList = this.gameCanvas.Children.ToArray();
            foreach (var item in childrenList)
            {
                if (item is CarSprite || item is TruckSprite || item is SuperCarSprite)
                {
                    this.gameCanvas.Children[count].Visibility = Visibility.Collapsed;
                    this.gameCanvas.Children.Remove(item);
                    count = 0;
                }

                count++;
            }
        }

        private void makeVehicleVisible()
        {
            if (this.vehicleIndex < this.roadManager.Count())
            {
                this.roadManager.ElementAt(this.vehicleIndex).Sprite.Visibility = Visibility.Visible;
                this.vehicleIndex++;
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

            this.gameCanvas.Children.Add(this.playerMovingFrame.Sprite);
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - this.gameSettings.BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            if (!this.playerManager.IsGameOverConditionMet() && !this.playerManager.IsRoundChanging())
            {
                this.handleGameOperations();
            }
            else if (this.playerManager.IsRoundChanging() && !this.roadManager.IsLastRound())
            {
                this.handleLevelChange();
            }
            else
            {
                this.setGameOverScreen();
                this.soundManager.PlayGameOverSound();
                this.timer.Stop();
            }
        }

        private void handleLevelChange()
        {
            this.soundManager.PlayCompleteLevelSound();
            this.deleteCurrentVehicles();
            this.hideFrogHomes();
            this.setPlayerToCenterOfBottomLane();
            this.roadManager.RoundCount++;
            this.createAndPlaceVehicles();
            this.updateLevel();
            this.playerManager.NextLevel();
            this.changeCanvasTheme();
        }

        private async void setGameOverScreen()
        {
            var isGameOver = new GameOverEventArg {GameOver = true};
            this.soundManager.PlayGameOverSound();
            this.GameOver?.Invoke(this, isGameOver);

            await this.handleAddScoreDialog();
        }

        private async Task handleAddScoreDialog()
        {
            await this.addScoreContentDialog.ShowAsync();
            if (this.addScoreContentDialog.IsPrimary)
            {
                var name = this.addScoreContentDialog.PlayerName;

                var score = new Score
                {
                    Name = name,
                    Value = this.playerManager.Score,
                    Level = this.playerManager.Level
                };

                this.scoreBoard.AddNewScore(score);
            }
        }

        private void lifeTimerTick(object sender, object e)
        {
            if (!this.playerManager.IsGameOverConditionMet() && !this.deathAnimation.IsDeathAnimationRunning())
            {
                this.decrementTimer();
            }
        }

        private void vehicleTimerTick(object sender, object e)
        {
            this.makeVehicleVisible();
        }

        private void handleGameOperations()
        {
            this.roadManager.MoveVehiclesInRoad();
            this.handleAllCarsCollisions();
            this.handleReachingHouse();
            this.handleBonusTimePowerUp();
            this.handleInvincibilityPowerUp();
            this.updateScore();
            this.updateLives();
            this.updateTimer();
            this.roadManager.WrapRoad();
            this.playerMovingFrame.SetFrameLocation(this.player.X, this.player.Y);
            this.handlePlayerVisibility();
        }

        private void handleBonusTimePowerUp()
        {
            if (this.collisionDetection.CheckForPlayerOnPowerUpCollision(this.player,
                    this.powerUpManager.TimePowerUp) &&
                this.powerUpManager.TimePowerUp.Sprite.Visibility == Visibility.Visible)
            {
                this.soundManager.PlayPowerUpSound();
                this.timeLeft += this.powerUpManager.TimePowerUp.GetBonusTime();
                this.powerUpManager.TimePowerUp.Sprite.Visibility = Visibility.Collapsed;
                this.powerUpManager.StartBonusTimePowerUpTimer();
            }
        }

        private void handleInvincibilityPowerUp()
        {
            if (this.collisionDetection.CheckForPlayerOnPowerUpCollision(this.player,
                    this.powerUpManager.InvincibilityPowerUp) &&
                this.powerUpManager.InvincibilityPowerUp.Sprite.Visibility == Visibility.Visible)
            {
                this.soundManager.PlayPowerUpSound();
                this.powerUpManager.InvincibilityPowerUp.Sprite.Visibility = Visibility.Collapsed;
                this.powerUpManager.StartInvincibilityPowerUpTimer();
            }
        }

        private void changeCanvasTheme()
        {
            if (this.roadManager.RoundCount == SecondRound)
            {
                this.gameCanvas.Background = new SolidColorBrush(Colors.Bisque);
            }
            else
            {
                this.gameCanvas.Background = new SolidColorBrush(Colors.Aqua);
            }
        }

        private void handlePlayerVisibility()
        {
            var x = this.backgroundWidth / 2 - this.player.Width / 2;
            var y = this.backgroundHeight - this.player.Height - this.gameSettings.BottomLaneOffset;
            if (this.deathAnimation.IsDeathAnimationRunning())
            {
                this.player.Sprite.Visibility = Visibility.Collapsed;
                this.playerMovingFrame.Sprite.Visibility = Visibility.Collapsed;
            }
            else if (Math.Abs(this.player.X - x) <= 0 && Math.Abs(this.player.Y - y) <= 0)
            {
                this.player.Sprite.Visibility = Visibility.Visible;
            }
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

        private void updateLevel()
        {
            var updateLevel = new UpdateLevelEventArg {Level = this.playerManager.Level};
            this.UpdateLevel?.Invoke(this, updateLevel);
        }

        private void decrementTimer()
        {
            if (this.timeLeft == 0)
            {
                this.playerManager.DecrementLives();
                this.soundManager.PlayTimeOutSound();
                this.handleDeathAnimation();
                this.timeLeft = this.gameSettings.InitialTimeLeft;
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
            if (this.canPlayerMove())
            {
                this.player.Sprite.RotateSpriteToFaceLeft();
                this.playerMovingFrame.Sprite.RotateSpriteToFaceLeft();
                this.animatePlayerMovement();
                this.player.MoveLeft();
            }
            else if (this.isPlayerAdjacentToLeftBoundary() && !this.playerManager.IsGameOverConditionMet()
                                                           && !this.deathAnimation.IsDeathAnimationRunning())
            {
                this.soundManager.PlayWallCollisionSound();
            }
        }

        private bool canPlayerMove()
        {
            return !this.isPlayerAdjacentToLeftBoundary() && !this.playerManager.IsGameOverConditionMet()
                                                          && !this.deathAnimation.IsDeathAnimationRunning();
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
            if (this.canPlayerMove())
            {
                this.player.Sprite.RotateSpriteToFaceRight();
                this.playerMovingFrame.Sprite.RotateSpriteToFaceRight();
                this.animatePlayerMovement();
                this.player.MoveRight();
            }
            else if (this.isPlayerAdjacentToRightBoundary() && !this.playerManager.IsGameOverConditionMet() &&
                     !this.deathAnimation.IsDeathAnimationRunning())
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
            if (this.canPlayerMove())
            {
                this.player.Sprite.RotateSpriteToFaceUp();
                this.playerMovingFrame.Sprite.RotateSpriteToFaceUp();
                this.animatePlayerMovement();
                this.player.MoveUp();
            }
        }

        private bool isPlayerOnTopBoundary()
        {
            if (this.player.Y < this.player.Height * this.gameSettings.TopBoundaryModifier &&
                !this.isPlayerUnderneathFrogHome())
            {
                this.handlePlayerLosingLife();
                return true;
            }

            return false;
        }

        private bool isPlayerUnderneathFrogHome()
        {
            return Math.Abs(this.player.X % this.gameSettings.FrogHomeOffset) <= 0 || Math.Abs(this.player.X) <= 0;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (this.canPlayerMove())
            {
                this.player.Sprite.RotateSpriteToFaceDown();
                this.playerMovingFrame.Sprite.RotateSpriteToFaceDown();
                this.animatePlayerMovement();
                this.player.MoveDown();
            }
        }

        private bool isPlayerAdjacentToBottomBoundary()
        {
            return this.player.Y >= this.backgroundHeight - this.player.Height * 2;
        }

        private void handleAllCarsCollisions()
        {
            foreach (var vehicle in this.roadManager)
            {
                this.handleSingleCarCollision(vehicle);
            }
        }

        private void handleSingleCarCollision(Vehicle vehicle)
        {
            if (this.collisionDetection.CheckForVehicleOnPlayerCollision(this.player, vehicle) &&
                !this.powerUpManager.IsInvincibilityActive())
            {
                this.handlePlayerLosingLife();
            }
        }

        private void handlePlayerLosingLife()
        {
            this.playerManager.DecrementLives();
            this.soundManager.PlayVehicleCollisionSound();
            this.handleDeathAnimation();
            this.timeLeft = this.gameSettings.InitialTimeLeft;
        }

        private void handleDeathAnimation()
        {
            this.deathAnimation.StartDeathAnimationTimer();
            this.deathAnimation.PlayNextFrame(0);
            this.setAnimationFramesToPlayerLocation();
            this.setPlayerToCenterOfBottomLane();
        }

        private void setAnimationFramesToPlayerLocation()
        {
            foreach (AnimationFrame frame in this.deathAnimation)
            {
                frame.SetFrameLocation(this.player.X, this.player.Y);
            }
        }

        private void handleReachingHouse()
        {
            if (this.isPlayerOnAFrogHome())
            {
                this.playerManager.IncrementScore(this.timeLeft);
                this.soundManager.PlayHomeSound();
                this.timeLeft = this.gameSettings.InitialTimeLeft;
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private void hideFrogHomes()
        {
            foreach (FrogHome home in this.frogHomeManager)
            {
                home.MarkFrogHomeUnOccupied();
            }
        }

        private bool isPlayerOnAFrogHome()
        {
            foreach (FrogHome home in this.frogHomeManager)
            {
                if (this.collisionDetection.CheckForPlayerOnFrogHomeCollision(this.player, home))
                {
                    this.handleFrogHomeOccupation(home);
                    return true;
                }
            }

            return false;
        }

        private void handleFrogHomeOccupation(FrogHome home)
        {
            if (!home.IsFrogHomeOccupied())
            {
                home.MarkFrogHomeOccupied();
                this.playerManager.IncrementHousesOccupied();
            }
            else
            {
                this.handlePlayerLosingLife();
            }
        }

        private void animatePlayerMovement()
        {
            if (this.player.Sprite.Visibility == Visibility.Visible)
            {
                this.player.Sprite.Visibility = Visibility.Collapsed;
                this.playerMovingFrame.Sprite.Visibility = Visibility.Visible;
            }
            else
            {
                this.playerMovingFrame.Sprite.Visibility = Visibility.Collapsed;
                this.player.Sprite.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}