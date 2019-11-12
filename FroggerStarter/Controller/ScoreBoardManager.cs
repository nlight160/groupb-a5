using System;
using FroggerStarter.Model;
using FroggerStarter.View.ContentDialogs;
using FroggerStarter.ViewModel;

namespace FroggerStarter.Controller
{
    /// <summary>Represents a score board</summary>
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
        public ScoreBoardViewModel viewModel;

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

            this.viewModel = new ScoreBoardViewModel();

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

            this.ScoreBoard.Add(score);
        }

        /// <summary>Reads the high score.</summary>
        public async void ReadHighScore()
        {
            await this.viewModel.FileReader.ReadCurrentFileAsync();

            this.viewModel.Scores = await this.viewModel.FileReader.GetListAsync();

        }

        /// <summary>Saves the new score.</summary>
        /// <param name="score">The score.</param>
        public async void SaveNewScore(Score score)
        {
            await this.viewModel.SaveFile.SaveAFileAsync(score);

            this.viewModel.Scores.Add(score);
        }

        #endregion
    }
}