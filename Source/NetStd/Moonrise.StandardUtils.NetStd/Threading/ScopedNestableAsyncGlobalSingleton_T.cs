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
using System.Reflection;
using System.Threading;

namespace Moonrise.Utils.Standard.Threading
{
    /// <summary>
    ///     Provides scoped, nestable, async global values.
    ///     <para>
    ///         Scoped because any call to get the value (via a static) that occurs somewhere INSIDE the using scope will get
    ///         that value.
    ///     </para>
    ///     <para>
    ///         Nestable because if you open another scope (through an interior/nested using) then THAT becomes the value
    ///         anything inside of THAT scope will receive whereas once outside of THAT using scope the value for the PREVIOUS
    ///         scope is the static value.
    ///     </para>
    ///     <para>
    ///         Async because a <see cref="AsyncLocal{T}" /> is used as the backing store and so each scopes within different
    ///         async flows are just for that async flow.
    ///     </para>
    ///     <para>
    ///         Global because it's sort of acting like a global variable!
    ///     </para>
    ///     <para>
    ///         Another way of thinking about this class is that it is a smuggler. It can smuggle values way down into call
    ///         heirarchies without you needing to retrofit paramters to pass to each call. You know the way you can use class
    ///         variables for temporary working purposes without them being true properties/attributes of that class (from the
    ///         design rather than language persepective here)? Well, a <see cref="ScopedNestableAsyncGlobalSingleton{T}" />
    ///         is really the same thing, but for a thread. Kinda!
    ///     </para>
    ///     <remarks>
    ///         <para>
    ///             Usage:
    ///             <para>
    ///                 public class SUT : NestableAsyncGlobalSingleton&lt;string&gt;{public SUT(string value) :
    ///                 base(value){}
    ///             </para>
    ///         </para>
    ///     </remarks>
    ///     <example>
    ///         Wrap any significant "outer code" with
    ///         <para>
    ///             using (new SUT("value")) { YOUR CODE }
    ///         </para>
    ///         Then anywhere, even deep, within YOUR CODE you can get the current nested, threaded global value via
    ///         SUT.CurrentValue().
    ///         <para>
    ///             NOTE: IF you create an instance of the derived class via a function - perhaps because you need to do some
    ///             common thing (like grab some info from the HttpContext etc) then IF you do any async calls within that
    ///             function the wrong async context will be used. You must instead call the await in the arguments to your
    ///             constructor and do that async work BEFORE constructing the derived class.
    ///         </para>
    ///     </example>
    /// </summary>
    /// <typeparam name="T">The type of the singelton</typeparam>
    /// <seealso cref="System.IDisposable" />
    public abstract class ScopedNestableAsyncGlobalSingleton<T> : IDisposable
    {
        /// <summary>
        ///     The current "thing". This is stored on a per async flow basis.
        /// </summary>
        private static readonly AsyncLocal<ScopedNestableAsyncGlobalSingleton<T>> AsyncedCurrentGlobal = new();

        /// <summary>
        ///     Gets the current Nestable Thread Global Singleton value. If not already set this will be the default for generic
        ///     type.
        /// </summary>
        public static T CurrentValue => Current != null ? Current.Value : default(T);

        /// <summary>
        ///     The previous NestableThreadGlobalSingleton. This allows us to nest scopes, should we so desire.
        /// </summary>
        protected ScopedNestableAsyncGlobalSingleton<T> Previous { get; }

        /// <summary>
        ///     Gets the current <see cref="ScopedNestableAsyncGlobalSingleton{T}" />
        /// </summary>
        protected static ScopedNestableAsyncGlobalSingleton<T> Current
        {
            get => AsyncedCurrentGlobal.Value;

            set => AsyncedCurrentGlobal.Value = value;
        }

        /// <summary>
        ///     The nested global threaded value
        /// </summary>
        protected T Value { get; set; }

        /// <summary>
        ///     Static initialisation for the <see cref="ScopedNestableAsyncGlobalSingleton{T}" /> class.
        /// </summary>
        static ScopedNestableAsyncGlobalSingleton()
        {
            AsyncedCurrentGlobal = new AsyncLocal<ScopedNestableAsyncGlobalSingleton<T>>(null);
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="ScopedNestableAsyncGlobalSingleton{T}" /> class from being created.
        ///     However we do need to be create one to initially populate the <see cref="ThreadLocal{T}" />
        /// </summary>
        private ScopedNestableAsyncGlobalSingleton()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScopedNestableAsyncGlobalSingleton{T}" /> class.
        /// </summary>
        /// <param name="value">The value which will be the current NestedAsyncGlobal value.</param>
        protected ScopedNestableAsyncGlobalSingleton(T value)
        {
            Previous = Current;
            Current = this;
            Value = value;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Inform any child implementations that we are disposing
            Disposing();

            if (AsyncedCurrentGlobal.Value != this)
            {
                throw new AmbiguousMatchException("The NestableAsyncGlobalSingleton<T> being disposed SHOULD be the current async flow static one, but for some reason isn't!");
            }

            AsyncedCurrentGlobal.Value = Previous;
        }

        /// <summary>
        ///     Indicates that the current <see cref="ScopedNestableAsyncGlobalSingleton{T}" /> is being disposed. Override this
        ///     to take
        ///     additional actions.
        /// </summary>
        protected virtual void Disposing()
        {
        }
    }
}
