using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using FroggerStarter.Extensions;
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
        public ScoreBoard ScoreBoard;

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
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(folderPath, fileName);
            var xRoot = new XmlRootAttribute {ElementName = "Score", IsNullable = true};

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
                result = (ScoreBoard) serializer.Deserialize(stream);
            }
        }

        /// <summary>
        ///     Gets the list asynchronously.
        /// </summary>
        /// <returns>
        ///     the collection object
        /// </returns>
        public Task<ObservableCollection<Score>> GetListAsync()
        {
            return Task.Run(() => this.ScoreBoard.ToObservableCollection());
        }

        #endregion
    }
}