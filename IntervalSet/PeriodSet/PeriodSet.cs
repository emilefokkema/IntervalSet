using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    public abstract class PeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod> : IEnumerablePeriodSet<TPeriod>, IPeriodSet<TSet>, IEmptyOrNot<TNonEmptySet>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : INonEmptyPeriod
    {
        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected abstract TSet MakeSet(IList<TPeriod> list);

        public virtual bool ContainsDate(DateTime date)
        {
            return false;
        }

        public virtual bool ContainsPeriod(DateTime from, DateTime to)
        {
            return false;
        }

        public virtual IEnumerable<DateTime> Boundaries { get { yield break; } }

        public TSet Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            new TListBuilder().InverseOfBoolean(list, Boundaries.Concat(changes ?? new List<DateTime>()).ToList(), b => ContainsDate(b) && trueFrom(b));
            return MakeSet(list);
        }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            new TListBuilder().InverseOfBoolean(list, Boundaries.Concat(changes ?? new List<DateTime>()).ToList(),
                (a, b) => ContainsPeriod(a, b) && trueEverywhereBetween(a, b));
            return MakeSet(list);
        }

        public TSet Minus(IPeriodSet other)
        {
            return Where(b => !other.ContainsDate(b), other.Boundaries.ToList());
        }

        public TSet Plus(IPeriodSet other)
        {
            IList<TPeriod> list = new List<TPeriod>();
            IList<DateTime> changes = Boundaries.Concat(other.Boundaries).ToList();
            new TListBuilder().InverseOfBoolean(list, changes, d => ContainsDate(d) || other.ContainsDate(d));
            return MakeSet(list);
        }

        public TSet Cross(IPeriodSet other)
        {
            return Where(other.ContainsDate, other.Boundaries.ToList());
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes)
        {
            return Where(trueFrom, changes);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes)
        {
            return Where(trueEverywhereBetween, changes);
        }

        IPeriodSet IPeriodSet.Minus(IPeriodSet other)
        {
            return Minus(other);
        }

        IPeriodSet IPeriodSet.Plus(IPeriodSet other)
        {
            return Plus(other);
        }

        IPeriodSet IPeriodSet.Cross(IPeriodSet other)
        {
            return Cross(other);
        }

        public virtual bool IsNonEmpty(out TNonEmptySet nonEmpty)
        {
            nonEmpty = default(TNonEmptySet);
            return false;
        }

        public virtual bool IsEmpty => true;

        public virtual int PeriodCount => 0;

        public virtual BoundaryKind Cross(DateTime date)
        {
            return BoundaryKind.None;
        }

        public bool Intersects(IPeriodSet other)
        {
            return !other.Cross(this).IsEmpty;
        }

        public virtual IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector) where TT : class
        {
            yield break;
        }

        public virtual void ForEach(Action<TPeriod> what)
        {
        }

        public bool Equals(IPeriodSet other)
        {
            if (other == null)
            {
                return false;
            }
            return Minus(other).IsEmpty && other.Minus(this).IsEmpty;
        }

        public override bool Equals(object other)
        {
            if (other is IPeriodSet)
            {
                return Equals((IPeriodSet)other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
