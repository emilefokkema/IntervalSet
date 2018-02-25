using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing a single period of time with a start date and an end date
    /// </summary>
    public abstract class StartEndingPeriod<TSet, TListBuilder, TPeriod> : StartingPeriod<TSet, TListBuilder, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
    {
        /// <summary>
        /// The end date of this period
        /// </summary>
        protected DateTime Latest { get; }

        /// <summary>
        /// Initializes a new <see cref="StartEndingPeriod{TSet,TListBuilder,TPeriod}"/> with a given start date and end date
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        protected StartEndingPeriod(DateTime from, DateTime to):base(from)
        {
            Latest = to;
        }

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            return date >= Earliest && date < Latest;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Earliest && to <= Latest;
        }

        /// <inheritdoc />
        public override IEnumerable<DateTime> Boundaries
        {
            get
            {
                yield return Earliest;
                yield return Latest;
            }
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            if (ContainsDate(date))
            {
                return BoundaryKind.Start;
            }

            if (Earliest == Latest && date == Latest)
            {
                return BoundaryKind.Start | BoundaryKind.End;
            }

            if (date == Latest)
            {
                return BoundaryKind.End;
            }

            return BoundaryKind.None;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Latest.GetHashCode() * 397) ^ Earliest.GetHashCode();
            }
        }
    }
}
