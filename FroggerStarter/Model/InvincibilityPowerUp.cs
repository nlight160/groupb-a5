using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.StationaryObject" />
    public class InvincibilityPowerUp : StationaryObject
    {
        #region Data members

        private const int CenterX = 300;
        private const int CenterY = 205;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvincibilityPowerUp" /> class.
        /// </summary>
        public InvincibilityPowerUp()
        {
            this.initializeInvincibilityPowerUpSprite();
        }

        #endregion

        #region Methods

        private void initializeInvincibilityPowerUpSprite()
        {
            Sprite = new InvincibilitySprite();
            X = CenterX;
            Y = CenterY;
        }

        #endregion
    }
}