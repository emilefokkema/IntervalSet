using FluentAssertions;
using IntervalSet;
using IntervalSet.Default;
using IntervalSet.Interval.Default;
using NUnit.Framework;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<IntervalSetTest.DefaultImplementation.DefaultTests.DoubleBuilder, double>;

namespace IntervalSetTest.DefaultImplementation
{
    [TestFixture]
    public class DefaultTests
    {
        public class DoubleBuilder : DefaultBuilder<DoubleBuilder,double>
        {
            public override Infinity<double> PositiveInfinity => new Infinity<double>(double.PositiveInfinity);
            public override Infinity<double> NegativeInfinity => new Infinity<double>(double.NegativeInfinity);
        }

        [Test]
        public void Test1()
        {
            DoubleSet fiveSix = new DoubleSet(5, 6);
            DoubleSet all = DoubleSet.All;
            IDefaultInterval<double> nonEmptyAll;
            all.IsNonEmpty(out nonEmptyAll).Should().BeTrue();
            nonEmptyAll.Start.Should().Be(double.NegativeInfinity);
            nonEmptyAll.End.Should().Be(double.PositiveInfinity);
            DoubleSet difference = all.Minus(fiveSix);
            DoubleSet product = difference.Cross(new DoubleSet(4, 7));
            product.Should().Be(new DoubleSet(4, 5).Plus(new DoubleSet(6, 7)));

            IDefaultInterval<double> nonEmptyProduct;
            product.IsNonEmpty(out nonEmptyProduct).Should().BeTrue();
            nonEmptyProduct.Start.Should().Be(4);
            nonEmptyProduct.End.Should().Be(7);

            product.Contains(4.5).Should().BeTrue();
            product.Contains(5.5).Should().BeFalse();
            difference.Contains(double.PositiveInfinity).Should().BeTrue();
            IDefaultInterval<double> nonEmptyDifference;
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.End.Should().Be(double.PositiveInfinity);

            product.Cross(fiveSix).Should().Be(DoubleSet.Empty);
        }
    }
}
