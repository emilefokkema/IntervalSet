using FluentAssertions;
using IntervalSet.Interval.Default;
using NUnit.Framework;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<double>;

namespace IntervalSetTest.DefaultImplementation
{
    [TestFixture]
    public class DefaultTests
    {
        [Test]
        public void Test1()
        {
            DoubleSet fiveSix = new DoubleSet(5, 6);
            DoubleSet all = DoubleSet.All;
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

            product.Cross(fiveSix).Should().Be(DoubleSet.Empty);
        }
    }
}
