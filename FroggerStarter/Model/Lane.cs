using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Stores and places vehicles them in the lane
    /// </summary>
    /// <seealso cref="Vehicle" />
    public class Lane : IEnumerable
   {

       private readonly ICollection<Vehicle> vehicles;
       private readonly int laneWidth;

       /// <summary>
        /// Initializes a new instance of the <see cref="Lane"/> class.
        /// </summary>
        public Lane(int laneWidth)
        {
            this.vehicles = new Collection<Vehicle>();
            this.laneWidth = laneWidth;
        }

       

        /// <summary>
        /// Adds the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <exception cref="ArgumentNullException"></exception>
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
        /// Sets the vehicles to lane.
        /// </summary>
        /// <param name="laneY">The lane y.</param>
        public void SetVehiclesToLane(int laneY)
        {
            var vehicleGap = this.calculateDistanceBetweenVehicles();
            var nextX = 0;
            foreach (var vehicle in this.vehicles)
            {
                nextX += vehicleGap;
                vehicle.X = nextX;
                vehicle.Y = laneY;
                this.setSpriteDirectionToVehicleDirection(vehicle);
            }
        }

        private void setSpriteDirectionToVehicleDirection(Vehicle vehicle)
        {
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
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var vehicle in this.vehicles)
            {
                yield return vehicle;
            }
        }

        /// <summary>
        /// Moves the vehicles in the lane.
        /// </summary>
        public void MoveVehiclesInLane()
        {
            foreach (var vehicle in this.vehicles)
            {
                vehicle.MoveVehicle();
            }
        }
    }
}
