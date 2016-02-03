using System;
using System.Diagnostics;
using NUnit.Framework;

namespace ArcadiaTechnology.Tools.Tests
{
    [TestFixture]
    public class ValidationToolTests
    {
        /// <summary>
        /// Redirects Debug Assert dialog messages to console output.
        /// </summary>
        /// <remarks>
        /// Debug assertions are written to NUnit's Console output tab.
        /// </remarks>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Disable Debug traces
            Trace.Listeners.Clear();

            // Disable Debug assert message boxes
            using (DefaultTraceListener listener = new DefaultTraceListener())
            {
                listener.AssertUiEnabled = false;
                Trace.Listeners.Add(listener);
            }

            // Restore Debug traces to NUnit's Console.Out tab.
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        [TestCase(
        "joe@abc.co.uk",
        Description = "Valid email in username",
        TestName = "ValidEmailInUserName"
        )]
        [TestCase(
        "joe.bloggs@abc.com",
        Description = "Valid email with period in username",
        TestName = "ValidEmailWithPeriodInUsername"
        )]
        [TestCase(
        "joe_bloggs@abc.com",
        Description = "Valid email with underscore in username",
        TestName = "ValidEmailWithUnderscoreInUsername"
        )]
        [TestCase(
        "joe-bloggs@abc.com",
        Description = "Valid email with hyphen in username",
        TestName = "ValidEmailWithHyphenInUsername"
        )]
        public void ValidEmail(string email)
        {
            Assert.IsTrue(ValidationTool.IsValidEmail(email));
        }
    }

    [Obsolete("No longer necessary since .NET introduced String.IsNullOrWhitespace method in .NET 4, but keep to show how to use NUnit Theory.")]
    public class IsNullOrEmptyOrBlankTests
    {
        [Datapoint]
        public string empty = String.Empty;

        [Datapoint]
        public string spaces = "  ";

        [Datapoint]
        public string nullInstance = null;

        [Theory]
        public void IsNullOrEmptyOrBlank(string arg)
        {
            Assert.That(ValidationTool.IsNullOrEmptyOrBlank(arg));
        }
    }
}