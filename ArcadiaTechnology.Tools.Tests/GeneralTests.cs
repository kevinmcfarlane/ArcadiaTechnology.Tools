using System;
using System.Diagnostics;
using NUnit.Framework;

namespace ArcadiaTechnology.Tools.Tests
{
    [TestFixture]
    public class GeneralTests
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

        [Test]
        public void FindEmails()
        {
            string addressList = "kip@abc; joe.bloggs@mac.com ,  bob.jones@xyz.net, abc.co.uk;a.jones@recfirst.com";
            string[] expected = new string[] { "joe.bloggs@mac.com", "bob.jones@xyz.net", "a.jones@recfirst.com" };
            string[] actual = General.FindEmails(addressList);

            CollectionAssert.AreEqual(expected, actual, "The emails found differ from expected.");
        }

        [Test]
        public void FindEmailsFromEmptyList()
        {
            string addressList = String.Empty;
            string[] expected = new string[] { };
            string[] actual = General.FindEmails(addressList);

            CollectionAssert.AreEqual(expected, actual, "The emails found differ from expected.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindEmailsWithInvalidPrecondition()
        {
            string addressList = null;
            string[] actual = General.FindEmails(addressList);
        }
    }
}