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

        public RelayCommand NameCommand { get; set; }
        public RelayCommand ScoreCommand { get; set; }
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
            this.NameCommand = new RelayCommand(this.SortName, this.CanSortName);
            this.ScoreCommand = new RelayCommand(this.SortScore, this.CanSortScore);
            this.LevelCommand = new RelayCommand(this.SortLevel, this.CanSortLevel);
            this.NameCommand.OnCanExecuteChanged();
            this.ScoreCommand.OnCanExecuteChanged();
            this.LevelCommand.OnCanExecuteChanged();
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void updateScores()
        {
            this.Scores = this.scoreBoard.ToObservableCollection();
        }

        private bool CanSortName(object obj)
        {
            return this.Scores != null;
        }

        private void SortName(object obj)
        {
            var result = this.scoreBoard.Select(x => x).OrderBy(x => x.Name).ThenBy(x => x.Value).ThenBy(x => x.Level)
                             .Take(10);
            this.Scores = result.ToObservableCollection();
        }

        private bool CanSortScore(object obj)
        {
            return this.Scores != null;
        }

        private void SortScore(object obj)
        {
            var result = this.scoreBoard.Select(x => x).OrderBy(x => x.Value).ThenBy(x => x.Name).ThenBy(x => x.Level)
                             .Take(10);
            this.Scores = result.ToObservableCollection();
        }

        private bool CanSortLevel(object obj)
        {
            return this.Scores != null;
        }

        private void SortLevel(object obj)
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