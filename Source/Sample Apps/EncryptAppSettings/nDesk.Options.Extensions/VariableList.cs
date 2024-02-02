#region MIT

//     Copyright 2015-2021 Will Hopkins - Moonrise Media Ltd.
//     will@moonrise.media - Happy to have a conversation
// 
//     Licenced under MIT licencing terms
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
// 
//         https://licenses.nuget.org/MIT
// 
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.

#endregion

using System.Collections;
using System.Collections.Generic;

namespace NDesk.Options.Extensions
{
    /// <summary>
    ///     VariableList OptionItemBase class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VariableList<T> : OptionItemBase<T>, IEnumerable<T>
    {
        //TODO: Might could transform this into an observable collection.
        /// <summary>
        ///     Values backing field.
        /// </summary>
        private readonly List<T> _values = new List<T>();

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        public VariableList(string prototype)
            : base(prototype)
        {
        }

        /// <summary>
        ///     Gets the ValuesList for internal consumption.
        /// </summary>
        internal List<T> ValuesList => _values;

        /// <summary>
        ///     Gets the Values.
        /// </summary>
        public IEnumerable<T> Values => _values;

        #region Enumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}