using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all of the lanes
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private const int LastRound = 3;
        private const int FirstRound = 1;
        private const int SecondRound = 2;
        private readonly IList<Lane> lanes;
        private readonly int backgroundWidth;
        private readonly int backgroundHeight;
        private readonly VehicleFactory vehicleFactory;
        private readonly GameSettings gameSettings;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the round count.
        /// </summary>
        /// <value>
        ///     The round count.
        /// </value>
        public int RoundCount { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        ///     Precondition: none
        ///     PostCondition: a new roadmanager is created
        /// </summary>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="backgroundHeight">Height of the background.</param>
        public RoadManager(int backgroundWidth, int backgroundHeight)
        {
            this.gameSettings = new GameSettings();
            this.lanes = new List<Lane>();
            this.backgroundWidth = backgroundWidth;
            this.backgroundHeight = backgroundHeight;
            this.vehicleFactory = new VehicleFactory();
            this.RoundCount = FirstRound;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            foreach (var lane in this.lanes)
            {
                foreach (var vehicle in lane)
                {
                    yield return vehicle;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Determines whether [is last round].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is last round]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLastRound()
        {
            return this.RoundCount == LastRound;
        }

        /// <summary>
        ///     Constructs the road.
        ///     Precondition: none
        ///     Postcondition: All 5 level 1 lanes are placed and constructed
        /// </summary>
        public void ConstructRoad()
        {
            if (this.RoundCount == FirstRound)
            {
                this.constructRoadLevel1();
            }
            else if (this.RoundCount == SecondRound)
            {
                this.constructRoadLevel2();
            }
            else
            {
                this.constructRoadLevel3();
            }
        }

        private void constructRoadLevel1()
        {
            this.constructLane1Level1();
            this.constructLane2Level1();
            this.constructLane3Level1();
            this.constructLane4Level1();
            this.constructLane5Level1();
            this.makeFirstVehiclesVisible();
        }

        /// <summary>
        ///     Constructs the road level2.
        ///     Precondition: none
        ///     Postcondition: All 5 level 2 lanes are placed and constructed
        /// </summary>
        private void constructRoadLevel2()
        {
            this.lanes.Clear();
            this.constructLane1Level2();
            this.constructLane2Level2();
            this.constructLane3Level2();
            this.constructLane4Level2();
            this.constructLane5Level2();
            this.makeFirstVehiclesVisible();
        }

        /// <summary>
        ///     Constructs the road level3.
        ///     Precondition: none
        ///     Postcondition: All 5 level 3 lanes are placed and constructed
        /// </summary>
        private void constructRoadLevel3()
        {
            this.lanes.Clear();
            this.constructLane1Level3();
            this.constructLane2Level3();
            this.constructLane3Level3();
            this.constructLane4Level3();
            this.constructLane5Level3();
            this.makeFirstVehiclesVisible();
        }

        private void constructLane5Level1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(5));
            lane.SetVehiclesToLane(this.calculateLanePosition(5));
            this.lanes.Add(lane);
        }

        private void constructLane4Level1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructSuperCarThatMovesLeft(10));
            lane.SetVehiclesToLane(this.calculateLanePosition(4));
            this.lanes.Add(lane);
        }

        private void constructLane3Level1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.SetVehiclesToLane(this.calculateLanePosition(3));
            this.lanes.Add(lane);
        }

        private void constructLane2Level1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(2));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(2));
            lane.SetVehiclesToLane(this.calculateLanePosition(2));
            this.lanes.Add(lane);
        }

        private void constructLane1Level1()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(1));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(1));
            lane.SetVehiclesToLane(this.calculateLanePosition(1));
            this.lanes.Add(lane);
        }

        private void constructLane5Level2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(6));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(6));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(6));
            lane.SetVehiclesToLane(this.calculateLanePosition(5));
            this.lanes.Add(lane);
        }

        private void constructLane4Level2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesLeft(5));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesLeft(5));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesLeft(5));
            lane.SetVehiclesToLane(this.calculateLanePosition(4));
            this.lanes.Add(lane);
        }

        private void constructLane3Level2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(4));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(4));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(4));
            lane.SetVehiclesToLane(this.calculateLanePosition(3));
            this.lanes.Add(lane);
        }

        private void constructLane2Level2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesRight(3));
            lane.SetVehiclesToLane(this.calculateLanePosition(2));
            this.lanes.Add(lane);
        }

        private void constructLane1Level2()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructSuperCarThatMovesRight(6));
            lane.SetVehiclesToLane(this.calculateLanePosition(1));
            this.lanes.Add(lane);
        }

        private void constructLane5Level3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(7));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(7));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(7));
            lane.SetVehiclesToLane(this.calculateLanePosition(5));
            this.lanes.Add(lane);
        }

        private void constructLane4Level3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(6));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(6));
            lane.SetVehiclesToLane(this.calculateLanePosition(4));
            this.lanes.Add(lane);
        }

        private void constructLane3Level3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructSuperCarThatMovesLeft(8));
            lane.SetVehiclesToLane(this.calculateLanePosition(3));
            this.lanes.Add(lane);
        }

        private void constructLane2Level3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(4));
            lane.AddVehicle(this.vehicleFactory.ConstructTruckThatMovesRight(4));
            lane.SetVehiclesToLane(this.calculateLanePosition(2));
            this.lanes.Add(lane);
        }

        private void constructLane1Level3()
        {
            var lane = new Lane(this.backgroundWidth);
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.AddVehicle(this.vehicleFactory.ConstructCarThatMovesLeft(3));
            lane.SetVehiclesToLane(this.calculateLanePosition(1));
            this.lanes.Add(lane);
        }

        private int calculateLanePosition(int laneNumber)
        {
            return this.backgroundHeight - this.gameSettings.LaneHeight * laneNumber -
                   (this.gameSettings.LaneHeight + this.gameSettings.BottomLaneOffset);
        }

        private void makeFirstVehiclesVisible()
        {
            foreach (var lane in this.lanes)
            {
                lane.ElementAt(0).Sprite.Visibility = Visibility.Visible;
            }
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
        ///     Precondition: none
        ///     Postcondition: lane wraps back to its beginning
        /// </summary>
        public void WrapRoad()
        {
            foreach (var lane in this.lanes)
            {
                lane.WrapLane();
            }
        }

        #endregion
    }
}