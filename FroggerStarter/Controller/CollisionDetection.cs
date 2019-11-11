using System;
using Windows.UI.Xaml;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles detection of colliding objects on the player
    /// </summary>
    public class CollisionDetection
    {
        #region Methods

        /// <summary>Checks for vehicle on player collision.
        ///   Precondition: player != null AND vehicle != null</summary>
        /// <param name="player">The player.</param>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>True if player is intersecting a visible vehicle's bounding box, false otherwise</returns>
        /// <exception cref="ArgumentNullException">player
        ///   or
        ///   vehicle</exception>
        public bool CheckForVehicleOnPlayerCollision(Frog player, Vehicle vehicle)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            return player.BoundingBox.IntersectsWith(vehicle.BoundingBox) 
                   && vehicle.Sprite.Visibility == Visibility.Visible;
        }

        /// <summary>
        ///     Checks for player on frog home collision.
        ///     Precondition: player != null AND home != null
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="home">The home.</param>
        /// <returns>
        ///     True if player is intersecting a home's bounding box, false otherwise
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     player
        ///     or
        ///     home
        /// </exception>
        public bool CheckForPlayerOnFrogHomeCollision(Frog player, FrogHome home)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (home == null)
            {
                throw new ArgumentNullException(nameof(home));
            }

            return player.BoundingBox.IntersectsWith(home.BoundingBox);
        }

        /// <summary>
        ///     Checks for player on frog home collision.
        ///     Precondition: player != null AND powerup != null
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="powerUp">The power up.</param>
        /// <returns>
        ///     True if player is intersecting a home's bounding box, false otherwise
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     player
        ///     or
        ///     home
        /// </exception>
        public bool CheckForPlayerOnPowerUpCollision(Frog player, StationaryObject powerUp)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (powerUp == null)
            {
                throw new ArgumentNullException(nameof(powerUp));
            }

            return player.BoundingBox.IntersectsWith(powerUp.BoundingBox);
        }

        #endregion
    }
}