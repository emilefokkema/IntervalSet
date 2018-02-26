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

            set.Cross(startOne).Should().Be(new Start(startOne, Inclusivity.Inclusive));
            set.Cross(startTwo).Should().Be(new End(startTwo, Inclusivity.Exclusive));
            set.Cross(startThree).Should().Be(new Start(startThree, Inclusivity.Inclusive));
            set.Cross(startFour).Should().Be(new Start(startFour, Inclusivity.Inclusive));
            set.Cross(startFive).Should().Be(new End(startFive, Inclusivity.Exclusive));
            set.Cross(startSix).Should().Be(null);

            new BoundedPeriodSet(startOne, startOne).Cross(startOne).Should().Be(new StartEnd(startOne));
        }
    }
}
