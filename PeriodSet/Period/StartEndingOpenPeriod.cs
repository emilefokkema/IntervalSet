using System;
using PeriodSet.Period.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an actual end date
    /// </summary>
    public class StartEndingOpenPeriod : DoubleBoundaryPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod,IOpenPeriod>, IOpenPeriod
    {
        /// <inheritdoc />
        public StartEndingOpenPeriod(Start from, End to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// This <see cref="IOpenPeriod"/> does have an end date
        /// </summary>
        public DateTime? To => Max.Date;

        /// <inheritdoc />
        public DateTime Earliest => Min.Date;
    }
}
