using System.Runtime.CompilerServices;

namespace Moonrise.Utils.Standard.Misc
{
    public static class FullName
    {
		/// <summary>
		/// Gets the full ClassName.MemberName.
		/// </summary>
		/// <remarks>
		/// Usage: Assert.AreEqual("Object.Equals", FullName.Of(nameof(Object.Equals)));
		/// </remarks>
		/// <param name="_">The </param>
		/// <param name="fullTypeName">Full name of the type.</param>
		/// <returns></returns>
		public static string Of(string _, [CallerArgumentExpression("_")] string fullTypeName = "")
        {
            if (fullTypeName.StartsWith("nameof(") && fullTypeName.EndsWith(")"))
            {
                return fullTypeName[7..^1];
            }

            return fullTypeName;
        }
    }
}
