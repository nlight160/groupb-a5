using System;

namespace FroggerStarter.View
{
    /// <summary>
    ///     Event handles setting game over text visibility
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class GameOverEventArg : EventArgs
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the game over text block.
        /// </summary>
        /// <value>
        ///     The game over text block.
        /// </value>
        public bool GameOver { get; set; }

        #endregion
    }

    /// <summary>
    ///     Event handles updating score
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class UpdateScoreEventArg : EventArgs
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }

        #endregion
    }

    /// <summary>
    ///     Event handles updating life timer
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class UpdateTimerEventArg : EventArgs
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the life timer.
        /// </summary>
        /// <value>
        ///     The life timer.
        /// </value>
        public int LifeTimer { get; set; }

        #endregion
    }

    /// <summary>
    ///     Handles updating the lives
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class UpdateLivesEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the remaining lives.
        /// </summary>
        /// <value>
        ///     The remaining lives.
        /// </value>
        public int RemainingLives { get; set; }

        #endregion
    }
}