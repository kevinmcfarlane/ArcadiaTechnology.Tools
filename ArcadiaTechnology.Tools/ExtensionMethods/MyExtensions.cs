using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadiaTechnology.Tools.ExtensionMethods
{
    /// <summary>
    /// Extension Methods.
    /// </summary>
    /// <remarks>Note: this method is now part of <see cref="String"/> in .NET 4.0 as <c>IsNullOrWhiteSpace</c>.</remarks>
    public static class MyExtensions
    {
        /// <summary>
        /// Indicates whether the specified string is null or empty or a string containing only spaces.
        /// </summary>
        /// <param name="s">A <see cref="String"/> reference.</param>
        /// <returns>
        /// <c>true</c> if the string is null or empty or blank; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmptyOrBlank(this string s)
        {
            return String.IsNullOrEmpty(s) || s.Trim().Length == 0;
        }
    }
}
