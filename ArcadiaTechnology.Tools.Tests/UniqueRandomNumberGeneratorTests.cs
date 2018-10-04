using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace ArcadiaTechnology.Tools.Tests
{
    /// <summary>
    /// Unique random number generator tests.
    /// </summary>
    [TestFixture]
    public class UniqueRandomNumberGeneratorTests
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

        /// <summary>
        /// Supplies an array containing just one number. The generated number should be that number.
        /// </summary>
        [Test]
        public void ShouldReturnNumberInSingleNumberArray()
        {
            int[] numbers = new int[] { 1 };
            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(numbers);

            int number = 0;
            
            while (g.RemainingNumbersCount > 0)
            {
                number = g.NewRandomNumber();
            }

            int expected = numbers[0];
            int actual = number;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Supplies a range with equal minimum and maximum numbers. The generated number should be that minimum or maximum number.
        /// </summary>
        [Test]
        public void ShouldReturnNumberInSingleNumberRange()
        {
            int minNumber = 2;
            int maxNumber = 2;

            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(minNumber, maxNumber);

            int number = 0;
            
            while (g.RemainingNumbersCount > 0)
            {
                number = g.NewRandomNumber();
            }

            int expected = minNumber;
            int actual = number;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Selects numbers at random from the original array until the entire range is exhausted.
        /// The <seealso cref="Wintellect.PowerCollections.Bag" /> of results should equal the bag corresponding to the original array.
        /// </summary>
        /// <remarks>
        /// We compare bags because the array can contain duplicates and the generated results will 
        /// be in a different order to the original array entries.
        /// </remarks>
        [Test]
        public void ShouldReturnEquivalentBagToNumberArray()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 3, 6, 4, 8, 17, 42, 6 };
            ConcurrentBag<int> expected = new ConcurrentBag<int>(numbers);
            ConcurrentBag<int> actual = new ConcurrentBag<int>();

            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(numbers);

            while (g.RemainingNumbersCount > 0)
            {
                int number = g.NewRandomNumber();
                actual.Add(number);
            }

            CollectionAssert.IsEmpty(
                actual.Except(expected), 
                "The bag of random numbers differs from the bag corresponding to the initial array.");
        }

        /// <summary>
        /// Generates a few random numbers and checks that they are a subset of the supplied number array.
        /// </summary>
        [Test]
        public void ShouldReturnProperSubsetOfNumberArray()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 3, 6, 4, 8, 17, 42, 6 };
            ConcurrentBag<int> initialBag = new ConcurrentBag<int>(numbers);
            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(numbers);
            const int GeneratedRandomNumberCount = 3;

            // Sanity check on test data
            Debug.Assert(
                GeneratedRandomNumberCount < initialBag.Count,
                String.Format("The generated random number count {0} must be less than the count of initial numbers {1} for this test.", GeneratedRandomNumberCount, initialBag.Count));

            Debug.WriteLine("Random Numbers");
            int number = 0;

            ConcurrentBag<int> actualBag = new ConcurrentBag<int>();

            for (int i = 1; i <= GeneratedRandomNumberCount; i++)
            {
                number = g.NewRandomNumber();
                actualBag.Add(number);
            }

            HashSet<int> initialSet = new HashSet<int>(initialBag.AsEnumerable());
            HashSet<int> actualSet = new HashSet<int>(actualBag.AsEnumerable());

            Assert.IsTrue(
                actualSet.IsProperSubsetOf(initialSet),
                "Generated numbers should be a subset of the initial numbers.");
        }
        
        /// <summary>
        /// Generates a few random numbers and checks that they are a subset of the supplied number range.
        /// </summary>
        [Test]
        public void ShouldReturnProperSubsetOfNumberRange()
        {
            int minNumber = 3;
            int maxNumber = 15;

            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(minNumber, maxNumber);

            ReadOnlyCollection<int> numbers = g.RemainingNumbers;
            HashSet<int> initialSet = new HashSet<int>(numbers);

            const int GeneratedRandomNumberCount = 3;

            // Sanity check on test data
            Debug.Assert(
                GeneratedRandomNumberCount < initialSet.Count,
                String.Format("The generated random number count {0} must be less than the count of initial numbers {1} for this test.", GeneratedRandomNumberCount, initialSet.Count));

            Debug.WriteLine("Random Numbers");
            int number = 0;

            HashSet<int> actual = new HashSet<int>();
            for (int i = 1; i <= GeneratedRandomNumberCount; i++)
            {
                number = g.NewRandomNumber();
                actual.Add(number);
            }

            Assert.IsTrue(
                actual.IsProperSubsetOf(initialSet),
                "Generated numbers should be a subset of the set corresponding to the initial range.");
        }
        
        /// <summary>
        /// Tests for exception when the minimum number in range is greater than maximum number.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinimumNumberInRangeGreaterThanMaximumNumber()
        {
            int minNumber = 3;
            int maxNumber = 2;

            UniqueRandomNumberGenerator g = new UniqueRandomNumberGenerator(minNumber, maxNumber);
        }
    }
}
