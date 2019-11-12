using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FroggerStarter.Extensions;
using FroggerStarter.IO;
using FroggerStarter.Model;

namespace FroggerStarter.ViewModel
{
    /// <summary>
    ///     Connects the game page score input to the data in the model
    /// </summary>
    public class ScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        public ScoreBoard scoreBoard;

        private ObservableCollection<Score> scores;


        #endregion

        #region Properties

        /// <summary>Gets or sets the scores.</summary>
        /// <value>The scores.</value>
        public ObservableCollection<Score> Scores
        {
            get => this.scores;
            set
            {
                this.scores = value;
                this.OnPropertyChanged();
            }
        }

        public FileReader FileReader { get; private set; }
        public SaveFileWriter SaveFile { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScoreBoardViewModel" /> class.
        ///     Precondition: none
        ///     Postcondition: a new score board view model is created
        /// </summary>
        public ScoreBoardViewModel()
        {

            this.FileReader = new FileReader();
            this.SaveFile = new SaveFileWriter();
            this.scoreBoard = new ScoreBoard();
            this.Scores = this.scoreBoard.ToObservableCollection();
        }


        
        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}