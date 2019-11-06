using Windows.UI.Xaml;
using FroggerStarter.View.Sprites.Vehicles;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Handles Cars and their functionality
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Car : Vehicle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Car" /> class.
        ///     Precondition: none
        ///     Postcondition: A new Car object is created
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public Car(VehicleDirection direction, int speed) :
            base(direction, speed)
        {
            this.initializeCarSprite();
        }

        #endregion

        #region Methods

        private void initializeCarSprite()
        {
            Sprite = new CarSprite
            {
                Visibility = Visibility.Collapsed
            };
        }

        #endregion
    }
}