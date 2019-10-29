using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using FroggerStarter.Model;
using FroggerStarter.View.Sprites.DeathFrames;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages the death frames to form an animation sequence
    /// </summary>
    public class AnimationManager
    {
        #region Properties

        /// <summary>
        ///     Gets the frames.
        /// </summary>
        /// <value>
        ///     The frames.
        /// </value>
        public IList<AnimationFrame> Frames { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationManager" /> class.
        ///     Precondition: none
        ///     Postcondition: A new death animation manager is created
        /// </summary>
        public AnimationManager()
        {
            this.Frames = new List<AnimationFrame>();
            this.initializeDeathSprites();
        }

        #endregion

        #region Methods

        private void initializeDeathSprites()
        {
            this.Frames.Add(new AnimationFrame(new DeathSprite1()));
            this.Frames.Add(new AnimationFrame(new DeathSprite2()));
            this.Frames.Add(new AnimationFrame(new DeathSprite3()));
            this.Frames.Add(new AnimationFrame(new DeathSprite4()));
        }

        /// <summary>
        ///     Plays the next frame.
        ///     Precondition: frameIndex is within range
        ///     Postcondition: frame at index is visible
        /// </summary>
        /// <param name="frameIndex">Index of the frame.</param>
        /// <exception cref="ArgumentOutOfRangeException">frameIndex</exception>
        public void PlayNextFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex > this.Frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(frameIndex));
            }

            this.Frames[frameIndex].Sprite.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Hides the current frame.
        ///     Precondition: frameIndex is within range
        ///     Postcondition: frame at index is hidden
        /// </summary>
        /// <param name="frameIndex">Index of the frame.</param>
        /// <exception cref="ArgumentOutOfRangeException">frameIndex</exception>
        public void HideCurrentFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex > this.Frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(frameIndex));
            }

            this.Frames[frameIndex].Sprite.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}