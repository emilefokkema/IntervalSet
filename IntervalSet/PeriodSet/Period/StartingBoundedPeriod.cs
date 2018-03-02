using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <see cref="DateTime.MaxValue"/> as end date (i.e. no end date)
    /// </summary>
    public class StartingBoundedPeriod : SingleBoundedPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, StartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod, IStartingPeriod<IBoundedPeriod>
    {
        /// <inheritdoc />
        public StartingBoundedPeriod(Boundary from):base(from)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IBoundedPeriod End(DateTime date)
        {
            return new StartEndingBoundedPeriod(Boundary, new Boundary(date, new EndKind(Inclusivity.Exclusive)));
        }

        /// <inheritdoc />
        public IBoundedPeriod End(Boundary end)
        {
            return new StartEndingBoundedPeriod(Boundary, end);
        }

        /// <inheritdoc />
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        /// <inheritdoc />
        /// <summary>
        /// Positive infinity represted as a <see cref="DateTime"/>
        /// </summary>
        public DateTime To => DateTime.MaxValue;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;
    }
}
