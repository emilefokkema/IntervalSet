using FluentAssertions;
using IntervalSet.PeriodSet;
using IntervalSet.PeriodSet.Period.Boundary;
using IntervalSet.PeriodSet.Period.Boundary.Kind;
using NUnit.Framework;

namespace IntervalSetTest.PeriodSet
{
    public class BoundaryTest : Tests
    {
        private BoundaryKind degenerate = new Degenerate();
        private BoundaryKind inclusiveStart = new Start(Inclusivity.Inclusive);
        private BoundaryKind exclusiveStart = new Start(Inclusivity.Exclusive);
        private BoundaryKind inclusiveEnd = new End(Inclusivity.Inclusive);
        private BoundaryKind exclusiveEnd = new End(Inclusivity.Exclusive);
        private BoundaryKind continuation = new Continuation();

        [Test]
        public void Test_cross()
        {
            OpenPeriodSet set = one + three + four;

            set.Cross(startOne).Should().Be(new Start(Inclusivity.Inclusive));
            set.Cross(startTwo).Should().Be(new End(Inclusivity.Exclusive));
            set.Cross(startThree).Should().Be(new Start(Inclusivity.Inclusive));
            set.Cross(startFour).Should().Be(new Continuation());
            set.Cross(startFive).Should().Be(new End(Inclusivity.Exclusive));
            set.Cross(startSix).Should().Be(null);

            new BoundedPeriodSet(startOne, startOne).Cross(startOne).Should().Be(new Degenerate());
        }

        [Test]
        public void Test_boundary_kind_minus()
        {
            inclusiveStart.Minus(exclusiveStart).Should().Be(degenerate);
            inclusiveEnd.Minus(exclusiveEnd).Should().Be(degenerate);

            inclusiveStart.Minus(inclusiveEnd).Should().Be(exclusiveStart);
            inclusiveStart.Minus(exclusiveEnd).Should().Be(inclusiveStart);

            inclusiveEnd.Minus(inclusiveStart).Should().Be(exclusiveEnd);
            inclusiveEnd.Minus(exclusiveStart).Should().Be(inclusiveEnd);

            continuation.Minus(inclusiveStart).Should().Be(exclusiveEnd);
            continuation.Minus(exclusiveStart).Should().Be(inclusiveEnd);
            continuation.Minus(inclusiveEnd).Should().Be(exclusiveStart);
            continuation.Minus(exclusiveEnd).Should().Be(inclusiveStart);
        }

        [Test]
        public void Test_boundary_kind_plus()
        {
            inclusiveStart.Plus(exclusiveStart).Should().Be(inclusiveStart);
            inclusiveEnd.Plus(exclusiveEnd).Should().Be(inclusiveEnd);

            inclusiveStart.Plus(inclusiveEnd).Should().Be(continuation);
            inclusiveStart.Plus(exclusiveEnd).Should().Be(continuation);

            inclusiveEnd.Plus(inclusiveStart).Should().Be(continuation);
            inclusiveEnd.Plus(exclusiveStart).Should().Be(continuation);

            continuation.Plus(inclusiveStart).Should().Be(continuation);
        }

        [Test]
        public void Test_boundary_kind_cross()
        {
            inclusiveStart.Cross(exclusiveStart).Should().Be(exclusiveStart);
            inclusiveEnd.Cross(exclusiveEnd).Should().Be(exclusiveEnd);

            inclusiveStart.Cross(inclusiveEnd).Should().Be(degenerate);
            inclusiveStart.Cross(exclusiveEnd).Should().BeNull();

            inclusiveEnd.Cross(inclusiveStart).Should().Be(degenerate);
            inclusiveEnd.Cross(exclusiveStart).Should().BeNull();

            continuation.Cross(inclusiveStart).Should().Be(inclusiveStart);
            continuation.Cross(exclusiveStart).Should().Be(exclusiveStart);
            continuation.Cross(inclusiveEnd).Should().Be(inclusiveEnd);
            continuation.Cross(exclusiveEnd).Should().Be(exclusiveEnd);
        }
    }
}
