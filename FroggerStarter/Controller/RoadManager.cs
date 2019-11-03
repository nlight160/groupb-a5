using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all of the lanes
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public class RoadManager : IEnumerable<Lane>
    {
        #region Data members

        private const int LaneOffsetFromBottom = 55;
        private readonly IList<Lane> lanes;
        private readonly int backgroundWidth;
        private readonly int backgroundHeight;
        private readonly int laneHeight;
        private readonly VehicleFactory vehicleFactory;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="laneHeight">Height of the lane.</param>
        public RoadManager(int backgroundWidth, int backgroundHeight, int laneHeight)
        {
            this.lanes = new List<Lane>();
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
            this.laneHeight = laneHeight;
            this.vehicleFactory = new VehicleFactory();
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
            foreach (var lane in this.lanes)
            {
                yield return lane;
            }
        }

        /// <summary>
        ///     Constructs the road.
        ///     Precondition: none
        ///     Postcondition: All 5 lanes are placed and constructed
        /// </summary>
        public void ConstructRoad()
        {
            this.constructLane1();
            this.constructLane2();
            this.constructLane3();
            this.constructLane4();
            this.constructLane5();
        }

        private void constructLane5()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.SetVehiclesToLane(this.calculateLanePosition(5));
            this.lanes.Add(lane);
        }

        private void constructLane4()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructSuperCarThatMovesLeft(10));
            lane.SetVehiclesToLane(this.calculateLanePosition(4));
            this.lanes.Add(lane);
        }

        private void constructLane3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.SetVehiclesToLane(this.calculateLanePosition(3));
            this.lanes.Add(lane);
        }

        private void constructLane2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(2));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(2));
            lane.SetVehiclesToLane(this.calculateLanePosition(2));
            this.lanes.Add(lane);
        }

        private void constructLane1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(1));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(1));
            lane.SetVehiclesToLane(this.calculateLanePosition(1));
            this.lanes.Add(lane);
        }

        private int calculateLanePosition(int laneNumber)
        {
            return this.backgroundHeight - this.laneHeight * laneNumber - LaneOffsetFromBottom;
        }

        /// <summary>
        ///     Moves the vehicles.
        ///     Precondition: none
        ///     PostCondition: All vehicles in the road move in their specified direction
        /// </summary>
        public void MoveVehiclesInRoad()
        {
            foreach (var lane in this.lanes)
            {
                lane.MoveVehiclesInLane();
            }
        }

        /// <summary>
        ///     Wraps the  road.
        /// </summary>
        public void WrapRoad()
        {
            foreach (var lane in this.lanes)
            {
                lane.WrapLane();
            }
        }

        IEnumerator<Lane> IEnumerable<Lane>.GetEnumerator()
        {
            return this.lanes.GetEnumerator();
        }

        #endregion
    }
}