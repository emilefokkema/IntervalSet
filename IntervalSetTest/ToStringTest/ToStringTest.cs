using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;
using IntervalSetTest.DefaultImplementation;
using NUnit.Framework;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<IntervalSetTest.DefaultImplementation.DoubleBuilder, double>;

namespace IntervalSetTest.ToStringTest
{
    [TestFixture]
    public class ToStringTest
    {
        [Test]
        public void TestBrackets()
        {
            string positiveInfinity = (double.PositiveInfinity).ToString("G", CultureInfo.CurrentCulture);
            string negativeInfinity = (double.NegativeInfinity).ToString("G", CultureInfo.CurrentCulture);
            DoubleSet.All.ToString().Should().Be($"({negativeInfinity}, {positiveInfinity})");
            DoubleSet fiveSix = new DoubleSet(5, 6);
            fiveSix.ToString().Should().Be("(5, 6)");
            DoubleSet.All.Minus(fiveSix).ToString().Should().Be($"({negativeInfinity}, 5] + [6, {positiveInfinity})");
            new DefaultDegenerateInterval<DoubleBuilder, double>(new Degenerate<double>(5))
                .ToString("G", CultureInfo.CurrentCulture).Should().Be("[5, 5]");
        }
    }
}
