using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class FrogHome : BasicObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHome" /> class.
        /// </summary>
        public FrogHome()
        {
            Sprite = new FrogHomeSprite();
            Sprite.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Marks the frog home occupied.
        /// </summary>
        public void MarkFrogHomeOccupied()
        {
            Sprite.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Determines whether [is frog home occupied].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is frog home occupied]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFrogHomeOccupied()
        {
            if (Sprite.Visibility == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }
}
