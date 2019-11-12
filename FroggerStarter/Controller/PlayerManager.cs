namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles statistics related to the player
    /// </summary>
    public class PlayerManager
    {
        #region Data members

        private const int LevelLimit = 4;

        private readonly GameSettings gameSettings;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the remaining lives.
        /// </summary>
        /// <value>
        ///     The remaining lives.
        /// </value>
        public int RemainingLives { get; private set; }

        /// <summary>
        ///     Gets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; private set; }

        /// <summary>
        ///     Gets the homes occupied.
        /// </summary>
        /// <value>
        ///     The homes occupied.
        /// </value>
        public int HomesOccupied { get; private set; }

        /// <summary>
        ///     Gets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        public int Level { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerManager" /> class.
        ///     Precondition: none
        ///     Postcondition: a player manager object is created
        /// </summary>
        public PlayerManager()
        {
            this.gameSettings = new GameSettings();
            this.RemainingLives = this.gameSettings.RemainingLives;
            this.Score = 0;
            this.Level = 1;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Decrements the lives.
        ///     Precondition: Lives > 0
        ///     Postcondition: RemainingLived - 1
        /// </summary>
        public void DecrementLives()
        {
            if (this.RemainingLives > 0)
            {
                this.RemainingLives--;
            }
        }

        /// <summary>
        ///     Increments the level.
        ///     Precondition: none
        ///     PostCondition: HomesOccupied is reset and level increases by one
        /// </summary>
        public void NextLevel()
        {
            if (this.HomesOccupied == this.gameSettings.NumberOfHomes)
            {
                this.Level++;
                this.HomesOccupied = 0;
            }
        }

        /// <summary>
        ///     Increments the score.
        ///     Precondition: none
        ///     Postcondition: Score is increased by lives specified value
        /// </summary>
        public void IncrementScore(int scoreToAdd)
        {
            this.Score += scoreToAdd * this.RemainingLives;
        }

        /// <summary>
        ///     Determines whether [is game over].
        ///     PreCondition: none
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is game over]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGameOverConditionMet()
        {
            return this.RemainingLives == 0 || this.Level == LevelLimit;
        }

        /// <summary>
        ///     Determines whether [is round changing].
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is round changing]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRoundChanging()
        {
            return this.HomesOccupied == this.gameSettings.NumberOfHomes;
        }

        /// <summary>
        ///     Increments the houses occupied.
        ///     Precondition: none
        ///     Postcondition: HomesOccupied + 1
        /// </summary>
        public void IncrementHousesOccupied()
        {
            this.HomesOccupied++;
        }

        #endregion
    }
}