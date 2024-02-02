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

using System;

namespace NDesk.Options.Extensions
{
    /// <summary>
    ///     OptionItemBase class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OptionItemBase<T>
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        protected OptionItemBase(string prototype)
        {
            Prototype = prototype;
        }

        /// <summary>
        ///     Gets the Prototype.
        /// </summary>
        protected string Prototype { get; }

        /// <summary>
        ///     Casts the Value string to the Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>Note that this is a hard cast.</remarks>
        protected internal static T CastString(string value)
        {
            //TODO: Might provide a hook for more explicit-user-provided string conversions.
            return (T) Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        ///     Throws an OptionException with the Message and Prototype.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected OptionException ThrowOptionException(string message)
        {
            return new OptionException(message, Prototype);
        }
    }
}