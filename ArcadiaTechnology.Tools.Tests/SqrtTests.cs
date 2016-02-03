using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ArcadiaTechnology.Tools.Tests
{
    public class SqrtTests
    {
        [Datapoint]
        public double zero = 0;

        [Datapoint]
        public double positive = 1;

        [Datapoint]
        public double negative = -1;

        [Datapoint]
        public double max = double.MaxValue;

        [Datapoint]
        public double infinity = double.PositiveInfinity;

        [Theory]
        public void SquareRootDefinition(double num)
        {
            Assume.That(num >= 0.0 && num < double.MaxValue);

            double sqrt = Math.Sqrt(num);

            Assert.That(sqrt >= 0.0);
            Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
        }
    }
}
