using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using FroggerStarter.Model;

namespace FroggerStarter.IO
{
    /// <summary>
    ///     Saves and writes to xml file
    /// </summary>
    public class SaveFileWriter
    {
        #region Methods

        /// <summary>
        ///     Saves a file asynchronous.
        ///     Precondition: score != null
        ///     Postcondition: score is serialized and saved
        /// </summary>
        /// <param name="score">The score.</param>
        public async Task SaveAFileAsync(Score score)
        {
            if (score == null)
            {
                throw new ArgumentNullException(nameof(score));
            }

            var fileName = "HighScoreBoard.xml";
            var projectDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(projectDirectory, fileName);

            using (var sw = new StreamWriter(path, true))
            {
                sw.Write(score);
            }

            IStorageFile newFile = await StorageFile.GetFileFromPathAsync(path);

            if (newFile.Name.EndsWith(".xml"))
            {
                this.writeToXml(newFile, score);
            }
        }

        private async void writeToXml(IStorageFile newFile, Score score)
        {
            var serializer = new XmlSerializer(typeof(Score));
            var writeStream = await newFile.OpenStreamForWriteAsync();

            serializer.Serialize(writeStream, score);

            writeStream.Close();
        }

        #endregion
    }
}