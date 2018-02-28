using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundary.Kind;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing a single period of time with a start date
    /// </summary>
    /// <typeparam name="TSet">the kind of <see cref="IPeriodSet"/> that contains this kind of period</typeparam>
    /// <typeparam name="TListBuilder">the kind of <see cref="IPeriodListBuilder{TPeriod,TStartingPeriod}"/> that will produce this kind of period</typeparam>
    /// <typeparam name="TPeriod">the type of this period</typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public abstract class StartingPeriod<TSet, TListBuilder, TStartingPeriod, TPeriod> : PeriodSet<TSet, TPeriod, TListBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// returns a typed version of this instance
        /// </summary>
        /// <returns></returns>
        protected abstract TPeriod GetPeriod();

        /// <summary>
        /// The start date of this period of time
        /// </summary>
        public DateTime Earliest { get; }

        /// <summary>
        /// Initializes a new <see cref="StartingPeriod{TSet,TListBuilder,TStartingPeriod,TPeriod}"/> with a given start date
        /// </summary>
        /// <param name="from"></param>
        protected StartingPeriod(DateTime from)
        {
            Earliest = from;
        }

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            return date >= Earliest;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Earliest;
        }

        /// <inheritdoc />
        public override IEnumerable<DateTime> Boundaries
        {
            get { yield return Earliest; }
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TPeriod nonEmpty)
        {
            nonEmpty = GetPeriod();
            return true;
        }

        /// <summary>
        /// A connected period of time is never empty
        /// </summary>
        public override bool IsEmpty => false;

        /// <summary>
        /// Namely, this one
        /// </summary>
        public override int PeriodCount => 1;

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            if (date == Earliest)
            {
                return new Start(Inclusivity.Inclusive);
            }
            if (date > Earliest)
            {
                return new Continuation();
            }

            return null;
        }

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector)
        {
            yield return selector(GetPeriod());
        }

        /// <inheritdoc />
        public override void ForEach(Action<TPeriod> what)
        {
            what(GetPeriod());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Earliest.GetHashCode();
        }
    }
}
