using System.Collections;
using System.Collections.Generic;

namespace FroggerStarter.Model
{
    /// <summary>
    /// Instance of  score board
    /// </summary>
    public class ScoreBoard : IEnumerable<Score>
    {

        private readonly IList<Score> scoreBoard;

        /// <summary>Initializes a new instance of the <see cref="ScoreBoard"/> class.</summary>
        public ScoreBoard()
        {
            this.scoreBoard = new List<Score>();
        }

        /// <summary>Adds the score.</summary>
        /// <param name="score">The score.</param>
        public void AddScore(Score score)
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
