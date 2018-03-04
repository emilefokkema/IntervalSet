using System.Collections.Generic;
using PeriodSet.Period;
using PeriodSet.Period.Boundaries;

namespace PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : Builder<OpenPeriodSet, IOpenPeriod, IStartingOpenPeriod>
    {

        /// <inheritdoc />
        public override IStartingOpenPeriod MakeStartingPeriod(Start from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IStartingOpenPeriod MakeStartingPeriod()
        {
            return new EntireOpenPeriod();
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
