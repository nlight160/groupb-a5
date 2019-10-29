using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public class FrogHomeManager : IEnumerable
    {
        private ICollection<FrogHome> homes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrogHomeManager"/> class.
        /// </summary>
        public FrogHomeManager()
        {
            this.homes = new Collection<FrogHome>();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var frogHome in this.homes)
            {
                yield return frogHome;
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

        /// <summary>
        /// Sets the frog homes.
        /// </summary>
        public void SetFrogHomes()
        {
            var frogHomeGap = 150;
            var nextX = 0;
            foreach (var frogHome in this.homes)
            {
                frogHome.X = nextX;
                frogHome.Y = frogHome.Height;
                nextX += frogHomeGap;
            }
        }

    }
}
