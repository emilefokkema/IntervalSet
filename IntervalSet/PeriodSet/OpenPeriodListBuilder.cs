using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : Builder<OpenPeriodSet, IOpenPeriod, StartingOpenPeriod>
    {

        /// <inheritdoc />
        public override StartingOpenPeriod MakeStartingPeriod(Start from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate(Degenerate degenerate)
        {
            return new DegenerateOpenPeriod(degenerate);
        }

        /// <inheritdoc />
        public override OpenPeriodSet MakeSet(IList<IOpenPeriod> periods)
        {
            return new OpenPeriodSet(periods);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> list)
        {
            return new NonEmptyOpenPeriodSet(list);
        }
    }
}
