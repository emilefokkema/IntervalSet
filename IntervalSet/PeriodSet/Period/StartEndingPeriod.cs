using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundary;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing a single period of time with a start date and an end date
    /// </summary>
    public abstract class StartEndingPeriod<TSet, TListBuilder, TStartingPeriod, TPeriod> : StartingPeriod<TSet, TListBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
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
            if (date == Earliest && Latest == Earliest)
            {
                return new Degenerate();
            }
            if (date == Earliest)
            {
                return new Start(Inclusivity.Inclusive);
            }

            if (date > Earliest && date < Latest)
            {
                return new Continuation();
            }

            if (date == Latest)
            {
                return new End(Inclusivity.Exclusive);
            }

            return null;
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
