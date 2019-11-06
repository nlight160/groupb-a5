using System;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Handles creation and functionality of bonus time power ups
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.StationaryObject" />
    public class BonusTimePowerUp : StationaryObject
    {
        #region Data members

        private readonly Random random;
        private DispatcherTimer respawnTimer;
        private int totalTime;
        private int timeUntilNextPowerUp;
        private readonly int backgroundWidth;
        private readonly int backgroundHeight;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusTimePowerUp" /> class.
        /// </summary>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// TODO magic numbers present in class
        /// <param name="backgroundHeight">Height of the background.</param>
        public BonusTimePowerUp(int backgroundWidth, int backgroundHeight)
        {
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
            this.initializeBonusTimePowerUpSprite();
            this.random = new Random();
            this.resetBonusTimePowerUp();
            this.totalTime = 0;
            this.timeUntilNextPowerUp = this.calculateTimeUntilNextPowerUp();
            this.setupRespawnTimer();
        }

        #endregion

        #region Methods

        private void initializeBonusTimePowerUpSprite()
        {
            Sprite = new BonusTimeSprite();
        }

        private void setupRespawnTimer()
        {
            this.respawnTimer = new DispatcherTimer();
            this.respawnTimer.Tick += this.respawnTimerTick;
            this.respawnTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
        }

        private void respawnTimerTick(object sender, object e)
        {
            if (this.totalTime == this.timeUntilNextPowerUp)
            {
                this.resetBonusTimePowerUp();
                this.respawnTimer.Stop();
            }

            this.totalTime++;
        }

        /// <summary>
        ///     Gets the bonus time.
        /// </summary>
        /// <returns> the bonus time between 1 and 10 inclusive </returns>
        public int GetBonusTime()
        {
            return this.random.Next(1, 11);
        }

        private int calculateTimeUntilNextPowerUp()
        {
            return this.random.Next(this.totalTime + 1, this.totalTime + 21);
        }

        /// <summary>
        ///     Starts the bonus time power up timer.
        /// </summary>
        public void StartBonusTimePowerUpTimer()
        {
            this.respawnTimer.Start();
            this.timeUntilNextPowerUp = this.calculateTimeUntilNextPowerUp();
        }

        private void resetBonusTimePowerUp()
        {
            X = this.roundDownToNearestMultipleOfFifty(this.random.Next(0, this.backgroundWidth - 50));
            Y = this.roundDownToNearestMultipleOfFifty(this.random.Next(150, this.backgroundHeight - 50)) + 5;
            Sprite.Visibility = Visibility.Visible;
        }

        private int roundDownToNearestMultipleOfFifty(int value)
        {
            var remainder = value % 50;
            return value - remainder;
        }

        #endregion
    }
}