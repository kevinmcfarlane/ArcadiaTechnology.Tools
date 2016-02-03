using System;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// Exception Helpers.
    /// </summary>
    public static class ExceptionTool
    {
        /// <summary>
        /// Formats the exception.
        /// </summary>
        /// <param name="ex">The exception instance.</param>
        /// <param name="newLineChars">The new line string representation.</param>
        /// <returns>The formatted exception.</returns>
        /// <remarks>
        /// The new line string could be, e.g., <c>Environment.NewLine</c>, "\n" or "&lt;br/&gt;" for HTML.
        /// </remarks>
        public static string FormatException(Exception ex, string newLineChars)
        {
            System.Diagnostics.Debug.Assert(ex != null, "Must have an exception instance.");

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(ex.GetType());
            builder.Append(newLineChars);
            builder.Append(ex.Message);
            builder.Append(newLineChars);
            builder.Append(newLineChars);

            Exception inner = ex.InnerException;

            while (inner != null)
            {
                builder.Append(inner.GetType());
                builder.Append(newLineChars);
                builder.Append(inner.Message);
                builder.Append(newLineChars);
                builder.Append(newLineChars);
                builder.Append(inner.StackTrace);
                builder.Append(newLineChars);
                builder.Append(" --- End of inner exception stack trace ---");
                builder.Append(newLineChars);
                builder.Append(newLineChars);

                inner = inner.InnerException;
            }

            builder.Append(ex.StackTrace);

            string result = builder.ToString();

            return result;
        }
    }
}