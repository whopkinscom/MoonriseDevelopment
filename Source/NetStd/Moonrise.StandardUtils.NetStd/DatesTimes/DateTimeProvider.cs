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
using System.Threading;

namespace Moonrise.Utils.Standard.DatesTimes
{
    /// <summary>
    ///     An interface for changing the time - H.G.Wells would be proud!
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        ///     What is considered to be "now"
        /// </summary>
        DateTime Now { get; }
    }

    /// <summary>
    ///     Provides a replaceable date time provider
    /// </summary>
    /// <seealso cref="IDateTimeProvider" />
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        ///     The per thread store of providers
        /// </summary>
        private static readonly ThreadLocal<IDateTimeProvider> Providers = new ThreadLocal<IDateTimeProvider>();

        /// <summary>
        ///     Gets the current DateTime
        /// </summary>
        public static DateTime Now => Provider.Now;

        /// <summary>
        ///     The provider of DateTimes
        /// </summary>
        public static IDateTimeProvider Provider
        {
            private get
            {
                if (Providers.Value == null)
                {
                    Providers.Value = new DateTimeProvider();
                }

                return Providers.Value;
            }

            set { Providers.Value = value; }
        }

        /// <summary>
        ///     Gets the normal now.
        /// </summary>
        DateTime IDateTimeProvider.Now => DateTime.Now;
    }
}