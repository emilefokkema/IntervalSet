using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// An <see cref="OpenPeriodSet"/> that contains at least one <see cref="IOpenPeriod"/>
    /// </summary>
    public class NonEmptyOpenPeriodSet : NonEmptyPeriodSet<OpenPeriodSet, IOpenPeriod, OpenPeriodListBuilder, StartingOpenPeriod, IOpenPeriod>, IOpenPeriod
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
        public NonEmptyOpenPeriodSet(DateTime from, DateTime? to = null):base(MakePeriod(from, to))
        {
        }

        private static IOpenPeriod MakePeriod(DateTime from, DateTime? to)
        {
            if (to.HasValue)
            {
                return new StartEndingOpenPeriod(from, to.Value);
            }
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        protected override IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> list)
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
        public DateTime? To => PeriodList.Last().To;

        public DateTime Earliest => PeriodList.First().Earliest;
    }
}
