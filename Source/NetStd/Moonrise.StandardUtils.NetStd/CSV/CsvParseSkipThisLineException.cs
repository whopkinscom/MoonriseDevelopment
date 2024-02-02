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

namespace Moonrise.Utils.Standard.CSV
{
    /// <summary>
    ///     Indicates that a condition has been detected whereby the entire line is to be skipped/discarded
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CsvParseSkipThisLineException : Exception
    {
    }
}