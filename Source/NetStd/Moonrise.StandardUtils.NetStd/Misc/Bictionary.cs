using System.Collections.Generic;
using System.Linq;

namespace Moonrise.Utils.Standard.Misc
{
    /// <summary>
    /// A BiDirectional Dictionary where you KNOW each element (key & value) are unique within their own types.
    /// </summary>
    /// <remarks>
    /// This is intended to be used for relatively (less than say 100) entries as the reverse index uses an Any() scan.
    /// </remarks>
    /// With thanks to https://stackoverflow.com/questions/10966331/two-way-bidirectional-dictionary-in-c
    /// <typeparam name="T1">The first type</typeparam>
    /// <typeparam name="T2">The second type</typeparam>
    /// <seealso cref="System.Collections.Generic.Dictionary&lt;T1, T2&gt;" />
    public class Bictionary<T1, T2> : Dictionary<T1, T2>
    {
        /// <summary>
        /// Allows for the "reverse" index of a dictionary.
        /// </summary>
        /// <value>
        /// The <see cref="T1" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public T1 this[T2 index]
        {
            get
            {
                bool found = false;

                // KeyValuePair is a struct and doesn't really have a default as such. However this technique of setting a boolean
                // which will create a bool value for the && but will only be executed if the first bit is true, is a good way to
                // not have to do a double pass through the dictionary using .Any and then again with a .Where.
                KeyValuePair<T1, T2> keyValue = this.FirstOrDefault(x => x.Value.Equals(index) && (found = true));

                if (!found)
                {
                    throw new KeyNotFoundException();
                }

                return keyValue.Key;
            }
        }
    }
}
