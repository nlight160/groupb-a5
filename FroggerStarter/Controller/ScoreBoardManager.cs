using FroggerStarter.IO;
using FroggerStarter.Model;
using FroggerStarter.ViewModel;

namespace FroggerStarter.Controller
{
    /// <summary>Represents a score board</summary>
    public class ScoreBoardManager
    {
        public ScoreBoard ScoreBoard;

        public FileReader FileReader;

        public SaveFileWriter SaveFile;

        public ScoreBoardViewModel viewModel;

        /// <summary>Initializes a new instance of the <see cref="ScoreManager"/> class.</summary>
        public ScoreBoardManager()
        {
            this.ScoreBoard = new ScoreBoard();
            this.FileReader = new FileReader();
            this.SaveFile = new SaveFileWriter();
            this.viewModel = new ScoreBoardViewModel();
        }

        /// <summary>Adds the new score.</summary>
        /// <param name="score">The score.</param>
        public async void AddNewScore(Score score)
        {
           await this.SaveFile.SaveAFileAsync(score);
        }

        /// <summary>Reads the high score.</summary>
        public async void ReadHighScore()
        {
            await this.FileReader.ReadCurrentFileAsync();
            this.ScoreBoard = this.FileReader.ScoreBoard;
         
        }


    }
}
