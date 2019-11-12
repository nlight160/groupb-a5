using System;
using Windows.UI.Xaml;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// </summary>
    public class PowerUpManager
    {
        #region Data members

        private const int MinBonusTimeRespawn = 1;
        private const int MaxBonusTimeRespawn = 21;
        private const int InvincibilityDuration = 10;
        private const int MinSpawnY = 150;
        private const int EdgeOfPlayArea = 50;
        private const int RoadOffset = 5;
        private const int MinSpawnX = 0;

        private readonly Random random;
        private DispatcherTimer respawnTimer;
        private int totalTime;
        private int timeUntilNextPowerUp;
        private readonly int backgroundWidth;
        private readonly int backgroundHeight;
        private DispatcherTimer invincibilityTimer;
        private readonly SoundManager soundManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the time power up.
        /// </summary>
        /// <value>
        ///     The time power up.
        /// </value>
        public BonusTimePowerUp TimePowerUp { get; }

        /// <summary>
        ///     Gets the invincibility power up.
        /// </summary>
        /// <value>
        ///     The invincibility power up.
        /// </value>
        public InvincibilityPowerUp InvincibilityPowerUp { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PowerUpManager" /> class.
        /// </summary>
        /// <param name="soundManager">The sound manager.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="backgroundHeight">Height of the background.</param>
        public PowerUpManager(SoundManager soundManager, int backgroundWidth, int backgroundHeight)
        {
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
            this.TimePowerUp = new BonusTimePowerUp();
            this.InvincibilityPowerUp = new InvincibilityPowerUp();

            this.random = new Random();
            this.resetBonusTimePowerUp();
            this.totalTime = 0;
            this.timeUntilNextPowerUp = this.calculateTimeUntilNextPowerUp();
            this.setupBonusTimeTimer();
            this.soundManager = soundManager;
            this.setupInvincibilityTimer();
        }

        #endregion

        #region Methods

        private void setupBonusTimeTimer()
        {
            this.respawnTimer = new DispatcherTimer();
            this.respawnTimer.Tick += this.respawnBonusTimeTick;
            this.respawnTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
        }

        private void respawnBonusTimeTick(object sender, object e)
        {
            if (this.totalTime == this.timeUntilNextPowerUp)
            {
                this.resetBonusTimePowerUp();
                this.respawnTimer.Stop();
            }

            this.totalTime++;
        }

        private int calculateTimeUntilNextPowerUp()
        {
            return this.random.Next(this.totalTime + MinBonusTimeRespawn, this.totalTime + MaxBonusTimeRespawn);
        }

        /// <summary>
        ///     Starts the bonus time power up timer.
        ///     Precondition: none
        ///     Postcondition: BonusTime power up timer starts
        /// </summary>
        public void StartBonusTimePowerUpTimer()
        {
            this.respawnTimer.Start();
            this.timeUntilNextPowerUp = this.calculateTimeUntilNextPowerUp();
        }

        private void resetBonusTimePowerUp()
        {
            this.TimePowerUp.X =
                this.roundDownToNearestMultipleOfFifty(
                    this.random.Next(MinSpawnX, this.backgroundWidth - EdgeOfPlayArea));

            this.TimePowerUp.Y =
                this.roundDownToNearestMultipleOfFifty(
                    this.random.Next(MinSpawnY, this.backgroundHeight - EdgeOfPlayArea)) + RoadOffset;

            this.TimePowerUp.Sprite.Visibility = Visibility.Visible;
        }

        private int roundDownToNearestMultipleOfFifty(int value)
        {
            var remainder = value % 50;
            return value - remainder;
        }

        private void setupInvincibilityTimer()
        {
            this.invincibilityTimer = new DispatcherTimer();
            this.invincibilityTimer.Tick += this.invincibilityTimerTick;
            this.invincibilityTimer.Interval = new TimeSpan(0, 0, 0, InvincibilityDuration, 0);
        }

        private void invincibilityTimerTick(object sender, object e)
        {
            this.invincibilityTimer.Stop();
            this.soundManager.PlayPowerDownSound();
        }

        /// <summary>
        ///     Starts the bonus time power up timer.
        ///     Precondition: none
        ///     Postcondition: timer starts
        /// </summary>
        public void StartInvincibilityPowerUpTimer()
        {
            this.invincibilityTimer.Start();
        }

        /// <summary>
        ///     Determines whether [is invincibility active].
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is invincibility active]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInvincibilityActive()
        {
            return this.invincibilityTimer.IsEnabled;
        }

        #endregion
    }
}