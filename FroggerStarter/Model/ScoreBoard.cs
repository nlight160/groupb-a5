using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Instance of  score board
    /// </summary>
     [Serializable]
     [XmlRoot(ElementName = "Score")]
    public class ScoreBoard : IEnumerable<Score>
    {
        
        private readonly IList<Score> scoreBoard;
        public ICollection<Score> Scores => this.scoreBoard;

        /// <summary>Initializes a new instance of the <see cref="ScoreBoard"/> class.</summary>
        public ScoreBoard()
        {
            this.scoreBoard = new List<Score>();
        }

        /// <summary>Adds the score.</summary>
        /// <param name="score">The score.</param>
        public void Add(Score score)
        {
            if (score != null)
            {
                this.scoreBoard.Add(score);
            }
        }
        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Score> GetEnumerator()
        {
            foreach (var score in this.scoreBoard)
            {
                yield return score;

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.scoreBoard.GetEnumerator();
        }
    }
}
