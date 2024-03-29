﻿#region MIT

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
using Microsoft.Extensions.Configuration;

namespace Moonrise.Microsoft.Extensions.Configuration.EncryptedJsonConfiguration
{
    /// <summary>
    ///     Hosts an extension method for <see cref="IConfigurationBuilder" />.
    /// </summary>
    public static class EncyptedJsonConfigurationExtensions
    {
        /// <summary>
        ///     Adds a potentially encypted JSON file as a configuration source.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="path">The path to the JSON file.</param>
        /// <param name="optional">if set to <c>true</c> [optional].</param>
        /// <param name="reloadOnChange">if set to <c>true</c> [reload on change].</param>
        /// <returns>
        ///     <see cref="builder" />
        /// </returns>
        /// <exception cref="ArgumentNullException">builder</exception>
        /// <exception cref="ArgumentException">File path must be a non-empty string.</exception>
        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder,
            string path,
            bool optional,
            bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            EncyptedJsonConfigurationSource source = new EncyptedJsonConfigurationSource
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }
}