using System;
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

            var deserializer = new XmlSerializer(typeof(ScoreBoard));
            var result = (ScoreBoard)deserializer.Deserialize(inStream);
            foreach (var item in result)
            {
                this.ScoreBoard.Add(item);
            }
            inStream.Dispose();

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