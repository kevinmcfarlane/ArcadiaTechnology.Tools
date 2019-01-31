using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// Generates a unique random number from a specified range of numbers.
    /// </summary>
    /// <remarks>
    /// "Unique" means with respect to the number's position in the range.
    /// Once a number has been generated for an instance the number at that position in the range will not be generated again.
    /// Subsequent generations will eventually exhaust the entries in the range.
    /// </remarks>
    /// <example>
    /// <para>
    /// This range has duplicates.
    /// </para>
    /// { 1, 2, 3, 4, 3, 6, 4, 8, 17, 42, 6 }
    /// <para>
    /// If the first 3 is selected at random then the generator can only subsequently select the second 3.
    /// If a random number were generated eleven times we might get this result:
    /// 42 6 3 4 2 4 6 17 3 8 1
    /// </para>
    /// <para>
    /// This range is an increasing sequence of consecutive numbers.
    /// </para>
    /// { 1, 2, 3, 4, 5, 6, 7, 8 }
    /// <para>
    /// If a random number were generated eight times we might get this result: 4 2 5 1 8 7 3 6
    /// </para>
    /// </example>
    public class UniqueRandomNumberGenerator
    {
        #region Fields

        private Random _rand = new Random();
        private List<int> _remainingNumbers = new List<int>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueRandomNumberGenerator"></see> class
        /// with a specified range of numbers.
        /// </summary>
        /// <remarks>
        /// The range may contain duplicates. See summary for <see cref="UniqueRandomNumberGenerator"></see> class.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="numbers" /> is null.</exception>
        /// <param name="numbers">The range of numbers.</param>
        public UniqueRandomNumberGenerator(IEnumerable<int> numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentNullException("numbers", "numbers is null.");
            }
            _remainingNumbers.AddRange(numbers);

            TraceRange();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueRandomNumberGenerator"/> class
        /// with an increasing sequence of consecutive numbers in the specified range.
        /// </summary>
        /// <remarks>
        /// If the client specifies a minimum number of 3 and a maximum number of 9 then internally
        /// the instance generates the range: {3, 4, 5, 6, 7, 8, 9} from which the random number is generated.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minNumber" /> is greater than <paramref name="maxNumber" />.</exception>
        /// <param name="minNumber">The minimum number in the range.</param>
        /// <param name="maxNumber">The maximum number in the range.</param>
        public UniqueRandomNumberGenerator(int minNumber, int maxNumber)
        {
            Debug.Assert(maxNumber >= minNumber, $"Min Value is {minNumber}, it must not be greater than the Max Value {maxNumber}.");

            if (minNumber > maxNumber)
            {
                throw new ArgumentOutOfRangeException("minNumber", minNumber, "minNumber is greater than the maxNumber.");
            }

            CreateIncreasingConsecutiveNumberRange(minNumber, maxNumber);

            TraceRange();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the count of remaining numbers from which a new random number will be generated.
        /// </summary>
        /// <remarks>
        /// Clients should check this prior to calls to <seealso cref="NewRandomNumber"/>.
        /// </remarks>
        /// <value>The remaining numbers count.</value>
        public int RemainingNumbersCount
        {
            get { return _remainingNumbers.Count; }
        }

        /// <summary>
        /// Gets the remaining numbers from which a new random number will be generated.
        /// </summary>
        /// <value>The remaining numbers array.</value>
        public ReadOnlyCollection<int> RemainingNumbers
        {
            get { return _remainingNumbers.AsReadOnly(); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Generates a new random number from the range of numbers initially supplied to the instance (see class summary for description of how this works).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><see cref="RemainingNumbersCount"/> = 0.</exception>
        /// <returns>The number stored at the randomly selected range index.</returns>
        public int NewRandomNumber()
        {
            Debug.Assert(RemainingNumbersCount > 0, $"The count of remaining numbers is {RemainingNumbersCount}, it must be at least 1.");

            // Generate a random index from the list of remaining numbers
            int maxIndex = _remainingNumbers.Count - 1;
            int index = _rand.Next(0, maxIndex + 1);

            // Get the number at that index
            int number = _remainingNumbers[index];

            // Now remove it so it can't be generated again
            _remainingNumbers.RemoveAt(index);

            return number;
        }

        private void CreateIncreasingConsecutiveNumberRange(int minNumber, int maxNumber)
        {
            int currentEntry = minNumber;

            while (currentEntry <= maxNumber)
            {
                _remainingNumbers.Add(currentEntry);
                currentEntry++;
            }
        }

        private void TraceRange()
        {
            Debug.WriteLine("Random Number List");
            _remainingNumbers.ForEach(value => Debug.Write($"{value} "));
            Debug.WriteLine(string.Empty);
        }

        #endregion Methods
    }
}