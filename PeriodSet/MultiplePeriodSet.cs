using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace PeriodSet
{
    /// <inheritdoc cref="PeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/>
    /// <summary>
    /// A subset of the <see cref="DateTime" /> space consisting of zero or more <see cref="IPeriodSet"/>s
    /// </summary>
    public abstract class MultiplePeriodSet<TSet,TBuilder, TStartingPeriod,TPeriod> : EmptyPeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod, DateTime>, new()
        where TPeriod : IPeriodSet
        where TStartingPeriod : class, TPeriod, IStartingInterval<TPeriod, DateTime>
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return PeriodList.Any(p => p.ContainsNegativeInfinity());
        }

        /// <summary>
        /// The list of <typeparamref name="TPeriod"/>s for this instance
        /// </summary>
        protected IList<TPeriod> PeriodList { get; }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/>
        /// </summary>
        protected MultiplePeriodSet():this(new List<TPeriod>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/> based on a given <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        protected MultiplePeriodSet(IPeriodSet set):this()
        {
            TStartingPeriod start = set.ContainsNegativeInfinity() ? Builder.MakeStartingInterval() : null;
            PeriodList = Builder.Build(set.Boundaries.ToList(), start).ToList();
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        protected MultiplePeriodSet(IList<TPeriod> list)
        {
            PeriodList = list;
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date and end date
        /// </summary>
        protected MultiplePeriodSet(Start<DateTime> from, End<DateTime> to) : this()
        {
            if (from.Location == to.Location)
            {
                PeriodList.Add(Builder.MakeDegenerate(new Degenerate<DateTime>(from.Location)));
            }
            else
            {
                PeriodList.Add(Builder.MakeStartingInterval(from).End(to));
            }
        }

        /// <summary>
        /// Initializes a new <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/> containing a <typeparamref name="TPeriod"/> with a given start date
        /// </summary>
        protected MultiplePeriodSet(Start<DateTime> from) : this()
        {
            PeriodList.Add(Builder.MakeStartingInterval(from));
        }
        
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
        public override string ToString(string format, IFormatProvider provider)
        {
            if (PeriodList.Count == 0)
            {
                return base.ToString(format, provider);
            }

            return string.Join(" + ", PeriodList.Select(p => p.ToString(format, provider)));
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TPeriod nonEmpty)
        {
            if (PeriodList.Any())
            {
                nonEmpty = Builder.MakeNonEmptySet(PeriodList);
                return true;
            }
            nonEmpty = default(TPeriod);
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
        public override IEnumerable<Boundary<DateTime>> Boundaries => PeriodList.SelectMany(p => p.Boundaries);

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
