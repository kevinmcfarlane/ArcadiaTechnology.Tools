using System;
using System.Collections.Generic;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// Presents assertion (logic) errors as exceptions.
    /// </summary>
    /// <remarks>
    /// Can be used to replace existing <c>Debug.Assert</c> calls with <c>Check.Assert</c>.
    /// </remarks>
    public static class Check
    {
        /// <summary>
        /// Checks for a condition and throws an exception containing a message if the condition is false. 
        /// </summary>
        /// <remarks>The exception message is prefixed with text indicating that this is an assertion failure.</remarks>
        /// <param name="condition"><c>true</c> to prevent a message being displayed; otherwise, <c>false</c>.</param>
        /// <param name="message">A message to display.</param>
        /// <exception cref="InvalidOperationException"><i>condition</i> is false.</exception>
        public static void Assert(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException("Assertion failed: " + message);
        }

        /// <summary>
        /// Checks for a condition and throws an exception containing a message if the condition is false 
        /// and an inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="condition"><c>true</c> to prevent a message being displayed; otherwise, <c>false</c>.</param>
        /// <param name="message">A message to display.</param>
        /// <param name="inner">The inner <see cref="Exception"/>.</param>
        /// <remarks>The exception message is prefixed with text indicating that this is an assertion failure.</remarks>
        /// <exception cref="InvalidOperationException"><i>condition</i> is false.</exception>
        public static void Assert(bool condition, string message, Exception inner)
        {
            if (!condition) throw new InvalidOperationException("Assertion failed: " + message, inner);
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is false
        /// and an inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="condition"><c>true</c> to prevent a message being displayed; otherwise, <c>false</c>.</param>
        /// <param name="inner">The inner <see cref="Exception"/>.</param>
        /// <remarks>The exception message contains text indicating that this is an assertion failure.</remarks>
        /// <exception cref="InvalidOperationException"><i>condition</i> is false.</exception>
        public static void Assert(bool condition, Exception inner)
        {
            if (!condition) throw new InvalidOperationException("Assertion failed.", inner);
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is false. 
        /// </summary>
        /// <remarks>The exception message contains text indicating that this is an assertion failure.</remarks>
        /// <param name="condition"><c>true</c> to prevent a message being displayed; otherwise, <c>false</c>.</param>
        /// <exception cref="InvalidOperationException"><i>condition</i> is false.</exception>
        public static void Assert(bool condition)
        {
            if (!condition) throw new InvalidOperationException("Assertion failed.");
        }
    }
}
