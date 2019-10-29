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

        public IList<DeathAnimationFrame> Frames { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathAnimationManager"/> class.
        /// </summary>
        public DeathAnimationManager()
        {
            this.Frames = new List<DeathAnimationFrame>();
            this.initializeDeathSprites();
        }

        private void initializeDeathSprites()
        {
            this.Frames.Add(new DeathAnimationFrame(new DeathSprite1()));
            this.Frames.Add(new DeathAnimationFrame(new DeathSprite2()));
            this.Frames.Add(new DeathAnimationFrame(new DeathSprite3()));
            this.Frames.Add(new DeathAnimationFrame(new DeathSprite4()));
        }

        public void playNextFrame(int frameIndex)
        {
            this.Frames[frameIndex].SetFrameVisibility(Visibility.Visible);
        }

        public void hideCurrentFrame(int frameIndex)
        {
            this.Frames[frameIndex].SetFrameVisibility(Visibility.Collapsed);
        }
    }
}
