using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : IntervalBuilder<IBoundedPeriod, DateTime>, ISetBuilder<BoundedPeriodSet, IBoundedPeriod, DateTime>
    {
        public BoundedPeriodSet MakeSet(IList<IBoundedPeriod> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        public IBoundedPeriod MakeNonEmptySet(IList<IBoundedPeriod> intervals)
        {
            return new NonEmptyBoundedPeriodSet(intervals);
        }
        /// <inheritdoc />
        public override IBoundedPeriod MakeStartingInterval(Start<DateTime> from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeStartingInterval()
        {
            return new EntireBoundedPeriod();
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeStartEndingInterval(Start<DateTime> @from, End<DateTime> to)
        {
            return new StartEndingBoundedPeriod(from, to);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeEndingInterval(End<DateTime> end)
        {
            return new EndingBoundedPeriod(end);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeDegenerate(Degenerate<DateTime> degenerate)
        {
            return new DegenerateBoundedPeriod(degenerate);
        }

        /// <inheritdoc />
        public override Infinity<DateTime> PositiveInfinity => new Infinity<DateTime>(DateTime.MaxValue);

        /// <inheritdoc />
        public override Infinity<DateTime> NegativeInfinity => new Infinity<DateTime>(DateTime.MinValue);
    }
}
