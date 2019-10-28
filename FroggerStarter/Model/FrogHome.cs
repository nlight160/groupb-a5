using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class FrogHome : BasicObject
    {

        private readonly ICollection<FrogHome> homes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHome" /> class.
        /// </summary>
        public FrogHome()
        {
            this.homes = new Collection<FrogHome>();
            Sprite = new FrogHomeSprite();
        }

        /// <summary>
        /// Sets the frog homes.
        /// </summary>
        public void SetFrogHomes()
        {
            var homeGap = 150;
            var nextX = 0;
            foreach (var frogHome in this.homes)
            {
                nextX += homeGap;
                frogHome.X = nextX;
                frogHome.Y = Sprite.Height;
            }
        }

        /// <summary>
        /// Adds the frog home.
        /// </summary>
        /// <param name="home">The home.</param>
        public void AddFrogHome(FrogHome home)
        {
            this.homes.Add(home);
        }


    }
}
