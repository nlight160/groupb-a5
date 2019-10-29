using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["AppWidth"];
        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GamePage" /> class.
        ///     Precondition: none
        ///     PostCondition: Game page is set and initialized
        /// </summary>
        public GamePage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().Title = "Frogger V2 by Nathaniel Lightholder";
            ApplicationView.PreferredLaunchViewSize = new Size
                {Width = this.applicationWidth, Height = this.applicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            this.gameManager = new GameManager(this.applicationHeight, this.applicationWidth);
            this.gameManager.InitializeGame(this.canvas);
            this.gameManager.GameOver += this.onGameOver;
            this.gameManager.UpdateScore += this.onUpdateScore;
            this.gameManager.UpdateTimer += this.onUpdateTimer;
            this.gameManager.UpdateLives += this.onUpdateLives;
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayerUp();
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayerDown();
                    break;
            }
        }

        private void onGameOver(object sender, GameOverEventArg e)
        {
            if (e.GameOver)
            {
                this.gameOverTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void onUpdateScore(object sender, UpdateScoreEventArg e)
        {
            this.scoreTextBlock.Text = "Score: " + e.Score;
        }

        private void onUpdateTimer(object sender, UpdateTimerEventArg e)
        {
            this.timerTextBlock.Text = "Timer: " + e.LifeTimer;
        }

        private void onUpdateLives(object sender, UpdateLivesEventArgs e)
        {
            this.livesTextBlock.Text = "Lives: " + e.RemainingLives;
        }

        #endregion
    }
}