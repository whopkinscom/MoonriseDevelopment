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
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace NDesk.Options.Extensions
{
    /// <summary>
    ///     VariableMatrix OptionItemBase class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VariableMatrix<T> : OptionItemBase<T>, IDictionary<string, T>
    {
        /* TODO: .NET 4.5 introduces the notion of ReadOnlyDictionary.
         * In prior framework versions we would need to roll our own. */
        //TODO: Might could enrich the model by implementing our own custom Dictionary.
        /// <summary>
        ///     Matrix backing field.
        /// </summary>
        private readonly IDictionary<string, T> _matrix = new Dictionary<string, T>();

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        public VariableMatrix(string prototype)
            : base(prototype)
        {
        }

        /// <summary>
        ///     Gets the Matrix.
        /// </summary>
        internal IDictionary<string, T> InternalMatrix => _matrix;

        /* TODO: Approaching .NET 4.5, use the IReadOnlyDictionary, or
         * potentially roll our own for prior .NET framework versions. */
        /// <summary>
        ///     Gets the Matrix.
        /// </summary>
        public IDictionary<string, T> Matrix => _matrix;

        #region Dictionary Members

        /// <summary>
        ///     Throws that the VariableMatrix IsReadOnly.
        /// </summary>
        /// <exception cref="ReadOnlyException"></exception>
        private static void ThrowReadOnly()
        {
            throw new ReadOnlyException("VariableMatrix is readonly.");
        }

        /// <summary>
        ///     Throws that the VariableMatrix IsReadOnly.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>Returns a value to keep the compiler happy.</returns>
        /// <exception cref="ReadOnlyException"></exception>
        private static TResult ThrowReadOnly<TResult>()
        {
            ThrowReadOnly();

            //To keep the compiler happy.
            return default;
        }

        /// <summary>
        ///     Runs the Dictionary Action on the Matrix.
        /// </summary>
        /// <param name="action"></param>
        private void DictionaryAction(Action<IDictionary<string, T>> action)
        {
            action(Matrix);
        }

        /// <summary>
        ///     Runs the Dictionary Func on the Matrix.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private TResult DictionaryFunc<TResult>(Func<IDictionary<string, T>, TResult> func)
        {
            return func(Matrix);
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return DictionaryFunc(x => x.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, T> item)
        {
            ThrowReadOnly();
        }

        public void Clear()
        {
            ThrowReadOnly();
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return DictionaryFunc(x => x.Contains(item));
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            DictionaryAction(x => x.CopyTo(array, arrayIndex));
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            //Keep the compiler happy, however, still it is readonly.
            return ThrowReadOnly<bool>();
        }

        public int Count
        {
            get { return DictionaryFunc(x => x.Count); }
        }

        public bool IsReadOnly => true;

        public bool ContainsKey(string key)
        {
            return DictionaryFunc(x => x.ContainsKey(key));
        }

        public void Add(string key, T value)
        {
            ThrowReadOnly();
        }

        public bool Remove(string key)
        {
            return ThrowReadOnly<bool>();
        }

        public bool TryGetValue(string key, out T value)
        {
            T local = default(T);
            bool result = DictionaryFunc(x => x.TryGetValue(key, out local));
            value = local;
            return result;
        }

        public T this[string key]
        {
            get { return DictionaryFunc(x => x[key]); }
            set
            {
                T local = value;
                ThrowReadOnly();
            }
        }

        public ICollection<string> Keys
        {
            get { return DictionaryFunc(x => x.Keys); }
        }

        public ICollection<T> Values
        {
            get { return DictionaryFunc(x => x.Values); }
        }

        #endregion
    }
}