using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.BasicObject" />
    public class DeathAnimationFrame : BasicObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathAnimationFrame"/> class.
        /// </summary>
        public DeathAnimationFrame(BaseSprite deathSprite)
        {
            Sprite = deathSprite;
            Sprite.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Sets the frame location.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SetFrameLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Sets the frame visibility.
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        public void SetFrameVisibility(Visibility visibility)
        {
            Sprite.Visibility = visibility;
        }
    }
}
