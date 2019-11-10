﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FroggerStarter.Extensions;
using FroggerStarter.Model;

namespace FroggerStarter.ViewModel
{
    /// <summary>
    /// </summary>
    public class ScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private readonly ScoreBoard scoreBoard;

        private ObservableCollection<Score> scores;

        #endregion

        #region Properties

        /// <summary>Gets or sets the scores.</summary>
        /// <value>The scores.</value>
        public ObservableCollection<Score> Scores
        {
            get { return this.scores; }
            set
            {
                this.scores = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ScoreBoardViewModel" /> class.</summary>
        public ScoreBoardViewModel()
        {
            ScoreBoard scoreBoard;
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