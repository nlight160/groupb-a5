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

        private const int BonusTime = 10;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusTimePowerUp" /> class.
        /// </summary>
        public BonusTimePowerUp()
        {
            this.initializeBonusTimePowerUpSprite();
        }

        #endregion

        #region Methods

        private void initializeBonusTimePowerUpSprite()
        {
            Sprite = new BonusTimeSprite();
        }

        /// <summary>
        ///     Gets the bonus time.
        /// </summary>
        /// <returns> the bonus time between 1 and 10 inclusive </returns>
        public int GetBonusTime()
        {
            return BonusTime;
        }

        #endregion
    }
}