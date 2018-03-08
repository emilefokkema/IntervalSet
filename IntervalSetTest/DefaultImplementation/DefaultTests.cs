using System.Collections.Generic;
using FluentAssertions;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;
using NUnit.Framework;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<IntervalSetTest.DefaultImplementation.DoubleBuilder, double>;
using IntSet = IntervalSet.Default.DefaultIntervalSet<int>;

namespace IntervalSetTest.DefaultImplementation
{
    [TestFixture]
    public class DefaultTests
    {
        [Test]
        public void Test_inheriting()
        {
            FloatSet set1 = new FloatSet(5, 6);
            FloatSet set2 = new FloatSet(5.5f, 6.5f);
            FloatSet join = set1.Plus(set2);
            IDefaultInterval<float> nonEmptyJoin;
            join.IsNonEmpty(out nonEmptyJoin).Should().BeTrue();
            nonEmptyJoin.Start.Location.Should().Be(5f);
            nonEmptyJoin.End.Location.Should().Be(6.5f);
        }

        [Test]
        public void Test_substituting_builder()
        {
            DoubleSet fiveSix = new DoubleSet(5, 6);
            DoubleSet all = DoubleSet.All;
            IDefaultInterval<double> nonEmptyAll;
            all.IsNonEmpty(out nonEmptyAll).Should().BeTrue();
            nonEmptyAll.Start.Location.Should().Be(double.NegativeInfinity);
            nonEmptyAll.End.Location.Should().Be(double.PositiveInfinity);
            DoubleSet difference = all.Minus(fiveSix);
            DoubleSet product = difference.Cross(new DoubleSet(4, 7));
            product.Should().Be(new DoubleSet(4, 5).Plus(new DoubleSet(6, 7)));

            IDefaultInterval<double> nonEmptyProduct;
            product.IsNonEmpty(out nonEmptyProduct).Should().BeTrue();
            nonEmptyProduct.Start.Location.Should().Be(4d);
            nonEmptyProduct.End.Location.Should().Be(7d);

            product.Contains(4.5).Should().BeTrue();
            product.Contains(5.5).Should().BeFalse();
            difference.Contains(double.PositiveInfinity).Should().BeTrue();
            IDefaultInterval<double> nonEmptyDifference;
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.End.Location.Should().Be(double.PositiveInfinity);

            product.Cross(fiveSix).Should().Be(DoubleSet.Empty);
        }

        [Test]
        public void Test_default()
        {
            IntSet set = new IntSet(new List<IDefaultInterval<int>> {new DefaultDegenerateInterval<int>(new Degenerate<int>(5))});
            IntSet set2 = set.Plus(new IntSet(6, 7));
            set2.Contains(5).Should().BeTrue();
            set2.Contains(6).Should().BeTrue();
            set2.Contains(7).Should().BeFalse();
            IDefaultInterval<int> nonEmptySet2;
            set2.IsNonEmpty(out nonEmptySet2).Should().BeTrue();
            nonEmptySet2.Start.Location.Should().Be(5);
            nonEmptySet2.End.Location.Should().Be(7);
        }
    }
}
