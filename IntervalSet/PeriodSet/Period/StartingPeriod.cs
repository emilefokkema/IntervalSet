using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public abstract class StartingPeriod<TSet, TListBuilder, TPeriod> : PeriodSet<TSet, TPeriod, TListBuilder, TPeriod>, INonEmptyPeriod
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : INonEmptyPeriod
    {
        protected abstract TPeriod GetPeriod();
        protected DateTime From { get; }

        protected StartingPeriod(DateTime from)
        {
            From = from;
        }

        public override bool ContainsDate(DateTime date)
        {
            return date >= From;
        }

        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= From;
        }

        public override IEnumerable<DateTime> Boundaries
        {
            get { yield return From; }
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
            if (date >= From)
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
            return From.GetHashCode();
        }

        DateTime INonEmptyPeriod.Earliest => From;
    }
}
