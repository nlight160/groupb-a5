using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Pickers;
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
       
            var fileName = "C: \\Users\\Marcus\\Desktop\\HighScoreBoard.xml";
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = Path.Combine(projectDirectory, "");
            var storageFolder =
                ApplicationData.Current.LocalFolder;
            var p = storageFolder.Path;
            var file = new FileInfo("C: \\Users\\Marcus\\Desktop\\HighScoreBoard.xml");
           
            IStorageFile newFile = await StorageFile.GetFileFromPathAsync(fileName);

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