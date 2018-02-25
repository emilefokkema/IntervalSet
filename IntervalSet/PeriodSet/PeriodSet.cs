using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc cref="IEnumerablePeriodSet{TPeriod}"/>
    /// <inheritdoc cref="IPeriodSet{TSet}"/>
    /// <inheritdoc cref="IEmptyOrNot{TNonEmpty}"/>
    /// <summary>
    /// A subset of the <see cref="DateTime" /> space consisting of zero or more <see cref="INonEmptyPeriod" />s
    /// </summary>
    public abstract class PeriodSet<TSet,TNonEmptySet,TListBuilder,TPeriod> : IEnumerablePeriodSet<TPeriod>, IPeriodSet<TSet>, IEmptyOrNot<TNonEmptySet>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : INonEmptyPeriod
    {
        /// <summary>
        /// The list of <typeparamref name="TPeriod"/>s for this instance
        /// </summary>
        protected IList<TPeriod> PeriodList { get; }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/>
        /// </summary>
        protected PeriodSet():this(new List<TPeriod>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> based on a given <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        protected PeriodSet(IPeriodSet set):this()
        {
            new TListBuilder().InverseOfBoolean(PeriodList, set.Boundaries.ToList(), set.ContainsDate);
        }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        protected PeriodSet(IList<TPeriod> list)
        {
            PeriodList = list;
        }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date and end date
        /// </summary>
        protected PeriodSet(DateTime from, DateTime to) : this(new List<TPeriod>())
        {
            new TListBuilder().Add(PeriodList, from, to);
        }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date and end date
        /// </summary>
        protected PeriodSet(DateTime from, DateTime? to) : this(new List<TPeriod>())
        {
            if (to.HasValue)
            {
                new TListBuilder().Add(PeriodList, from, to.Value);
            }
            else
            {
                new TListBuilder().Add(PeriodList, from);
            }
        }

        /// <summary>
        /// Initializes a new <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date
        /// </summary>
        protected PeriodSet(DateTime from) : this(new List<TPeriod>())
        {
            new TListBuilder().Add(PeriodList, from);
        }

        /// <summary>
        /// Returns a <typeparamref name="TNonEmptySet"/> based on a given non-empty list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected abstract TNonEmptySet MakeNonEmptySet(IList<TPeriod> list);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected abstract TSet MakeSet(IList<TPeriod> list);

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
            unchecked
            {
                int result = 17;
                foreach (TPeriod period in PeriodList)
                {
                    result = result * 31 + period.GetHashCode();
                }
                return result;
            }
        }


        /// <inheritdoc />
        public TSet Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            new TListBuilder().InverseOfBoolean(list, Boundaries.Concat(changes ?? new List<DateTime>()).ToList(), b => ContainsDate(b) && trueFrom(b));
            return MakeSet(list);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes)
        {
            return Where(trueFrom,changes);
        }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes = null)
        {
            IList<TPeriod> list = new List<TPeriod>();
            new TListBuilder().InverseOfBoolean(list, Boundaries.Concat(changes ?? new List<DateTime>()).ToList(),
                (a, b) => ContainsPeriod(a, b) && trueEverywhereBetween(a, b));
            return MakeSet(list);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes)
        {
            return Where(trueEverywhereBetween,changes);
        }

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the relative complement of another <see cref="IPeriodSet"/> in this one
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        public TSet Minus(IPeriodSet other)
        {
            return Where(b => !other.ContainsDate(b), other.Boundaries.ToList());
        }

        IPeriodSet IPeriodSet.Minus(IPeriodSet other)
        {
            return Minus(other);
        }

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the union of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        public TSet Plus(IPeriodSet other)
        {
            IList<TPeriod> list = new List<TPeriod>();
            IList<DateTime> changes = Boundaries.Concat(other.Boundaries).ToList();
            new TListBuilder().InverseOfBoolean(list, changes, d => ContainsDate(d) || other.ContainsDate(d));
            return MakeSet(list);
        }

        IPeriodSet IPeriodSet.Plus(IPeriodSet other)
        {
            return Plus(other);
        }

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the intersection of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        public TSet Cross(IPeriodSet other)
        {
            return Where(other.ContainsDate, other.Boundaries.ToList());
        }

        IPeriodSet IPeriodSet.Cross(IPeriodSet other)
        {
            return Cross(other);
        }

        /// <inheritdoc />
        public virtual bool IsNonEmpty(out TNonEmptySet nonEmpty)
        {
            if (PeriodList.Any())
            {
                nonEmpty = MakeNonEmptySet(PeriodList);
                return true;
            }
            nonEmpty = default(TNonEmptySet);
            return false;
        }
        

        /// <inheritdoc />
        public virtual bool IsEmpty => !PeriodList.Any();

        /// <inheritdoc />
        public virtual int PeriodCount => PeriodList.Count;

        /// <inheritdoc />
        public bool ContainsDate(DateTime date)
        {
            return PeriodList.Any(p => p.ContainsDate(date));
        }

        /// <inheritdoc />
        public BoundaryKind Cross(DateTime date)
        {
            return PeriodList.Aggregate(BoundaryKind.None, (b, p) => b | p.Cross(date));
        }

        /// <inheritdoc cref="IPeriodSet.ContainsPeriod"/>
        public bool ContainsPeriod(DateTime from, DateTime to)
        {
            return PeriodList.Any(p => p.ContainsPeriod(from, to));
        }

        /// <inheritdoc />
        public bool Intersects(IPeriodSet other)
        {
            return !other.Cross(this).IsEmpty;
        }

        /// <inheritdoc cref="IPeriodSet.Boundaries"/>
        public virtual IEnumerable<DateTime> Boundaries => PeriodList.SelectMany(p => p.Boundaries);

        /// <inheritdoc cref="IEnumerablePeriodSet{TPeriod}.Select{TT}(Func{TPeriod,TT})"/>
        public IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector) where TT : class
        {
            return PeriodList.Select(selector);
        }

        /// <inheritdoc cref="IEnumerablePeriodSet{TPeriod}.ForEach"/>
        public void ForEach(Action<TPeriod> what)
        {
            foreach (TPeriod period in PeriodList)
            {
                what(period);
            }
        }
    }
}
