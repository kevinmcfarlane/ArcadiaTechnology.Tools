using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// General utility methods.
    /// </summary>
    public static class General
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Finds the emails in a delimited string of email addresses.
        /// </summary>
        /// <remarks>
        /// The emails can be delimited using ' ', ';', or ',', e.g.,
        /// <para>
        /// "kip@abc.com; joe.bloggs@mac.com ,  bob.jones@xyz.net, ef@abc.co.uk;a.jones@recfirst.com"
        /// </para>
        /// Invalid emails are skipped.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="addressList"/> is <c>null</c>.</exception>
        /// <param name="addressList">The email address list, e.g., "kip@abc.com; joe.bloggs@mac.com ,  bob.jones@xyz.net, ef@abc.co.uk;a.jones@recfirst.com".</param>
        /// <returns>Returns an array of email addresses or an empty array if none found.</returns>
        public static string[] FindEmails(string addressList)
        {
            if (addressList == null)
                throw new ArgumentNullException("addressList", "addressList is null.");

            // Extract emails, skipping blank entries
            var emails = addressList.Split(new char[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve the valid emails.
            var result = emails.Where(e => ValidationTool.IsValidEmail(e)).ToArray();

            return result;
        }

        /// <summary>
        /// Splits an email address list into separate email addresses.
        /// </summary>
        /// <remarks>
        /// Splits a comma, or semi-colon, delimited list of email addresses into separate valid and invalid email addresses.
        /// The list may also contain any number of empty spaces around the email addresses.
        /// </remarks>
        public static void SplitEmails()
        {
            var addressList = "kip@abc; joe.bloggs@mac.com ,  bob.jones@xyz.net, abc.co.uk;a.jones@recfirst.com";

            // Extract emails, skipping blank entries
            var emails = addressList.Split(new char[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Print the valid emails.
            var validEmails = emails.Where(e => ValidationTool.IsValidEmail(e)).ToList();
            validEmails.ForEach(e => Console.WriteLine("Valid Email = " + e));

            // Print the invalid emails
            var invalidEmails = emails.Where(e => ValidationTool.IsInvalidEmail(e)).ToList();
            invalidEmails.ForEach(e => Console.WriteLine("Invalid Email = " + e));
        }

        /// <summary>
        /// Splits a pascal or camel case string into a string of words separated by spaces.
        /// </summary>
        /// <param name="source">The source pascal or camel case string.</param>
        /// <returns>A copy of the original string with each word separated by a space.</returns>
        /// <remarks>
        /// This matches all capital letters, replaces them with a space and the letter we found ($1),
        /// then trims the result to remove the initial space if there was a capital letter at the beginning.
        /// </remarks>
        /// <example>
        /// <c>MyLeftFoot</c> becomes <c>My Left Foot</c>.
        /// </example>
        public static string SplitPascalOrCamelCase(string source)
        {
            string result =
                Regex.Replace(source, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

            return result;
        }
        #endregion Methods
    }
}