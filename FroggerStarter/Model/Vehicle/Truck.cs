using Windows.UI.Xaml;
using FroggerStarter.View.Sprites.Vehicles;

namespace FroggerStarter.Model.v
{
    /// <summary>
    ///     Handles Trucks and their functionality
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class Truck : Vehicle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Truck" /> class.
        ///     Precondition: None
        ///     Postcondition: A Truck object is created
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public Truck(VehicleDirection direction, int speed) :
            base(direction, speed)
        {
            this.initializeTruckSprite();
        }

        #endregion

        #region Methods

        private void initializeTruckSprite()
        {
            Sprite = new TruckSprite
            {
                Visibility = Visibility.Collapsed
            };
        }

        #endregion
    }
}