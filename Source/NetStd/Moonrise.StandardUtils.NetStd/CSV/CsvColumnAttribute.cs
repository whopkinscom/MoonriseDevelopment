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
    ///     Defines metadata about how to convert from CSV data into the attributed property.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvColumnAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CsvColumnAttribute" /> class. i.e. default takes the header name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public CsvColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        /// <summary>
        ///     The header name for a CSV column
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        ///     A custom converter.
        /// </summary>
        public Type Converter { get; set; } = null;
    }
}