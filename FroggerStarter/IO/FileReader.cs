using System;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using FroggerStarter.Model;
using FileAttributes = Windows.Storage.FileAttributes;

namespace FroggerStarter.IO
{
    /// <summary>Reads XML File</summary>
    public class FileReader
    {
        #region Data members

        /// <summary>The score board</summary>
        public readonly ScoreBoard ScoreBoard;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="FileReader" /> class.</summary>
        public FileReader()
        {
            this.ScoreBoard = new ScoreBoard();
        }

        #endregion

        #region Methods

        /// <summary>Reads the current file asynchronous.</summary>
        public async Task ReadCurrentFileAsync()
        {
            var fileName = "HighScoreBoard.xml";
            var path = Path.Combine(Environment.CurrentDirectory, @"groupb-a5\", fileName);
            var storageFolder =
                ApplicationData.Current.LocalFolder;

            IStorageFile file = await storageFolder.GetFileAsync(path);
        }

        private async void readFromXml(IStorageFile file)
        {
            var serializer = new XmlSerializer(typeof(ScoreBoard));
            ScoreBoard result;

            var stream = await file.OpenStreamForReadAsync();
            {
                result = (ScoreBoard) serializer.Deserialize(stream);
            }

            foreach (var item in result)
            {
                this.ScoreBoard.AddScore(item);
            }
        }

        #endregion
    }
}