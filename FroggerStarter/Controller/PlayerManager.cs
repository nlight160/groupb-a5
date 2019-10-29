using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// Handles statistics related to the player
    /// </summary>
    public class PlayerManager
    {

        /// <summary>
        /// Gets the remaining lives.
        /// </summary>
        /// <value>
        /// The remaining lives.
        /// </value>
        public int RemainingLives { get; private set; }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the homes occupied.
        /// </summary>
        /// <value>
        /// The homes occupied.
        /// </value>
        public int HomesOccupied { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerManager"/> class.
        /// </summary>
        public PlayerManager()
        {
            this.RemainingLives = 3;
            this.Score = 0;
        }

        /// <summary>
        /// Decrements the lives.
        /// </summary>
        public void DecrementLives()
        {
            if (this.RemainingLives > 0)
            {
                this.RemainingLives--;
            }
        }

        /// <summary>
        /// Increments the score.
        /// </summary>
        public void IncrementScore()
        {
            this.Score++;
        }

        /// <summary>
        /// Determines whether [is game over].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is game over]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGameOverConditionMet()
        {
            return this.RemainingLives == 0 || this.HomesOccupied == 5;
        }

        /// <summary>
        /// Increments the houses occupied.
        /// </summary>
        public void IncrementHousesOccupied()
        {
            this.HomesOccupied++;
        }
    }
}
