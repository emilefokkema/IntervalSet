using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : Builder<BoundedPeriodSet, IBoundedPeriod, DateTime>
    {
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
        public override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> periods)
        {
            return new BoundedPeriodSet(periods);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeNonEmptySet(IList<IBoundedPeriod> list)
        {
            return new NonEmptyBoundedPeriodSet(list);
        }
    }
}
