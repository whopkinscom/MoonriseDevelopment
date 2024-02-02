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
using System.Collections.Generic;

namespace Moonrise.Utils.Standard.CSV
{
    /// <summary>
    ///     Indicates an exception whilst parsing a CSV file
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CsvParseException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CsvParseException" /> class.
        /// </summary>
        public CsvParseException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CsvParseException" /> class.
        /// </summary>
        /// <param name="rowNo">The row no.</param>
        /// <param name="rowContent">Content of the row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="exception">The exception.</param>
        public CsvParseException(int rowNo, string rowContent, string columnName, Exception exception)
        {
            RowNo = rowNo;
            RowContent = rowContent;
            ColumnName = columnName;
            Exception = exception;
        }

        /// <summary>
        ///     Collated exceptions. See <seealso cref="CsvParser{TTarget,TOverlay}.CollateExceptions" />
        /// </summary>
        public List<CsvParseException> CollatedExceptions { get; private set; }

        /// <summary>
        ///     The name of the column the exception occurred processing.
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        ///     The exception that occurred.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        ///     The content of the column the exception occurred processing.
        /// </summary>
        public string RowContent { get; }

        /// <summary>
        ///     The row number the exception occurred processing.
        /// </summary>
        public int RowNo { get; }

        /// <summary>
        ///     Adds the specified parse exception.
        /// </summary>
        /// <param name="parseException">The parse exception.</param>
        internal void Add(CsvParseException parseException)
        {
            if (CollatedExceptions == null)
            {
                CollatedExceptions = new List<CsvParseException>();
            }

            CollatedExceptions.Add(parseException);
        }
    }
}