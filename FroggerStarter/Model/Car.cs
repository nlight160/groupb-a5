using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Handles Cars and their functionality
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Car : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="direction">The Direction.</param>
        /// <param name="speed">The speed.</param>
        public Car(VehicleDirection direction, int speed) : 
            base(direction, speed)
        {
          this.initializeCarSprite();
        }

        private void initializeCarSprite()
        {
            Sprite = new CarSprite();
        }
    }
}
