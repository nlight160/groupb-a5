using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.IO;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    public class ScoreBoardManager
    {
        public ScoreBoard ScoreBoard;

        public FileReader FileReder;

        public SaveFileWriter SaveFile;

        /// <summary>Initializes a new instance of the <see cref="ScoreManager"/> class.</summary>
        public ScoreBoardManager()
        {
            this.ScoreBoard = new ScoreBoard();
            this.FileReder = new FileReader();
            this.SaveFile = new SaveFileWriter();
        }

        /// <summary>Adds the new score.</summary>
        /// <param name="score">The score.</param>
        public async void AddNewScore(Score score)
        {
           await this.SaveFile.SaveAFileAsync(score);
        }


    }
}
