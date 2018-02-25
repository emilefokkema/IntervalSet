using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="BoundedPeriodSet"/> that contains at least one <see cref="IBoundedPeriod"/>
    /// </summary>
    public class NonEmptyBoundedPeriodSet : NonEmptyPeriodSet<BoundedPeriodSet, IBoundedPeriod, BoundedPeriodListBuilder, IBoundedPeriod>, IBoundedPeriod
    {
        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IPeriodSet set):base(set)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IList<IBoundedPeriod> list):base(list)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(DateTime from, DateTime? to = null):base(from,to)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod MakeNonEmptySet(IList<IBoundedPeriod> list)
        {
            return new NonEmptyBoundedPeriodSet(list);
        }

        /// <inheritdoc />
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        /// <summary>
        /// The end date of the last <see cref="IBoundedPeriod"/> in this <see cref="BoundedPeriodSet"/>
        /// </summary>
        public DateTime To => PeriodList.Last().To;

        public DateTime Earliest => PeriodList.First().Earliest;
    }
}
