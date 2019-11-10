namespace FroggerStarter.Model
{
    /// <summary>Initialize Score</summary>
    public class Score
   {

       /// <summary>Gets or sets the name.</summary>
       /// <value>The name.</value>
       public  string Name { get; private set; }

       /// <summary>Gets or sets the value.</summary>
       /// <value>The value.</value>
       public  int Value { get; private set; }

        /// <summary>Gets or sets the level.</summary>
        /// <value>The level.</value>
        public  int Level { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Score"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        public Score(string name, int score, int level)
        {
            this.Name = name;
            this.Value = score;
            this.Level = level;
        }

    }
}
