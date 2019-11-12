using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View.ContentDialogs
{
    /// <summary>
    ///     Handles high score content dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class HighScoreContentDialog
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreContentDialog" /> class.
        ///     Precondition: none
        ///     Postcondition: a high score content dialog is created
        /// </summary>
        public HighScoreContentDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        #endregion
    }
}