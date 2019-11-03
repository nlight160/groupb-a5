using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using FroggerStarter.Model;
using FroggerStarter.View.Sprites.DeathFrames;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages the death frames to form an animation sequence
    /// </summary>
    public class AnimationManager : IEnumerable<AnimationFrame>
    {
        #region Data members

        private IList<AnimationFrame> frames;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnimationManager" /> class.
        ///     Precondition: none
        ///     Postcondition: A new death animation manager is created
        /// </summary>
        public AnimationManager()
        {
            this.frames = new List<AnimationFrame>();
            this.initializeDeathSprites();
        }

        #endregion

        #region Methods

        private void initializeDeathSprites()
        {
            this.frames.Add(new AnimationFrame(new DeathSprite1()));
            this.frames.Add(new AnimationFrame(new DeathSprite2()));
            this.frames.Add(new AnimationFrame(new DeathSprite3()));
            this.frames.Add(new AnimationFrame(new DeathSprite4()));
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
            if (frameIndex < 0 || frameIndex > this.frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(frameIndex));
            }

            this.frames[frameIndex].Sprite.Visibility = Visibility.Visible;
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
            if (frameIndex < 0 || frameIndex > this.frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(frameIndex));
            }

            this.frames[frameIndex].Sprite.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var animationFrame in this.frames)
            {
                yield return animationFrame;
            }
        }

        IEnumerator<AnimationFrame> IEnumerable<AnimationFrame>.GetEnumerator()
        {
            return this.frames.GetEnumerator();
        }


        #endregion
    }
}