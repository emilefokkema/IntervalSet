﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IntervalSet;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;
using Newtonsoft.Json;
using NUnit.Framework;
using PeriodSet;
using PeriodSet.Period;

namespace IntervalSetTest.PeriodSet
{
    public class PeriodSetTest : Tests
    {
        [Test]
        public void Test_operations()
        {
            (new OpenPeriodSet(startOne, startFour) - two).Should().Be(one + three);

            (two - new OpenPeriodSet(startOne, startFour)).Should().Be(empty);

            (one - empty).Should().Be(one);

            (one - one).Should().Be(empty);

            (new OpenPeriodSet(startOne) - new OpenPeriodSet(startTwo)).Should().Be(one);

            (new OpenPeriodSet(startTwo) - new OpenPeriodSet(startOne)).Should().Be(empty);

            (new OpenPeriodSet(startOne) - two).Should().Be(one + new OpenPeriodSet(startThree));

            (empty - one).Should().Be(empty);

            (new OpenPeriodSet(startOne, startThree) + new OpenPeriodSet(startTwo, startFour))
                .Should().Be(new OpenPeriodSet(startOne, startFour));

            (one + three + two)
                .Should().Be(new OpenPeriodSet(startOne, startFour));

            (new OpenPeriodSet(startOne, startThree) + 
                new OpenPeriodSet(startFour, startSeven) + 
                new OpenPeriodSet(startTwo, startFive) + 
                new OpenPeriodSet(startSix, startEight))
                .Should().Be(new OpenPeriodSet(startOne, startEight));

            ((one + two) * (two + three)).Should().Be(two);

            (one * three).Should().Be(empty);

            (one * two).Should().Be(empty);

            (two * (one + two + three)).Should().Be(two);

            (one * empty).Should().Be(empty);

            (one * one).Should().Be(one);
        }

        [Test]
        public void Test_empty()
        {
            IOpenPeriod nonEmpty;
            OpenPeriodSet.Empty.IsNonEmpty(out nonEmpty).Should().BeFalse();
            nonEmpty.Should().BeNull();

            (OpenPeriodSet.Empty + OpenPeriodSet.Empty).Should().Be(OpenPeriodSet.Empty);
            (BoundedPeriodSet.Empty + BoundedPeriodSet.Empty).Should().Be(BoundedPeriodSet.Empty);
        }

        [Test]
        public void Test_degenerate()
        {
            BoundedPeriodSet degenerate = new BoundedPeriodSet(startOne,startOne);
            degenerate.Should().NotBe(BoundedPeriodSet.Empty);
            degenerate.Should().NotBe(OpenPeriodSet.Empty);
            degenerate.IsEmpty.Should().BeFalse();
            IDefaultInterval<DateTime> nonEmptyDegenerate;
            degenerate.IsNonEmpty(out nonEmptyDegenerate).Should().BeTrue();
            nonEmptyDegenerate.Start.Should().Be(startOne);
            nonEmptyDegenerate.End.Should().Be(startOne);

            (degenerate * one).Should().Be(degenerate);
            (one - degenerate).Cross(degenerate).Should().Be(empty);
            (degenerate + two).Cross(degenerate).Should().NotBe(empty);

            degenerate = new BoundedPeriodSet(startTwo, startTwo);
            BoundedPeriodSet set = one + two;
            BoundedPeriodSet difference = set - degenerate;
            difference.Contains(startTwo).Should().BeFalse();
            difference.Cross(degenerate).Should().Be(empty);
        }

        [Test]
        public void Test_initialize_degenerate()
        {
            BoundedPeriodSet degenerate;
            NonEmptyBoundedPeriodSet nonEmptyDegenerate;
            OpenPeriodSet openDegenerate;
            NonEmptyOpenPeriodSet nonEmptyOpenDegenerate;

            DateTime from = startOne;
            DateTime? to = startOne;
            degenerate = new BoundedPeriodSet(from, to);
            degenerate.Boundaries.Count().Should().Be(1);

            nonEmptyDegenerate = new NonEmptyBoundedPeriodSet(startOne, startOne);
            nonEmptyDegenerate.Boundaries.Count().Should().Be(1);

            openDegenerate = new OpenPeriodSet(from, to);
            openDegenerate.Boundaries.Count().Should().Be(1);

            nonEmptyOpenDegenerate = new NonEmptyOpenPeriodSet(startOne, startOne);
            nonEmptyOpenDegenerate.Boundaries.Count().Should().Be(1);
        }

        [Test]
        public void Test_nonempty()
        {
            OpenPeriodSet set = new OpenPeriodSet(startOne,startTwo) + new OpenPeriodSet(startThree,startFour);
            IOpenPeriod nonEmpty;
            set.IsNonEmpty(out nonEmpty);
            nonEmpty.Earliest.Should().Be(startOne);
            nonEmpty.To.Should().Be(startFour);

            

            NonEmptyBoundedPeriodSet nonEmptyBounded = new NonEmptyBoundedPeriodSet(startOne);
            DateTime earliest = nonEmptyBounded.Start;
            DateTime last = nonEmptyBounded.End;
            earliest.Should().Be(startOne);
            last.Should().Be(DateTime.MaxValue);
            nonEmptyBounded.IntervalCount.Should().Be(1);

            NonEmptyOpenPeriodSet nonEmptyOpen = new NonEmptyOpenPeriodSet(startOne);
            earliest = nonEmptyOpen.Earliest;
            DateTime? lastIfAny = nonEmptyOpen.To;
            earliest.Should().Be(startOne);
            lastIfAny.Should().Be(null);

            nonEmptyOpen = new NonEmptyOpenPeriodSet(startOne);
            nonEmptyOpen.Earliest.Should().Be(startOne);
        }

        private void ValidCast(Action action, bool valid)
        {
            if (valid)
            {
                action.Should().NotThrow<InvalidCastException>();
            }
            else
            {
                action.Should().Throw<InvalidCastException>();
            }
        }

        [Test]
        public void Test_nonempty_cast()
        {
            ValidCast(() =>
            {
                NonEmptyBoundedPeriodSet x = (NonEmptyBoundedPeriodSet)new BoundedPeriodSet(startOne, startOne);
            },true);
            ValidCast(() =>
            {
                NonEmptyBoundedPeriodSet x = (NonEmptyBoundedPeriodSet)new BoundedPeriodSet();
            }, false);
            ValidCast(() =>
            {
                NonEmptyBoundedPeriodSet x = (NonEmptyBoundedPeriodSet)new BoundedPeriodSet(startOne, startTwo);
            }, true);
            ValidCast(() =>
            {
                NonEmptyOpenPeriodSet x = (NonEmptyOpenPeriodSet)new OpenPeriodSet(startOne, startOne);
            }, true);
            ValidCast(() =>
            {
                NonEmptyOpenPeriodSet x = (NonEmptyOpenPeriodSet)new OpenPeriodSet();
            }, false);
            ValidCast(() =>
            {
                NonEmptyOpenPeriodSet x = (NonEmptyOpenPeriodSet)new OpenPeriodSet(startOne, startTwo);
            }, true);
        }

        [Test]
        public void Test_where()
        {
            (one + three).Where((x, y) => x == startTwo && y == startThree)
                .Should().Be(empty);

            (one + three).Where((x, y) => x == startOne && y == startTwo)
                .Should().Be(one);

            (one + three).Where((x, y) => x >= startOne && y <= startFour)
                .Should().Be(one + three);

            OpenPeriodSet.All.Where((x, y) => x == startTwo && y == startThree, new[] { startOne, startTwo, startThree, startFour })
                .Should().Be(two);

            OpenPeriodSet.All.Where(x => x == startTwo, new[] { startOne, startTwo, startThree })
                .Should().Be(two);

            OpenPeriodSet.All.Where(x => x == startTwo || x == startThree, new[] { startOne, startTwo, startThree })
                .Should().Be(new OpenPeriodSet(startTwo));

            OpenPeriodSet.All.Where(x => x == startTwo, new[] { startOne, startTwo })
                .Should().Be(new OpenPeriodSet(startTwo));

            new OpenPeriodSet(startTwo).Where((x, y) => true, new[] {startOne, startTwo, startThree}).Should().Be(two);

            (one + two + three).Where((x, y) => x >= startTwo && y <= startFive).Should().Be(empty);

            (one + two + three).Where((x, y) => x >= startTwo && y <= startFive , new[] {startTwo}).Should().Be(two + three);

            (one + two + three).Where((x, y) => x >= startTwo && y <= startFive, new[] { startTwo, startThree }).Should().Be(two + three);
        }

        [Test]
        public void Test_all()
        {
            OpenPeriodSet openAll = OpenPeriodSet.All;
            BoundedPeriodSet boundedAll = BoundedPeriodSet.All;
            OpenPeriodSet remainder = openAll.Minus(boundedAll);
            openAll.Should().Be(boundedAll);
            remainder.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void TestEquals()
        {
            BoundedPeriodSet bounded = new OpenPeriodSet(startOne, startTwo);
            bounded.Should()
                .Be((BoundedPeriodSet)one);

            OpenPeriodSet open = new BoundedPeriodSet(startOne, startTwo);
            open.Should().Be(one);

            bounded = new OpenPeriodSet(startOne);
            NonEmptyBoundedPeriodSet nonEmptyBounded = (NonEmptyBoundedPeriodSet) bounded;
            nonEmptyBounded.End.Should().Be(DateTime.MaxValue);
            nonEmptyBounded.Should().Be(new BoundedPeriodSet(startOne));
            new BoundedPeriodSet(startOne).Should().Be(nonEmptyBounded);
        }

        [Test]
        public void Test_collision()
        {
            Dictionary<IIntervalSet<DateTime>,string> dictionary = new Dictionary<IIntervalSet<DateTime>, string>
            {
                { one, "one"},
                { BoundedPeriodSet.Empty, "empty"}
            };
            dictionary.ContainsKey((BoundedPeriodSet)one).Should().BeTrue();
            dictionary.ContainsKey(OpenPeriodSet.Empty).Should().BeTrue();
        }

        [Test]
        public void Test_intersect()
        {
            new BoundedPeriodSet(startOne, startTwo).Intersects(new BoundedPeriodSet(startTwo, startThree)).Should()
                .BeFalse();
        }

        [Test]
        public void Test_periods()
        {
            DefaultStartingInterval<DateTime> start1 = new DefaultStartingInterval<DateTime>(new Start<DateTime>(startOne, Inclusivity.Inclusive));
            DefaultStartingInterval<DateTime> start2 = new DefaultStartingInterval<DateTime>(new Start<DateTime>(startTwo, Inclusivity.Inclusive));


            DefaultIntervalSet<DateTime> difference = start1.Minus(start2);
            difference.Should().Be(one);

            IDefaultInterval<DateTime> period;
            start1.IsNonEmpty(out period).Should().BeTrue();
            period.Should().Be(start1);
        }

        [Test]
        public void Test_entire()
        {
            new OpenPeriodSet(new EntireOpenPeriod()).Should().Be(new OpenPeriodSet(new EntireOpenPeriod()));
            
        }

        [Test]
        public void Test_entire_subtract()
        {
            OpenPeriodSet entire = OpenPeriodSet.All;

            IOpenPeriod nonEmptyEntire;
            entire.IsNonEmpty(out nonEmptyEntire).Should().BeTrue();
            nonEmptyEntire.Earliest.Should().Be(DateTime.MinValue);
            nonEmptyEntire.To.Should().Be(null);

            OpenPeriodSet set1 = new OpenPeriodSet(startFour);
            OpenPeriodSet difference = entire - set1;

            difference.Contains(startOne).Should().BeTrue();
            IOpenPeriod nonEmptyDifference;
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.Earliest.Should().Be(DateTime.MinValue);
            nonEmptyDifference.To.Should().Be(startFour);

            difference = difference - (one + two);
            difference.Contains(startTwo).Should().BeFalse();
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.Earliest.Should().Be(DateTime.MinValue);
            nonEmptyDifference.To.Should().Be(startFour);

            difference = entire - (one + two);
            difference.Contains(startTwo).Should().BeFalse();
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.Earliest.Should().Be(DateTime.MinValue);
            nonEmptyDifference.To.Should().Be(null);

            difference = entire - new OpenPeriodSet(startOne, startOne);
            difference.Contains(startOne).Should().BeFalse();
            difference.IsNonEmpty(out nonEmptyDifference).Should().BeTrue();
            nonEmptyDifference.Earliest.Should().Be(DateTime.MinValue);
            nonEmptyDifference.To.Should().Be(null);
        }

        [Test]
        public void Test_entire_add()
        {
            OpenPeriodSet left = OpenPeriodSet.All - new OpenPeriodSet(startTwo);
            OpenPeriodSet right = new OpenPeriodSet(startOne);
            (left + right).Should().Be(OpenPeriodSet.All);
            (right + left).Should().Be(OpenPeriodSet.All);

            (OpenPeriodSet.All - (two + three) + (one + two + three + four)).Should().Be(OpenPeriodSet.All);

            OpenPeriodSet left1 = OpenPeriodSet.All - new OpenPeriodSet(startOne);
            OpenPeriodSet left2 = OpenPeriodSet.All - new OpenPeriodSet(startTwo);

            (left1 + one).Should().Be(left2);
        }

        [Test]
        public void Test_entire_cross()
        {
            OpenPeriodSet left = new OpenPeriodSet(startOne);
            OpenPeriodSet right = OpenPeriodSet.All - new OpenPeriodSet(startTwo);

            (left * right).Should().Be(one);
            (right * left).Should().Be(one);

            OpenPeriodSet left1 = OpenPeriodSet.All - new OpenPeriodSet(startTwo);
            OpenPeriodSet left2 = OpenPeriodSet.All - new OpenPeriodSet(startOne);
            (left1 * left2).Should().Be(left2);
            (left2 * left1).Should().Be(left2);
        }

        [Test]
        public void Test_serialization()
        {
            string serialized = JsonConvert.SerializeObject(one);
            OpenPeriodSet deserialized = JsonConvert.DeserializeObject<OpenPeriodSet>(serialized);
            deserialized.Should().Be(one);
        }
    }
}
