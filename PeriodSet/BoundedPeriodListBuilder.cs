using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : IntervalBuilder<IBoundedPeriod, DateTime>
    {
        /// <inheritdoc />
        public override IBoundedPeriod MakeStartingInterval<TBuilder>(Start<DateTime> from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeStartingInterval<TBuilder>()
        {
            return new EntireBoundedPeriod();
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeStartEndingInterval<TBuilder>(Start<DateTime> @from, End<DateTime> to)
        {
            return new StartEndingBoundedPeriod(from, to);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeEndingInterval<TBuilder>(End<DateTime> end)
        {
            return new EndingBoundedPeriod(end);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeDegenerate<TBuilder>(Degenerate<DateTime> degenerate)
        {
            return new DegenerateBoundedPeriod(degenerate);
        }

        /// <inheritdoc />
        public override Infinity<DateTime> PositiveInfinity => new Infinity<DateTime>(DateTime.MaxValue);

        /// <inheritdoc />
        public override Infinity<DateTime> NegativeInfinity => new Infinity<DateTime>(DateTime.MinValue);
    }
}
