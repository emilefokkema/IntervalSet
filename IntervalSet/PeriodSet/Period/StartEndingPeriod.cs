using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public abstract class StartEndingPeriod<TSet, TListBuilder, TPeriod> : StartingPeriod<TSet, TListBuilder, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
    {
        protected DateTime Latest { get; }

        protected StartEndingPeriod(DateTime from, DateTime to):base(from)
        {
            Latest = to;
        }

        public override bool ContainsDate(DateTime date)
        {
            return date >= Earliest && date < Latest;
        }

        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Earliest && to <= Latest;
        }

        public override IEnumerable<DateTime> Boundaries
        {
            get
            {
                yield return Earliest;
                yield return Latest;
            }
        }

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

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latest.GetHashCode() * 397) ^ Earliest.GetHashCode();
            }
        }
    }
}
