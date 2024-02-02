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

namespace NDesk.Options.Core
{
    /// <summary>
    ///     Requirement class.
    /// </summary>
    public class Requirement
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public Requirement()
        {
            //Such that we do not leave the function open.
            IsRequirementSatisfied = () => false;
        }

        /// <summary>
        ///     Gets or sets a function indicating whether IsRequirementSatisfied.
        /// </summary>
        public Func<bool> IsRequirementSatisfied { get; set; }

        /// <summary>
        ///     Gets or sets the Text.
        /// </summary>
        public string[] Text { get; set; }
    }
}