using System;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Generic vehicle class handles basic functionality for all vehicles
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class Vehicle : GameObject
    {

        /// <summary>
        /// Gets the Direction.
        /// </summary>
        /// <value>
        /// The Direction.
        /// </value>
        public VehicleDirection Direction { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle" /> class.
        /// </summary>
        /// <param name="direction">The direction of the vehicle's movement.</param>
        /// <param name="speed">The speed of the vehicle.</param>
        /// <exception cref="ArgumentOutOfRangeException">speed</exception>
        protected Vehicle(VehicleDirection direction, int speed)
        {
            if (speed <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }
            this.Direction = direction;
            this.SetSpeed(speed, 0);
        }

        /// <summary>
        /// Moves the vehicle based on its direction.
        /// </summary>
        public void MoveVehicle()
        {
            if (this.Direction == VehicleDirection.Right)
            {
                this.MoveRight();
            }
            else
            {
                this.MoveLeft();
            }
        }
    }
}
