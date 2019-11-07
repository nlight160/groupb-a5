using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Handles functionality for frog homes
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.StationaryObject" />
    public class FrogHome : StationaryObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FrogHome" /> class.
        ///     Precondition: none
        ///     Postcondition: A frog home object is created
        /// </summary>
        public FrogHome()
        {
            Sprite = new FrogHomeSprite {
                Visibility = Visibility.Collapsed
            };
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Marks the frog home occupied.
        ///     Precondition: none
        ///     Postcondition: frog home is revealed
        /// </summary>
        public void MarkFrogHomeOccupied()
        {
            Sprite.Visibility = Visibility.Visible;
        }

        /// <summary>Marks the frog home un occupied.</summary>
        public void MarkFrogHomeUnOccupied()
        {
            Sprite.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///     Determines whether [is frog home occupied].
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is frog home occupied]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFrogHomeOccupied()
        {
            return Sprite.Visibility == Visibility.Visible;
        }

        #endregion
    }
}