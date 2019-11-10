using System;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View.ContentDialogs
{
    /// <summary>Add score</summary>
    public sealed partial class AddScoreContentDialog
    {
        /// <summary>
        /// Name of the player
        /// </summary>
        public string PlayerName;

        public bool IsPrimary;


        /// <summary>Initializes a new instance of the <see cref="AddScoreContentDialog"/> class.</summary>
        public AddScoreContentDialog()
        {
            this.InitializeComponent();
            IsPrimaryButtonEnabled = false;
            this.IsPrimary = false;
        }

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
            if (this.nameBox.Text == String.Empty)
            {
                IsPrimaryButtonEnabled = false;
            }
            else
            {
                IsPrimaryButtonEnabled = true;
            }
        }
    }
}
