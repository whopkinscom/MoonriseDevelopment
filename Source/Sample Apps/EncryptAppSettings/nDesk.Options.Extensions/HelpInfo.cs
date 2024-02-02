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
    /// <summary>
    ///     HelpInfo is a decorator that handles a Boolean-Variable.
    /// </summary>
    public class HelpInfo
    {
        /// <summary>
        ///     Help backing field.
        /// </summary>
        private readonly Switch _help;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="optionSet">An OptionSet.</param>
        /// <param name="prototype">The HelpPrototype.</param>
        /// <param name="description"></param>
        internal HelpInfo(OptionSet optionSet, string prototype, string description = null)
        {
            string variablePrototype = prototype;
            _help = optionSet.AddSwitch(variablePrototype, description);
        }

        /// <summary>
        ///     Gets the Help.
        /// </summary>
        public Switch Help => _help;
    }
}