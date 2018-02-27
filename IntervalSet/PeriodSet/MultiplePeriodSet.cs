using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period.Boundary;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc cref="PeriodSet{TSet, TNonEmptySet, TListBuilder, TPeriod}"/>
    /// <summary>
    /// A subset of the <see cref="DateTime" /> space consisting of zero or more <see cref="IPeriodSet"/>s
    /// </summary>
    public abstract class MultiplePeriodSet<TSet,TNonEmptySet,TListBuilder,TPeriod> : PeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : IPeriodSet
    {
        /// <summary>
        /// The list of <typeparamref name="TPeriod"/>s for this instance
        /// </summary>
        protected IList<TPeriod> PeriodList { get; }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/>
        /// </summary>
        protected MultiplePeriodSet():this(new List<TPeriod>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> based on a given <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        protected MultiplePeriodSet(IPeriodSet set):this()
        {
            foreach (TPeriod period in new TListBuilder().InverseOfBoolean(set.Boundaries.ToList(), set.ContainsDate))
            {
                PeriodList.Add(period);
            }
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        protected MultiplePeriodSet(IList<TPeriod> list)
        {
            PeriodList = list;
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date and end date
        /// </summary>
        protected MultiplePeriodSet(DateTime from, DateTime to) : this()
        {
            PeriodList.Add(new TListBuilder().MakePeriod(from, to));
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date
        /// </summary>
        protected MultiplePeriodSet(DateTime from) : this()
        {
            PeriodList.Add(new TListBuilder().MakePeriod(from));
        }

        /// <summary>
        /// Returns a <typeparamref name="TNonEmptySet"/> based on a given non-empty list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected abstract TNonEmptySet MakeNonEmptySet(IList<TPeriod> list);
        

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (PeriodList.Count == 0)
            {
                return 0;
            }

            if (PeriodList.Count == 1)
            {
                return PeriodList.ElementAt(0).GetHashCode();
            }

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
        public override bool IsNonEmpty(out TNonEmptySet nonEmpty)
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
        public override bool IsEmpty => !PeriodList.Any();

        /// <inheritdoc />
        public override int PeriodCount => PeriodList.Count;

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            return PeriodList.Any(p => p.ContainsDate(date));
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            return PeriodList.Select(p => p.Cross(date)).FirstOrDefault(b => b != null);
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return PeriodList.Any(p => p.ContainsPeriod(from, to));
        }

        /// <inheritdoc />
        public override IEnumerable<DateTime> Boundaries => PeriodList.SelectMany(p => p.Boundaries);

        /// <inheritdoc cref="IEnumerablePeriodSet{TPeriod}.Select{TT}(Func{TPeriod,TT})"/>
        public override IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector)
        {
            return PeriodList.Select(selector);
        }

        /// <inheritdoc />
        public override void ForEach(Action<TPeriod> what)
        {
            foreach (TPeriod period in PeriodList)
            {
                what(period);
            }
        }
    }
}
