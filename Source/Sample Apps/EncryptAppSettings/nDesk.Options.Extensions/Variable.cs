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

namespace NDesk.Options.Extensions
{
    /* TODO: Consider what to do with Variable, add RequiredVariable,
     * or does that mix with the Requirement class? */
    /// <summary>
    ///     Variable OptionItemBase class.
    ///     The last known Variable Value is recalled.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Variable<T> : OptionItemBase<T>
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="prototype">The name of the Variable.</param>
        public Variable(string prototype)
            : base(prototype)
        {
            Value = default;
        }

        /// <summary>
        ///     Gets the Value.
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        ///     Implicitly converts the Variable value.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static implicit operator T(Variable<T> variable)
        {
            return variable.Value;
        }
    }
}