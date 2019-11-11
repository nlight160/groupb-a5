using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
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
            var fileName = "HighScoreBoard.xml";
           

            var theFolder = ApplicationData.Current.LocalFolder;
            var theFile = await theFolder.GetFileAsync(fileName);
            var inStream = await theFile.OpenStreamForReadAsync();
         
            var projectDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Debug.Print(projectDirectory);
            var path = Path.Combine(projectDirectory, fileName);

            var board = new ScoreBoard();

            IStorageFile newFile = await StorageFile.GetFileFromPathAsync(path);
            var folder = ApplicationData.Current.LocalFolder;

            

            var deserializer = new XmlSerializer(typeof(Score));
            using (FileStream fileStream = new FileStream(newFile.Path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var test = (Score)deserializer.Deserialize(fileStream);
            }
            

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