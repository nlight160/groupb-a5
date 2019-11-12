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

        private readonly IList<AnimationFrame> frames;
        private int frameIndex;
        private DispatcherTimer deathAnimationTimer;

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
            this.frameIndex = 0;
            this.setupAnimationTimer();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
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
        /// <param name="index">Index of the frame.</param>
        /// <exception cref="ArgumentOutOfRangeException">frameIndex</exception>
        public void PlayNextFrame(int index)
        {
            if (index < 0 || index > this.frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this.frames[index].Sprite.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Hides the current frame.
        ///     Precondition: frameIndex is within range
        ///     Postcondition: frame at index is hidden
        /// </summary>
        /// <param name="index">Index of the frame.</param>
        /// <exception cref="ArgumentOutOfRangeException">frameIndex</exception>
        public void HideCurrentFrame(int index)
        {
            if (index < 0 || index > this.frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this.frames[index].Sprite.Visibility = Visibility.Collapsed;
        }

        private void setupAnimationTimer()
        {
            this.deathAnimationTimer = new DispatcherTimer();
            this.deathAnimationTimer.Tick += this.animationTimerTick;
            this.deathAnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void animationTimerTick(object sender, object e)
        {
            if (this.frameIndex > 0)
            {
                this.HideCurrentFrame(this.frameIndex - 1);
            }

            if (this.frameIndex == this.frames.Count)
            {
                this.handleStopAnimation();
            }
            else
            {
                this.PlayNextFrame(this.frameIndex);
                this.frameIndex++;
            }
        }

        /// <summary>
        ///     Determines whether [is death animation running].
        ///     Precondition: none
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is death animation running]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDeathAnimationRunning()
        {
            return this.deathAnimationTimer.IsEnabled;
        }

        private void handleStopAnimation()
        {
            this.deathAnimationTimer.Stop();
            this.frameIndex = 0;
        }

        /// <summary>
        ///     Starts the death animation timer.
        ///     Precondition: none
        ///     Postcondition: death animation timer begins
        /// </summary>
        public void StartDeathAnimationTimer()
        {
            this.deathAnimationTimer.Start();
        }

        #endregion
    }
}