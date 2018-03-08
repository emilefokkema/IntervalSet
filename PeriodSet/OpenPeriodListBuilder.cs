using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : IntervalBuilder<IOpenPeriod, DateTime>, ISetBuilder<OpenPeriodSet, IOpenPeriod, DateTime>
    {
        public OpenPeriodSet MakeSet(IList<IOpenPeriod> intervals)
        {
            return new OpenPeriodSet(intervals);
        }

        public IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> intervals)
        {
            return new NonEmptyOpenPeriodSet(intervals);
        }
        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval(Start<DateTime> from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval()
        {
            return new EntireOpenPeriod();
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate(Degenerate<DateTime> degenerate)
        {
            return new DegenerateOpenPeriod(degenerate);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeEndingInterval(End<DateTime> end)
        {
            return new EndingOpenPeriod(end);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartEndingInterval(Start<DateTime> from, End<DateTime> to)
        {
            return new StartEndingOpenPeriod(from, to);
        }

        /// <inheritdoc />
        protected override DateTime PositiveInfinity => DateTime.MaxValue;

        /// <inheritdoc />
        protected override DateTime NegativeInfinity => DateTime.MinValue;
    }
}
