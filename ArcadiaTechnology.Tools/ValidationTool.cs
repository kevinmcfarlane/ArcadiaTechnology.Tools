using System;
using System.Text.RegularExpressions;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// General validation helpers.
    /// </summary>
    public static class ValidationTool
    {
        /// <summary>
        /// Is the specified email address valid?
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>
        /// <c>true</c> if valid email; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmail(string email)
        {
            string pattern =
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(pattern);

            return re.IsMatch(email);
        }

        /// <summary>
        /// Is the specified email address invalid?
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>
        /// 	<c>true</c> if invalid email; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInvalidEmail(string email)
        {
            return !IsValidEmail(email);
        }

        /// <summary>
        /// Indicates whether the specified string is null or empty or a string containing only spaces.
        /// </summary>
        /// <param name="arg">A String reference.</param>
        /// <returns>
        /// <c>true</c> if the string is null or empty or blank; otherwise, <c>false</c>.
        /// </returns>
        [Obsolete("This helper is no longer necessary since .NET introduced String.IsNullOrWhitespace method in .NET 4.")]
        public static bool IsNullOrEmptyOrBlank(string arg)
        {
            return String.IsNullOrEmpty(arg) || arg.Trim().Length == 0;
        }
    }
}