using FluentAssertions;
using IntervalSet.PeriodSet;
using IntervalSet.PeriodSet.Period.Boundary;
using NUnit.Framework;

namespace IntervalSetTest.PeriodSet
{
    public class BoundaryTest : Tests
    {
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
        public void Test_boundary_kind()
        {
            BoundaryKind degenerate = new Degenerate();
            BoundaryKind inclusiveStart = new Start(Inclusivity.Inclusive);
            BoundaryKind exclusiveStart = new Start(Inclusivity.Exclusive);
            BoundaryKind inclusiveEnd = new End(Inclusivity.Inclusive);
            BoundaryKind exclusiveEnd = new End(Inclusivity.Exclusive);

            inclusiveStart.Minus(exclusiveStart).Should().Be(degenerate);
            inclusiveEnd.Minus(exclusiveEnd).Should().Be(degenerate);

            inclusiveStart.Minus(inclusiveEnd).Should().Be(exclusiveStart);
            inclusiveStart.Minus(exclusiveEnd).Should().Be(inclusiveStart);

            inclusiveEnd.Minus(inclusiveStart).Should().Be(exclusiveEnd);
            inclusiveEnd.Minus(exclusiveStart).Should().Be(inclusiveEnd);

            new Continuation().Minus(inclusiveStart).Should().Be(exclusiveEnd);
            new Continuation().Minus(exclusiveStart).Should().Be(inclusiveEnd);
            new Continuation().Minus(inclusiveEnd).Should().Be(exclusiveStart);
            new Continuation().Minus(exclusiveEnd).Should().Be(inclusiveStart);
        }
    }
}
