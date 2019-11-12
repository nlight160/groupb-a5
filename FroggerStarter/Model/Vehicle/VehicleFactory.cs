using System;

namespace FroggerStarter.Model.v
{
    /// <summary>
    ///     Handles construction of vehicles
    /// </summary>
    public class VehicleFactory
    {
        #region Methods

        /// <summary>
        ///     Constructs the car that moves left.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new car that moves left
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public Car ConstructCarThatMovesLeft(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new Car(VehicleDirection.Left, vehicleSpeed);
        }

        /// <summary>
        ///     Constructs the car that moves right.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new car that moves right
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public Car ConstructCarThatMovesRight(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new Car(VehicleDirection.Right, vehicleSpeed);
        }

        /// <summary>
        ///     Constructs the truck that moves left.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new truck that moves left
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public Truck ConstructTruckThatMovesLeft(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new Truck(VehicleDirection.Left, vehicleSpeed);
        }

        /// <summary>
        ///     Constructs the truck that moves right.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new truck that moves right
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public Truck ConstructTruckThatMovesRight(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new Truck(VehicleDirection.Right, vehicleSpeed);
        }

        /// <summary>
        ///     Constructs the super car that moves left.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new super car that moves left
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public SuperCar ConstructSuperCarThatMovesLeft(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new SuperCar(VehicleDirection.Left, vehicleSpeed);
        }

        /// <summary>
        ///     Constructs the super car that moves right.
        ///     Precondition: speed &gt; 0
        /// </summary>
        /// <param name="vehicleSpeed">The vehicle speed.</param>
        /// <returns>
        ///     A new super car that moves right
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">vehicleSpeed</exception>
        public SuperCar ConstructSuperCarThatMovesRight(int vehicleSpeed)
        {
            if (vehicleSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleSpeed));
            }

            return new SuperCar(VehicleDirection.Right, vehicleSpeed);
        }

        #endregion
    }
}