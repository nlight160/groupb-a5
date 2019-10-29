using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Stores and places vehicles in the lane
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class Lane : IEnumerable
    {
        #region Data members

        private readonly ICollection<Vehicle> vehicles;
        private readonly int laneWidth;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        ///     Precondition: lane width > 0
        ///     Postcondition: A lane object is created
        /// </summary>
        /// <param name="laneWidth">Width of the lane.</param>
        /// <exception cref="ArgumentOutOfRangeException">laneWidth</exception>
        public Lane(int laneWidth)
        {
            if (laneWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneWidth));
            }

            this.vehicles = new Collection<Vehicle>();
            this.laneWidth = laneWidth;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var vehicle in this.vehicles)
            {
                yield return vehicle;
            }
        }

        /// <summary>
        ///     Adds the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <exception cref="ArgumentNullException">vehicle</exception>
        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            this.vehicles.Add(vehicle);
        }

        /// <summary>
        ///     Wraps the lane.
        /// </summary>
        public void WrapLane()
        {
            foreach (var vehicle in this.vehicles)
            {
                this.setVehicleXtoBeginningOfTheLane(vehicle);
            }
        }

        private void setVehicleXtoBeginningOfTheLane(Vehicle vehicle)
        {
            if (vehicle.Direction == VehicleDirection.Left && vehicle.X < 0)
            {
                vehicle.X = this.laneWidth + vehicle.Width;
            }
            else if (vehicle.Direction == VehicleDirection.Right && vehicle.X > this.laneWidth)
            {
                vehicle.X = 0 - vehicle.Width;
            }
        }

        /// <summary>
        ///     Sets the vehicles to lane.
        ///     Precondition: laneY &gt; 0
        ///     Postcondition: vehicles are spaced apart in a lane at the same Y location
        /// </summary>
        /// <param name="laneY">The lane y.</param>
        /// <exception cref="ArgumentOutOfRangeException">laneY</exception>
        public void SetVehiclesToLane(int laneY)
        {
            if (laneY < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneY));
            }

            var nextX = 0;
            foreach (var vehicle in this.vehicles)
            {
                nextX += this.calculateDistanceBetweenVehicles();
                vehicle.X = nextX;
                vehicle.Y = laneY;
                this.setSpriteDirectionToVehicleDirection(vehicle);
            }
        }

        private void setSpriteDirectionToVehicleDirection(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            if (vehicle.Direction == VehicleDirection.Right)
            {
                vehicle.Sprite.ReverseSprite();
            }
        }

        private int calculateDistanceBetweenVehicles()
        {
            return this.laneWidth / this.vehicles.Count;
        }

        /// <summary>
        ///     Moves the vehicles in the lane.
        ///     Precondition: none
        ///     Postcondition: All vehicles in the lane move together
        /// </summary>
        public void MoveVehiclesInLane()
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.MoveVehicle();
            }
        }

        #endregion
    }
}