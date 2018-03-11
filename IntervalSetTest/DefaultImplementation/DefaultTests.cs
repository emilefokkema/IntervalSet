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
            nonEmptyJoin.Start.Should().Be(5f);
            nonEmptyJoin.End.Should().Be(6.5f);
        }

        [Test]
        public void Test_substituting_builder()
        {
            DoubleSet a = new DoubleSet(6, 6);
            a.IsEmpty.Should().BeTrue();

            DoubleSet six = DoubleSet.Empty.Plus(6);
            six.Contains(6).Should().BeTrue();

            new DoubleSet(1, 2).Plus(new DoubleSet(2, 3)).Plus(2).Should().Be(new DoubleSet(1, 3));
            new DoubleSet(1, 3).Minus(2).Should().Be(new DoubleSet(1, 2).Plus(new DoubleSet(2, 3)));

            DoubleSet fiveSix = new DoubleSet(5, 6);
            fiveSix.Contains(5).Should().BeFalse();
            DoubleSet all = DoubleSet.All;
            IDefaultInterval<double> nonEmptyAll;
            all.IsNonEmpty(out nonEmptyAll).Should().BeTrue();
            nonEmptyAll.Start.Should().Be(double.NegativeInfinity);
            nonEmptyAll.End.Should().Be(double.PositiveInfinity);
            DoubleSet difference = all.Minus(fiveSix);
            DoubleSet product = difference.Cross(new DoubleSet(4, 7));
            product.Should().Be(new DoubleSet(4, 5).Plus(new DoubleSet(6, 7)).Plus(5).Plus(6));

            IDefaultInterval<double> nonEmptyProduct;
            product.IsNonEmpty(out nonEmptyProduct).Should().BeTrue();
            nonEmptyProduct.Start.Should().Be(4d);
            nonEmptyProduct.End.Should().Be(7d);

            product.Contains(4.5).Should().BeTrue();
            product.Contains(5.5).Should().BeFalse();
            difference.Contains(double.PositiveInfinity).Should().BeTrue();
            IDefaultInterval<double> nonEmptyDifference;
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.End.Should().Be(double.PositiveInfinity);

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
            nonEmptySet2.Start.Should().Be(5);
            nonEmptySet2.End.Should().Be(7);
        }

        [Test]
        public void Test_complement()
        {
            IntSet complement = new IntSet(1, 3).Complement();
            complement.ContainsNegativeInfinity().Should().BeTrue();
            complement.Contains(3).Should().BeTrue();
            complement.Contains(1).Should().BeFalse();
            complement.Contains(2).Should().BeFalse();

            complement = new IntSet(5).Complement();
            complement.Contains(5).Should().BeFalse();
            complement.ContainsNegativeInfinity().Should().BeTrue();
            complement.Contains(6).Should().BeFalse();
            complement.Contains(4).Should().BeTrue();

            DoubleSet set = new DoubleSet(1, 2).Plus(new DoubleSet(2, 3));
            DoubleSet doubleComplement = set.Complement();
            doubleComplement.Contains(2).Should().BeTrue();
            doubleComplement.Contains(1.5).Should().BeFalse();
            doubleComplement.Contains(2.5).Should().BeFalse();

            DoubleSet degenerate = DoubleSet.Empty.Plus(4);
            doubleComplement = degenerate.Complement();
            doubleComplement.Contains(4).Should().BeFalse();
            doubleComplement.Contains(5).Should().BeTrue();
            doubleComplement.Contains(3).Should().BeTrue();

            DoubleSet.All.Minus(5).Complement().Should().Be(DoubleSet.Empty.Plus(5));
        }

        [Test]
        public void Test_interior()
        {
            DoubleSet degenerate = DoubleSet.Empty.Plus(7);
            degenerate.Interior().Should().Be(DoubleSet.Empty);

            DoubleSet closed = new DoubleSet(5, 6).Plus(5).Plus(6);
            closed.Interior().Should().Be(new DoubleSet(5, 6));

            closed.Plus(degenerate).Interior().Should().Be(new DoubleSet(5, 6));

            new DoubleSet(5, 6).Interior().Should().Be(new DoubleSet(5, 6));

            IntSet intSet = new IntSet(3,3);
            intSet.IsEmpty.Should().BeFalse();
            intSet.Interior().IsEmpty.Should().BeTrue();
        }
    }
}
