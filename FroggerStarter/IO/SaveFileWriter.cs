using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using FroggerStarter.Model;

namespace FroggerStarter.IO
{
    /// <summary></summary>
    public class SaveFileWriter
    {
        #region Methods

        /// <summary>Saves a file asynchronous.</summary>
        /// <param name="score">The score.</param>
        public async Task SaveAFileAsync(Score score)
        {
            var fileName = "HighScoreBoard.xml";
            var projectDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(projectDirectory, fileName);

            IStorageFile newFile = await StorageFile.GetFileFromPathAsync(path);
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var xDocument = XDocument.Load(path);
            var root = xDocument.Element("Score");
            var rows = root.Descendants("Score");
            var firstRow = rows.First();
            firstRow.AddBeforeSelf(
                new XElement("Score",
                    new XElement("Name", score.Name),
                    new XElement("Value", score.Value),
                    new XElement("Level", score.Level)));
            xDocument.Save(path);
            using (var writer = xDocument.CreateWriter())
            {
                var serializer = new XmlSerializer(typeof(Score));
                serializer.Serialize(writer, score);
            }

           
        }

        private async void writeToXml(IStorageFile newFile, Score score)
        {
            var serializer = new XmlSerializer(typeof(ScoreBoard));
            var writeStream = await newFile.OpenStreamForWriteAsync();
            var board = new ScoreBoard();

            using (writeStream)
            {
                board.Add(score);
                serializer.Serialize(writeStream, board);
            }

            writeStream.Dispose();
        }

        #endregion
    }
}