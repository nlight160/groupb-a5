namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Stores base frogger settings
    /// </summary>
    public class GameSettings
    {
        #region Properties

        /// <summary>
        ///     Gets the bottom l ane offset.
        /// </summary>
        /// <value>
        ///     The bottom l ane offset.
        /// </value>
        public int BottomLaneOffset => 5;

        /// <summary>
        ///     Gets the height of the lane.
        /// </summary>
        /// <value>
        ///     The height of the lane.
        /// </value>
        public int LaneHeight => 50;

        /// <summary>
        ///     Gets the frog home offset.
        /// </summary>
        /// <value>
        ///     The frog home offset.
        /// </value>
        public int FrogHomeOffset => 150;

        /// <summary>
        ///     Gets the top boundary y.
        /// </summary>
        /// <value>
        ///     The top boundary y.
        /// </value>
        public int TopBoundaryModifier => 3;

        /// <summary>
        ///     Gets the number of homes.
        /// </summary>
        /// <value>
        ///     The number of homes.
        /// </value>
        public int NumberOfHomes => 5;

        /// <summary>
        ///     Gets the initial time left.
        /// </summary>
        /// <value>
        ///     The initial time left.
        /// </value>
        public int InitialTimeLeft => 20;

        /// <summary>
        ///     Gets the remaining lives.
        /// </summary>
        /// <value>
        ///     The remaining lives.
        /// </value>
        public int RemainingLives => 4;

        /// <summary>
        ///     Gets the duration of the invincibility.
        /// </summary>
        /// <value>
        ///     The duration of the invincibility.
        /// </value>
        public int InvincibilityDuration => 10;

        /// <summary>
        ///     Gets the minimum bonus time respawn.
        /// </summary>
        /// <value>
        ///     The minimum bonus time respawn.
        /// </value>
        public int MinBonusTimeRespawn => 1;

        /// <summary>
        ///     Gets the maximum bonus time respawn.
        /// </summary>
        /// <value>
        ///     The maximum bonus time respawn.
        /// </value>
        public int MaxBonusTimeRespawn => 21;

        #endregion
    }
}