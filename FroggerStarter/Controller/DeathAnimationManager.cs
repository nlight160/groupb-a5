using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Model;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Controller
{


    /// <summary>
    /// 
    /// </summary>
    public class DeathAnimationManager
    {

        public IList<DeathAnimationFrame> deathAnimation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathAnimationManager"/> class.
        /// </summary>
        public DeathAnimationManager()
        {
            this.deathAnimation = new List<DeathAnimationFrame>();
            this.initializeDeathSprites();
        }

        private void initializeDeathSprites()
        {
            this.deathAnimation.Add(new DeathAnimationFrame(new DeathSprite1()));
            this.deathAnimation.Add(new DeathAnimationFrame(new DeathSprite2()));
            this.deathAnimation.Add(new DeathAnimationFrame(new DeathSprite3()));
            this.deathAnimation.Add(new DeathAnimationFrame(new DeathSprite4()));
        }
    }
}
