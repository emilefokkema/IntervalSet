using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// An <see cref="OpenPeriodSet"/> that contains at least one <see cref="IOpenPeriod"/>
    /// </summary>
    public class NonEmptyOpenPeriodSet : NonEmptyPeriodSet<OpenPeriodSet, NonEmptyOpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod>
    {
        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(IPeriodSet set):base(set)
        {
        }

        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(IList<IOpenPeriod> list):base(list)
        {
            
        }

        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(DateTime from, DateTime? to = null) : base(from, to)
        {
        }

        /// <inheritdoc />
        protected override NonEmptyOpenPeriodSet MakeNonEmptySet(IList<IOpenPeriod> list)
        {
            return new NonEmptyOpenPeriodSet(list);
        }

        /// <inheritdoc />
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
        }

        /// <summary>
        /// De end date (if any) of the last <see cref="IOpenPeriod"/> in this <see cref="NonEmptyOpenPeriodSet"/>
        /// </summary>
        public DateTime? Last => PeriodList.Last().To;
    }
}
