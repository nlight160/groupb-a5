using System;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Handles basic functionality of a death animation frame
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.StationaryObject" />
    public class AnimationFrame : StationaryObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationFrame" /> class.
        ///     Precondition: deathSprite != null
        ///     Postcondition: A new death animation frame is created
        /// </summary>
        /// <param name="deathSprite">The death sprite.</param>
        public AnimationFrame(BaseSprite deathSprite)
        {
            Sprite = deathSprite ?? throw new ArgumentNullException();
            Sprite.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the frame location.
        ///     Precondition: none
        ///     Postcondition: The sprite's X = new X AND The Sprites Y = new Y
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SetFrameLocation(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}