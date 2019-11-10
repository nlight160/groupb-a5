using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Extensions
{
    /// <summary>
    ///   <para>Extends List class</para>
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}
