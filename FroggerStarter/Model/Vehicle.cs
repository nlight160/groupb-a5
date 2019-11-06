using System;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Generic vehicle class handles basic functionality for all vehicles
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class Vehicle : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets the Direction.
        /// </summary>
        /// <value>
        ///     The Direction.
        /// </value>
        public VehicleDirection Direction { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vehicle" /> class.
        ///     Precondition: speed &gt; 0
        ///     Postcondition: A vehicle objected is created
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
            SetSpeed(speed, 0);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the vehicle based on its direction.
        ///     Precondition: none
        ///     Postcondition: vehicle moves right if its direction is right, left otherwise
        /// </summary>
        public void MoveVehicle()
        {
            if (this.Direction == VehicleDirection.Right)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        #endregion
    }
}