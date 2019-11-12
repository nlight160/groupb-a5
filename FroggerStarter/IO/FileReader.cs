using System;
using System.IO;
using System.Xml.Serialization;
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
        public void ReadCurrentFile()
        {
            var fileName = "HighScoreBoard.xml";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(folderPath, fileName);
            var xRoot = new XmlRootAttribute {ElementName = "Score", IsNullable = true};

            var serializer = new XmlSerializer(typeof(ScoreBoard), xRoot);

            var reader = new StreamReader(path);
            this.ScoreBoard = (ScoreBoard) serializer.Deserialize(reader);
            reader.Close();
        }

        #endregion
    }
}