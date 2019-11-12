using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Instance of  score board
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Score")]
    public class ScoreBoard : IEnumerable<Score>
    {
        #region Data members

        private readonly IList<Score> scoreBoard;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScoreBoard" /> class.
        ///     Precondition: none
        ///     Postcondition: a new score board is created
        /// </summary>
        public ScoreBoard()
        {
            this.scoreBoard = new List<Score>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
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

        /// <summary>Adds the score.</summary>
        /// <param name="score">The score.</param>
        public void Add(Score score)
        {
            if (score != null)
            {
                this.scoreBoard.Add(score);
            }
        }

        /// <summary>
        ///     Adds the score.
        ///     Precondition: score != null
        ///     Postcondition: score is added to score board
        /// </summary>
        /// <param name="score">The score.</param>
        public void AddScore(Score score)
        {
            if (score == null)
            {
                throw new ArgumentNullException(nameof(score));
            }

            this.scoreBoard.Add(score);
        }

        #endregion
    }
}