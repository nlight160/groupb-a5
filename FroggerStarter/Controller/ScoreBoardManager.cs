using System;
using FroggerStarter.IO;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages the score board
    /// </summary>
    public class ScoreBoardManager
    {
        #region Data members

        /// <summary>
        ///     The score board
        /// </summary>
        public ScoreBoard ScoreBoard;

        /// <summary>
        ///     The file reader
        /// </summary>
        public FileReader FileReader;

        /// <summary>
        ///     The save file
        /// </summary>
        public SaveFileWriter SaveFile;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScoreBoardManager" /> class.
        ///     Precondition: none
        ///     Postcondition: A new scoreboard manager is created
        /// </summary>
        public ScoreBoardManager()
        {
            this.ScoreBoard = new ScoreBoard();
            this.FileReader = new FileReader();
            this.SaveFile = new SaveFileWriter();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the new score.
        ///     Precondition: score != null
        ///     Postcondition: score is serialized and saved
        /// </summary>
        /// <param name="score">The score.</param>
        public async void AddNewScore(Score score)
        {
            if (score == null)
            {
                throw new ArgumentNullException(nameof(score));
            }

            await this.SaveFile.SaveAFileAsync(score);
        }

        #endregion
    }
}