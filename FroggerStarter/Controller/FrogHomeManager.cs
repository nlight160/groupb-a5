using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public class FrogHomeManager : IEnumerable
    {
        #region Data members

        private readonly int frogHomeOffset;
        private readonly ICollection<FrogHome> homes;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FrogHomeManager" /> class.
        ///     Precondition: none
        ///     Postcondition: A new Frog home manager is created
        /// </summary>
        public FrogHomeManager(int frogHomeOffset)
        {
            this.homes = new Collection<FrogHome>();
            this.frogHomeOffset = frogHomeOffset;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var frogHome in this.homes)
            {
                yield return frogHome;
            }
        }

        /// <summary>
        ///     Adds the frog home.
        ///     Precondition: home != null
        ///     Postcondition: home added to homes
        /// </summary>
        /// <param name="home">The home.</param>
        /// <exception cref="ArgumentNullException">home</exception>
        public void AddFrogHome(FrogHome home)
        {
            if (home == null)
            {
                throw new ArgumentNullException(nameof(home));
            }

            this.homes.Add(home);
        }

        /// <summary>
        ///     Sets the frog homes.
        ///     Precondition: none
        ///     Postcondition: frog homes are set centered and spaced equally at goal
        /// </summary>
        public void SetFrogHomes()
        {
            var nextX = 0;
            foreach (var frogHome in this.homes)
            {
                frogHome.X = nextX;
                frogHome.Y = frogHome.Height;
                nextX += this.frogHomeOffset;
            }
        }

        #endregion
    }
}