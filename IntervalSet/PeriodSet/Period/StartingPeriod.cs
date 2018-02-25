using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public abstract class StartingPeriod<TSet, TListBuilder, TPeriod> : PeriodSet<TSet, TPeriod, TListBuilder, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
    {
        protected abstract TPeriod GetPeriod();
        public DateTime Earliest { get; }

        protected StartingPeriod(DateTime from)
        {
            Earliest = from;
        }

        public override bool ContainsDate(DateTime date)
        {
            return date >= Earliest;
        }

        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Earliest;
        }

        public override IEnumerable<DateTime> Boundaries
        {
            get { yield return Earliest; }
        }

        public override bool IsNonEmpty(out TPeriod nonEmpty)
        {
            nonEmpty = GetPeriod();
            return true;
        }

        public override bool IsEmpty => false;

        public override int PeriodCount => 1;

        public override BoundaryKind Cross(DateTime date)
        {
            if (date >= Earliest)
            {
                return BoundaryKind.Start;
            }

            return BoundaryKind.None;
        }

        public override IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector)
        {
            yield return selector(GetPeriod());
        }

        public override void ForEach(Action<TPeriod> what)
        {
            what(GetPeriod());
        }

        public override int GetHashCode()
        {
            return Earliest.GetHashCode();
        }
    }
}
