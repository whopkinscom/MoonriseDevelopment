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

namespace Moonrise.Utils.Standard
{
    /// <summary>
    ///     An interface that implements <see cref="IRequire" /> is an interface that has been declared to indicate a cohesive
    ///     set of functionality that is required by a particular class. Typically that class will then define a nested class
    ///     that contains instances of each required interface, with the constructor for the class taking in the nested class
    ///     that contains all of the instances it requires to do its job. Now there's nothing new about this at all and
    ///     <see cref="IRequire" /> has just been created simply to indicate that an interface is a required interface.
    ///     <para>
    ///         An <see cref="IRequire" /> has no definition BEYOND indicating that the interface is from this pattern.
    ///     </para>
    /// </summary>
    public interface IRequire
    {
    }
}