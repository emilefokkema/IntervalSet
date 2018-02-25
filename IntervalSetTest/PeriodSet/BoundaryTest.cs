using FluentAssertions;
using IntervalSet.PeriodSet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSetTest.PeriodSet
{
    public class BoundaryTest : Tests
    {
        [Test]
        public void Test_cross()
        {
            OpenPeriodSet set = one + three + four;

            set.Cross(startOne).Should().Be(BoundaryKind.Start);
            set.Cross(startTwo).Should().Be(BoundaryKind.End);
            set.Cross(startThree).Should().Be(BoundaryKind.Start);
            set.Cross(startFour).Should().Be(BoundaryKind.Start);
            set.Cross(startFive).Should().Be(BoundaryKind.End);
            set.Cross(startSix).Should().Be(BoundaryKind.None);
        }
    }
}
