using System;
using System.Xml.Serialization;

namespace FroggerStarter.Model
{
    /// <summary>Initialize Score</summary>
    [Serializable]
    [XmlRoot(ElementName = "Score")]
    public class Score
   {

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [XmlElement("Name")]
        public  string Name { get;  set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [XmlElement("Value")]
        public  int Value { get;  set; }

        /// <summary>Gets or sets the level.</summary>
        /// <value>The level.</value
        [XmlElement("Level")]

        public  int Level { get;  set; }

    }
}