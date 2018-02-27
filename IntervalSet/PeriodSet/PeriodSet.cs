using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundary;
using IntervalSet.PeriodSet.Period.Boundary.Kind;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A base class for implementations of <see cref="IPeriodSet"/>
    /// </summary>
    /// <typeparam name="TSet">the type of this implementation</typeparam>
    /// <typeparam name="TNonEmptySet">the non-empty version of this implementation</typeparam>
    /// <typeparam name="TListBuilder">the type of <see cref="IPeriodListBuilder{TPeriod}"/> for this implementation</typeparam>
    /// <typeparam name="TPeriod">the kind of connected period of time for this implementation</typeparam>
    public abstract class PeriodSet<TSet, TNonEmptySet, TListBuilder, TStartingPeriod, TPeriod> : IEnumerablePeriodSet<TPeriod>, IPeriodSet<TSet>, IEmptyOrNot<TNonEmptySet>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected abstract TSet MakeSet(IList<TPeriod> list);

        /// <inheritdoc />
        public virtual bool ContainsDate(DateTime date)
        {
            return false;
        }

        /// <inheritdoc />
        public virtual bool ContainsPeriod(DateTime from, DateTime to)
        {
            return false;
        }

        /// <inheritdoc />
        public virtual IEnumerable<DateTime> Boundaries { get { yield break; } }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            foreach (TPeriod period in new TListBuilder().InverseOfBoolean(Boundaries.Concat(changes ?? new List<DateTime>()).ToList(), b => ContainsDate(b) && trueFrom(b)))
            {
                list.Add(period);
            }
            return MakeSet(list);
        }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            foreach (TPeriod period in new TListBuilder().InverseOfBoolean(Boundaries.Concat(changes ?? new List<DateTime>()).ToList(),
                (a, b) => ContainsPeriod(a, b) && trueEverywhereBetween(a, b)))
            {
                list.Add(period);
            }
            return MakeSet(list);
        }

        /// <inheritdoc />
        public TSet Minus(IPeriodSet other)
        {
            return Where(b => !other.ContainsDate(b), other.Boundaries.ToList());
        }

        /// <inheritdoc />
        public TSet Plus(IPeriodSet other)
        {
            IList<TPeriod> list = new List<TPeriod>();
            IList<DateTime> changes = Boundaries.Concat(other.Boundaries).ToList();
            foreach (TPeriod period in new TListBuilder().InverseOfBoolean(changes, d => ContainsDate(d) || other.ContainsDate(d)))
            {
                list.Add(period);
            }
            return MakeSet(list);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public virtual bool IsNonEmpty(out TNonEmptySet nonEmpty)
        {
            nonEmpty = default(TNonEmptySet);
            return false;
        }

        /// <inheritdoc />
        public virtual bool IsEmpty => true;

        /// <inheritdoc />
        public virtual int PeriodCount => 0;

        /// <inheritdoc />
        public virtual BoundaryKind Cross(DateTime date)
        {
            return null;
        }

        /// <inheritdoc />
        public bool Intersects(IPeriodSet other)
        {
            return !other.Cross(this).IsEmpty;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector) where TT : class
        {
            yield break;
        }

        /// <inheritdoc />
        public virtual void ForEach(Action<TPeriod> what)
        {
        }

        /// <inheritdoc />
        public bool Equals(IPeriodSet other)
        {
            if (other == null)
            {
                return false;
            }
            return Minus(other).IsEmpty && other.Minus(this).IsEmpty;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other is IPeriodSet)
            {
                return Equals((IPeriodSet)other);
            }
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
