using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class CollisionDetection
    {
        private readonly Frog player;
        private readonly Collection<FrogHome> homes;
        private readonly RoadManager lanes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionDetection"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="homes">The homes.</param>
        /// <param name="lanes">The lanes.</param>
        public CollisionDetection(Frog player, Collection<FrogHome> homes, RoadManager lanes)
        {
            this.player = player;
            this.homes = homes;
            this.lanes = lanes;

        }

        /// <summary>
        /// Checks for vehicle on player collision.
        /// </summary>
        /// <returns></returns>
        public bool CheckForVehicleOnPlayerCollision()
        {
            foreach (Lane lane in this.lanes)
            {
                foreach (Vehicle vehicle in lane)
                {
                    return this.player.BoundingBox.IntersectsWith(vehicle.BoundingBox);
                }
            }

            return false;
        }

        /// <summary>
        /// Checks for player on frog home collision.
        /// </summary>
        /// <returns></returns>
        public bool CheckForPlayerOnFrogHomeCollision()
        {
            foreach (var home in this.homes)
            {
                return this.player.BoundingBox.IntersectsWith(home.BoundingBox);
            }

            return false;
        }
    }
}
