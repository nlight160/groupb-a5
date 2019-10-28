using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Handles Trucks and their functionality
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Truck : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Truck"/> class.
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public Truck(VehicleDirection direction, int speed) :
            base(direction, speed)
        {
            this.initializeTruckSprite();
        }

        private void initializeTruckSprite()
        {
            Sprite = new TruckSprite();
        }
    }
}
