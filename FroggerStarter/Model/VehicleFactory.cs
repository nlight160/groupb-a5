namespace FroggerStarter.Model
{

    /// <summary>
    /// Handles construction of vehicles
    /// </summary>
    public class VehicleFactory
    {

        /// <summary>
        /// Constructs the car that moves left.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <returns>
        /// A new car that moves left
        /// </returns>
        public Car ConstructCarThatMovesLeft(int speed)
        {
            return new Car(VehicleDirection.Left, speed);
        }


        /// <summary>
        /// Constructs the car that moves right.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <returns>
        /// A new car that moves right
        /// </returns>
        public Car ConstructCarThatMovesRight(int speed)
        {
            return new Car(VehicleDirection.Right, speed);
        }


        /// <summary>
        /// Constructs the truck that moves left.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <returns>
        /// A new truck that moves left
        /// </returns>
        public Truck ConstructTruckThatMovesLeft(int speed)
        {
            return new Truck(VehicleDirection.Left, speed);
        }

        /// <summary>
        /// Constructs the truck that moves right.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <returns>
        /// A new truck that moves right
        /// </returns>
        public Truck ConstructTruckThatMovesRight(int speed)
        {
            return new Truck(VehicleDirection.Right, speed);
        }
    }
}
