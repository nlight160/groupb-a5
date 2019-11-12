using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View.ContentDialogs
{
    /// <summary>
    ///     handles Add score content dialog
    /// </summary>
    public sealed partial class AddScoreContentDialog
    {
        #region Data members

        /// <summary>
        ///     Name of the player
        /// </summary>
        public string PlayerName;

        /// <summary>
        ///     The is primary
        /// </summary>
        public bool IsPrimary;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddScoreContentDialog" /> class.
        ///     Precondition: none
        ///     PostCondtion: an add score content dialog is created
        /// </summary>
        public AddScoreContentDialog()
        {
            this.InitializeComponent();
            IsPrimaryButtonEnabled = false;
            this.IsPrimary = false;
        }

        #endregion

        #region Methods

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.PlayerName = this.nameBox.Text;
            this.IsPrimary = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.nameBox.Text == string.Empty)
            {
                IsPrimaryButtonEnabled = false;
            }
            else
            {
                IsPrimaryButtonEnabled = true;
            }
        }

        #endregion
    }
}