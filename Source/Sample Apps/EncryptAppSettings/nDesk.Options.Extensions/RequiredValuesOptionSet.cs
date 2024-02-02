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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NDesk.Options.Extensions
{
    /// <summary>
    ///     Derives from OptionSet, and adds capability for variables that are required.
    /// </summary>
    /// <remarks>http://www.ndesk.org/doc/ndesk-options/NDesk.Options/OptionSet.html</remarks>
    public class RequiredValuesOptionSet : OptionSet
    {
        /// <summary>
        ///     Dictionary that holds whether or not prototype variables have been set
        /// </summary>
        private readonly Dictionary<string, bool> _requiredVariableValues = new Dictionary<string, bool>();

        public IEnumerable<Option> GetMissingVariables()
        {
            // get items in dictionary where there is no entry
            IEnumerable<Option> q = from t in _requiredVariableValues
                join o in this as KeyedCollection<string, Option> on t.Key equals o.Prototype
                where t.Value == false
                select o;

            return q;
        }

        /// <summary>
        ///     Adds the Required Variable to the OptionSet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Variable<T> AddRequiredVariable<T>(string prototype, string description = null)
        {
            _requiredVariableValues.Add(prototype + "=", false);
            return this.AddVariable<T>(prototype,
                variablePrototype => { _requiredVariableValues[variablePrototype] = true; }, description);
        }

        /// <summary>
        ///     Adds the Required VariableList to the OptionSet.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public VariableList<TVariable> AddRequiredVariableList<TVariable>(string prototype, string description = null)
        {
            _requiredVariableValues.Add(prototype + "=", false);
            return this.AddVariableList<TVariable>(prototype,
                variablePrototype => { _requiredVariableValues[variablePrototype] = true; }, description);
        }
    }
}