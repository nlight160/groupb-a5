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
        /// <summary>
        /// Checks for vehicle on player collision.
        /// </summary>
        /// <returns></returns>
        public bool CheckForVehicleOnPlayerCollision(Frog player, Vehicle vehicle)
        {
            return player.BoundingBox.IntersectsWith(vehicle.BoundingBox);
        }

        /// <summary>
        /// Checks for player on frog home collision.
        /// </summary>
        /// <returns></returns>
        public bool CheckForPlayerOnFrogHomeCollision(Frog player, FrogHome home)
        {
            return player.BoundingBox.IntersectsWith(home.BoundingBox);
        }
    }
}
