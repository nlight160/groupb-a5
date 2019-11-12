using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FroggerStarter.Extensions;
using FroggerStarter.Model;
using FroggerStarter.Utility;
using NathanielLightholderA4.Annotations;

namespace FroggerStarter.ViewModel
{
    /// <summary>
    ///     Connects the game page score input to the data in the model
    /// </summary>
    public class ScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        /// <summary>
        ///     The score board
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the name command.
        /// </summary>
        /// <value>
        ///     The name command.
        /// </value>
        public RelayCommand NameCommand { get; set; }

        /// <summary>
        ///     Gets or sets the score command.
        /// </summary>
        /// <value>
        ///     The score command.
        /// </value>
        public RelayCommand ScoreCommand { get; set; }

        /// <summary>
        ///     Gets or sets the level command.
        /// </summary>
        /// <value>
        ///     The level command.
        /// </value>
        public RelayCommand LevelCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScoreBoardViewModel" /> class.
        ///     Precondition: none
        ///     Postcondition: a new score board view model is created
        /// </summary>
        public ScoreBoardViewModel()
        {
            this.scoreBoard = new ScoreBoard();
            this.Scores = this.scoreBoard.ToObservableCollection();
            this.NameCommand = new RelayCommand(this.sortName, this.canSortName);
            this.ScoreCommand = new RelayCommand(this.sortScore, this.canSortScore);
            this.LevelCommand = new RelayCommand(this.sortLevel, this.canSortLevel);
            this.NameCommand.OnCanExecuteChanged();
            this.ScoreCommand.OnCanExecuteChanged();
            this.LevelCommand.OnCanExecuteChanged();
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Updates the scores.
        /// </summary>
        public void updateScores()
        {
            this.Scores = this.scoreBoard.ToObservableCollection();
        }

        /// <summary>
        ///     Determines whether this instance [can sort name] the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can sort name] the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool canSortName(object obj)
        {
            return this.Scores != null;
        }

        /// <summary>
        ///     Sorts the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void sortName(object obj)
        {
            var result = this.scoreBoard.Select(x => x).OrderBy(x => x.Name).ThenBy(x => x.Value).ThenBy(x => x.Level)
                             .Take(10);
            this.Scores = result.ToObservableCollection();
        }

        /// <summary>
        ///     Determines whether this instance [can sort score] the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can sort score] the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool canSortScore(object obj)
        {
            return this.Scores != null;
        }

        /// <summary>
        ///     Sorts the score.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void sortScore(object obj)
        {
            var result = this.scoreBoard.Select(x => x).OrderBy(x => x.Value).ThenBy(x => x.Name).ThenBy(x => x.Level)
                             .Take(10);
            this.Scores = result.ToObservableCollection();
        }

        /// <summary>
        ///     Determines whether this instance [can sort level] the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can sort level] the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool canSortLevel(object obj)
        {
            return this.Scores != null;
        }

        /// <summary>
        ///     Sorts the level.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void sortLevel(object obj)
        {
            var result = this.scoreBoard.Select(x => x).OrderBy(x => x.Level).ThenBy(x => x.Value).ThenBy(x => x.Name)
                             .Take(10);
            this.Scores = result.ToObservableCollection();
        }

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}