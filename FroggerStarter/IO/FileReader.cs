using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Media.Animation;
using FroggerStarter.Model;

namespace FroggerStarter.IO
{
    /// <summary>
    ///     Reads XML File
    /// </summary>
    public class FileReader
    {
        #region Data members

        /// <summary>The score board</summary>
        public  ScoreBoard ScoreBoard;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="FileReader" /> class.</summary>
        public FileReader()
        {
            this.ScoreBoard = new ScoreBoard();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Reads the current file asynchronous.
        ///     Precondition: none
        ///     Postcondition: file is read
        /// </summary>
        public async Task ReadCurrentFileAsync()
        {
            string fileName = "HighScoreBoard.xml";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string path = Path.Combine(folderPath, fileName);
            XmlRootAttribute xRoot = new XmlRootAttribute {ElementName = "Scores", IsNullable = true};

            XmlSerializer serializer = new XmlSerializer(typeof(ScoreBoard), xRoot);

            StreamReader reader = new StreamReader(path);
            this.ScoreBoard = (ScoreBoard)serializer.Deserialize(reader);
            reader.Close();


        }

        private async void readFromXml(IStorageFile file)
        {
            var serializer = new XmlSerializer(typeof(ScoreBoard));
            ScoreBoard result;
            
            var stream = await file.OpenStreamForReadAsync();
            {
                result = (ScoreBoard)serializer.Deserialize(stream);
            }

            
        }

        #endregion
    }
}