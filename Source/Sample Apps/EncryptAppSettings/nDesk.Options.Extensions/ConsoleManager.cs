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
using System.IO;
using System.Linq;
using NDesk.Options.Core;

namespace NDesk.Options.Extensions
{
    /// <summary>
    ///     ConsoleManager class.
    /// </summary>
    public class ConsoleManager
    {
        /// <summary>
        ///     HelpInfo backing field.
        /// </summary>
        private readonly HelpInfo _helpInfo;

        /// <summary>
        ///     OptionSet backing field.
        /// </summary>
        private readonly RequiredValuesOptionSet _optionSet;

        /// <summary>
        ///     Requirements backin field.
        /// </summary>
        private readonly List<Requirement> _requirements = new List<Requirement>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleManager" />
        ///     class using a specified value for HelpInfo prototype and description.
        /// </summary>
        /// <param name="consoleName">Name of the console.</param>
        /// <param name="optionSet">An OptionSet.</param>
        /// <param name="helpPrototype">The Help prototype. Informs a Switch with its Prototype.</param>
        /// <param name="helpDescription">The help description.</param>
        public ConsoleManager(string consoleName, RequiredValuesOptionSet optionSet, string helpPrototype = "?",
            string helpDescription = "Show the help")
            : this(optionSet, consoleName, new HelpInfo(optionSet, helpPrototype, helpDescription))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleManager" /> class.
        /// </summary>
        /// <param name="optionSet">An OptionSet.</param>
        /// <param name="consoleName">Name of the console.</param>
        /// <param name="helpInfo">The help info.</param>
        private ConsoleManager(RequiredValuesOptionSet optionSet, string consoleName, HelpInfo helpInfo)
        {
            ConsoleName = consoleName;
            _optionSet = optionSet;
            _helpInfo = helpInfo;
        }

        //TODO: TBD: Could potentially be an observable collection.
        /// <summary>
        ///     Gets the Requirements.
        /// </summary>
        public List<Requirement> Requirements => _requirements;

        /// <summary>
        ///     Gets the ConsoleName.
        /// </summary>
        internal string ConsoleName { get; }

        /// <summary>
        ///     Parses the Command-Line Args or Shows the Help, whichever is appropriate.
        ///     Appropriateness is determined by whether there are Remaining Args, or
        ///     whether the Help option was specified.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="args"></param>
        /// <returns>true when parsing was successful and no help was requested.</returns>
        /// <remarks>
        ///     Which, by simplifying the model in SOLID-, DRY-style, the need
        ///     for many in the way of helpers vanishes altogether.
        /// </remarks>
        public bool TryParseOrShowHelp(TextWriter writer, params string[] args)
        {
            List<string> remaining = _optionSet.Parse(args);

            //Not-parsed determined here.
            bool parsed = !(remaining.Any()
                            || _optionSet.GetMissingVariables().Any()
                            || _helpInfo.Help.Enabled);

            //Show-error when any-remaining or missing-variables.
            if (remaining.Any() || _optionSet.GetMissingVariables().Any())
            {
                writer.WriteLine("{0}: error parsing arguments:", ConsoleName);
            }
            else if (!parsed)
            {
                writer.WriteLine("{0} options:", ConsoleName);
            }

            //Show-help when not-parsed.
            if (!parsed)
            {
                _optionSet.WriteOptionDescriptions(writer);
            }

            return parsed;
        }
    }
}