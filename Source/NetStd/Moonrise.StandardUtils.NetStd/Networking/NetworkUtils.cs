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

using System.Net;
using System.Net.Sockets;

namespace Moonrise.Utils.Standard.Networking
{
    /// <summary>
    ///     Utility methods for doing various network related stuff
    /// </summary>
    public class NetworkUtils
    {
        /// <summary>
        ///     Determines whether the specified address is an IP address. NOTE: Prefixes, such as "http:\\" are NOT allowed.
        /// </summary>
        /// <param name="addr">The address string to check.</param>
        /// <param name="checkFullDotNotation">
        ///     Determines if the string uses dot notation, as by default "5" would be treated as
        ///     "000.000.000.005"! - ONLY CURRENTLY VALID FOR IPv4!
        /// </param>
        /// <param name="allowPort">Determines if a port number can be on the end - ONLY CURRENTLY VALID FOR IPv4!</param>
        /// <returns>
        ///     True or False
        /// </returns>
        public static bool IsIPAddress(string addr, bool checkFullDotNotation = false, bool allowPort = true)
        {
            bool retVal = true;

            // 3 or more colons indicates a likely ipV6 address!
            if (allowPort && addr.Contains(":") && addr.Split(':').Length < 3)
            {
                int colonPos = addr.LastIndexOf(":");

                if (colonPos < addr.Length - 2)
                {
                    string port = addr.Substring(colonPos + 1);
                    int ignore;

                    retVal = int.TryParse(port, out ignore);
                }
                else
                {
                    retVal = false;
                }

                addr = addr.Substring(0, colonPos);
            }

            if (checkFullDotNotation)
            {
                retVal = addr.Split('.').Length == 4;
            }

            if (retVal)
            {
                retVal = false;
                IPAddress ip;

                if (IPAddress.TryParse(addr, out ip))
                {
                    if (ip.AddressFamily == AddressFamily.InterNetworkV6
                        || ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        retVal = true;
                    }
                }
            }

            return retVal;
        }
    }
}