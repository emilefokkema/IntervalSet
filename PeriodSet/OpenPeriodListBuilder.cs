using System;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : IntervalBuilder<IOpenPeriod, DateTime>
    {

        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval<TBuilder>(Start<DateTime> from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval<TBuilder>()
        {
            return new EntireOpenPeriod();
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate<TBuilder>(Degenerate<DateTime> degenerate)
        {
            return new DegenerateOpenPeriod(degenerate);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeEndingInterval<TBuilder>(End<DateTime> end)
        {
            return new EndingOpenPeriod(end);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartEndingInterval<TBuilder>(Start<DateTime> from, End<DateTime> to)
        {
            return new StartEndingOpenPeriod(from, to);
        }

        /// <inheritdoc />
        public override Infinity<DateTime> PositiveInfinity => new Infinity<DateTime>(DateTime.MaxValue);

        /// <inheritdoc />
        public override Infinity<DateTime> NegativeInfinity => new Infinity<DateTime>(DateTime.MinValue);
    }
}
