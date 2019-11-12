using Windows.UI.Xaml;
using FroggerStarter.View.Sprites.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Initializes Super car sprite and handles functionality
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class SuperCar : Vehicle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuperCar" /> class.
        ///     Precondition: None
        ///     Postcondition: A SuperCar object is created
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public SuperCar(VehicleDirection direction, int speed) :
            base(direction, speed)
        {
            this.initializeSuperCarSprite();
        }

        #endregion

        #region Methods

        private void initializeSuperCarSprite()
        {
            Sprite = new SuperCarSprite {
                Visibility = Visibility.Collapsed
            };
        }

        #endregion
    }
}