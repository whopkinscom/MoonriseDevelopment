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

using Moonrise.Utils.Standard.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Moonrise.Utils.Standard.Validation
{
    /// <summary>
    ///     Validates an email address
    /// </summary>
    /// <remarks>There is NO CHECK on the existence of the domain, just the format is a basic alphaNumeric@alphaNumeric.alphaNumeric!</remarks>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field)]
    public class EmailValidationAttribute : ValidationAttribute
    {
        /// <summary>
        ///     Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class. </returns>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!(value is string))
            {
                throw new ValidationException($"The {nameof(EmailValidationAttribute)} is only valid for use on strings!");
            }

            string strVal = value as string;

            ValidationResult retVal = ValidationResult.Success;

            if (!strVal.Contains("@") && !strVal.Extract("@").Contains("."))
            {
                retVal = new ValidationResult(ErrorMessage);
            }

            return retVal;
        }
    }
}