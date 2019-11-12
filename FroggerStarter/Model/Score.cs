using System;
using System.Xml.Serialization;

namespace FroggerStarter.Model
{
    /// <summary>Initialize Score</summary>
    [Serializable]
    [XmlRoot(ElementName = "Score")]
    public class Score
    {
        #region Properties

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [XmlElement("Value")]
        public int Value { get; set; }

        /// <summary>Gets or sets the level.</summary>
        /// <value>The level.</value>
        [XmlElement("Level")]
        public int Level { get; set; }

        /// <summary>
        ///     Gets the full score.
        /// </summary>
        /// <value>
        ///     The full score.
        /// </value>
        public string FullScore => $"{this.Name} : Score {this.Value} : Level {this.Level}";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Score" /> class.
        /// </summary>
        public Score()
        {
            this.Name = string.Empty;
            this.Value = 0;
            this.Level = 0;
        }

        #endregion
    }
}