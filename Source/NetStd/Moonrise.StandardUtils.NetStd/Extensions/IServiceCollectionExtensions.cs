#region MIT

// Copyright 2015-2025 Will Hopkins - Moonrise Media Ltd.
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

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Moonrise.Utils.Standard.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds injectible configuration from the <see cref="IConfiguration"/> source - essentially appsettings.json.
        /// </summary>
        /// <remarks>
        /// This allows us to break the IOptions dependency on consuming classes and thus a greater separation of concerns.
        /// </remarks>
        /// <typeparam name="TConfig">The configuration type</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> instance being extended</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> provider</param>
        /// <param name="sectionName">The section name where the config data is located</param>
        /// <returns>The <see cref="services"/></returns>
        public static IServiceCollection AddConfiguration<TConfig>(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionName) where TConfig : class, new()
        {
            object configLock = new();

            // Register the configuration object as a singleton
            services.AddSingleton(provider =>
            {
                TConfig config = new ();
                IConfigurationSection configSection = configuration.GetSection(sectionName);
                configSection.Bind(config);

                // Monitor for changes
                ChangeToken.OnChange(
                    () => configSection.GetReloadToken(),
                    () =>
                    {
                        lock (configLock)
                        {
                            configSection.Bind(config);
                        }
                    });

                return config;
            });

            return services;
        }
    }
}