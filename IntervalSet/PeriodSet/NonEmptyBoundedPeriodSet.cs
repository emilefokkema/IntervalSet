using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="BoundedPeriodSet"/> that contains at least one <see cref="IBoundedPeriod"/>
    /// </summary>
    public class NonEmptyBoundedPeriodSet : NonEmptyPeriodSet<BoundedPeriodSet, NonEmptyBoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod>
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
        protected override NonEmptyBoundedPeriodSet MakeNonEmptySet(IList<IBoundedPeriod> list)
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
        public DateTime Last => PeriodList.Last().To;
    }
}
