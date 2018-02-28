using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an actual end date
    /// </summary>
    public class StartEndingOpenPeriod : DoubleBoundedPeriod<OpenPeriodSet, OpenPeriodListBuilder, StartingOpenPeriod,IOpenPeriod>, IOpenPeriod
    {
        /// <inheritdoc />
        public StartEndingOpenPeriod(Boundary from, Boundary to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
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
