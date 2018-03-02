using System;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : PeriodListBuilder<IOpenPeriod, StartingOpenPeriod>
    {
        /// <inheritdoc />
        public override Start MakeStartingBoundary(DateTime from)
        {
            return new Start(from, Inclusivity.Inclusive);
        }

        /// <inheritdoc />
        public override End MakeEndingBoundary(DateTime to)
        {
            return new End(to, Inclusivity.Exclusive);
        }

        /// <inheritdoc />
        public override StartingOpenPeriod MakeStartingPeriod(Boundary from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate(Degenerate degenerate)
        {
            return new StartingOpenPeriod(degenerate);
        }
    }
}
