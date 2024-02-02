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
    ///     OptionSetExtensions class.
    /// </summary>
    public static class OptionSetExtensions
    {
        /// <summary>
        ///     Adds a Switch to the OptionSet.
        /// </summary>
        /// <typeparam name="TOptionSet">Any derived OptionSet.</typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns>The Switch associated with the OptionSet.</returns>
        public static Switch AddSwitch<TOptionSet>(this TOptionSet optionSet, string prototype, string description)
            where TOptionSet : OptionSet
        {
            /* Switch and not a flag. Switch implies on or off, enabled or disabled,
             * whereas flag implies combinations, masking. */
            Switch @switch = new Switch();

            //Which leaves us injecting a hook into the options.
            optionSet.Add(prototype, description, x => @switch.Enabled = !string.IsNullOrEmpty(x));

            //Kinda like having a future, not quite, but real close.
            return @switch;
        }

        /// <summary>
        ///     Adds a strongly typed Variable to the OptionSet.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns>The Variable associated with the OptionSet.</returns>
        public static Variable<TVariable> AddVariable<TVariable>(this OptionSet optionSet,
            string prototype, string description = null)
        {
            // We pass an empty method to the addedEventHandler because we don't need anything extra.
            return AddVariable<TVariable>(optionSet, prototype, _ => { }, description);
        }

        /// <summary>
        ///     Adds a strongly typed Variable to the OptionSet with the option to execute arbitrary
        ///     code when options are parsed.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="onAdded">Allows execution of arbitrary code when an option is parsed.</param>
        /// <param name="description"></param>
        /// <returns>The Variable associated with the OptionSet.</returns>
        internal static Variable<TVariable> AddVariable<TVariable>(this OptionSet optionSet,
            string prototype, Action<string> onAdded, string description = null)
        {
            string variablePrototype = prototype + "=";

            Variable<TVariable> variable = new Variable<TVariable>(variablePrototype);

            optionSet.Add(variablePrototype, description, x =>
            {
                variable.Value = Variable<TVariable>.CastString(x);
                // Perform whatever our downstream callers need to do when an option is parsed.
                onAdded(variablePrototype);
            });

            return variable;
        }

        /// <summary>
        ///     Accumulates option values in a strongly-typed Variable list.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <returns>The VariableList associated with the OptionSet.</returns>
        public static VariableList<TVariable> AddVariableList<TVariable>(this OptionSet optionSet,
            string prototype, string description = null)
        {
            // We pass nothing to the addedEventHandler because we don't need anything extra.
            return AddVariableList<TVariable>(optionSet, prototype, none => { }, description);
        }

        /// <summary>
        ///     Accumulates option values in a strongly-typed Variable list with the option to execute
        ///     arbitrary code when options are parsed.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="onAdded">Allows execution of arbitrary code when an option is parsed.</param>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <returns>The VariableList associated with the OptionSet.</returns>
        internal static VariableList<TVariable> AddVariableList<TVariable>(this OptionSet optionSet,
            string prototype, Action<string> onAdded, string description = null)
        {
            string variablePrototype = prototype + "=";
            VariableList<TVariable> variable = new VariableList<TVariable>(variablePrototype);
            optionSet.Add(variablePrototype, description ?? string.Empty, x =>
            {
// ReSharper disable InconsistentNaming
                TVariable x_Value = Variable<TVariable>.CastString(x);
// ReSharper restore InconsistentNaming
                variable.ValuesList.Add(x_Value);

                // Perform whatever our downstream callers need to do when an option is parsed.
                onAdded(variablePrototype);
            });
            return variable;
        }

        /// <summary>
        ///     Accumulates options into a strongly-typed VariableMatrix.
        /// </summary>
        /// <typeparam name="TVariable"></typeparam>
        /// <param name="optionSet"></param>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns>The VariableMatrix associated with the OptionSet.</returns>
        public static VariableMatrix<TVariable> AddVariableMatrix<TVariable>(this OptionSet optionSet,
            string prototype, string description = null)
        {
            string variablePrototype = prototype + ":";

            VariableMatrix<TVariable> variable = new VariableMatrix<TVariable>(variablePrototype);

            //Key/value pairs are indeed parsed out of the args list.
            optionSet.Add(variablePrototype, description ?? string.Empty, (k, x) =>
            {
                if (string.IsNullOrEmpty(k))
                {
                    throw new OptionException("Name not specified", variablePrototype);
                }

                // ReSharper disable InconsistentNaming
                TVariable x_Value = Variable<TVariable>.CastString(x);
// ReSharper restore InconsistentNaming

                //Utilize the InternalMatrix for purposes of this one.
                variable.InternalMatrix.Add(k, x_Value);
            });

            return variable;
        }
    }
}