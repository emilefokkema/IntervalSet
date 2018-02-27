using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    /// <summary>
    /// An <see cref="IPeriodSet" /> in which each period has an end of type <see cref="DateTime"/> and positive infinity is represented as <see cref="DateTime.MaxValue"/>
    /// </summary>
    public class BoundedPeriodSet : MultiplePeriodSet<BoundedPeriodSet,NonEmptyBoundedPeriodSet, BoundedPeriodListBuilder,StartingBoundedPeriod, IBoundedPeriod>
    {
        /// <inheritdoc />
        public BoundedPeriodSet(IList<IBoundedPeriod> list):base(list)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="BoundedPeriodSet"/> based on a given <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        public BoundedPeriodSet(IPeriodSet set):base(set)
        {
        }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from, DateTime to) : base(from, to)
        { }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from, DateTime? to = null)
        {
            if (to.HasValue)
            {
                PeriodList.Add(new StartEndingBoundedPeriod(from, to.Value));
            }
            else
            {
                PeriodList.Add(new StartingBoundedPeriod(from));
            }
        }

        /// <inheritdoc />
        public BoundedPeriodSet()
        {
        }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from) : base(from)
        {
        }

        

        /// <summary>
        /// Loops through each of the <see cref="IBoundedPeriod"/>s in this <see cref="BoundedPeriodSet"/> and applies a given <see cref="Action{T,T}" /> to their respective start and end dates
        /// </summary>
        /// <param name="what"></param>
        public void ForEach(Action<DateTime, DateTime> what)
        {
            ForEach(p => what(p.Earliest,p.To));
        }

        /// <summary>
        /// Projects each respective start and end date of the <see cref="IBoundedPeriod"/>s in this <see cref="BoundedPeriodSet"/> to a new form
        /// </summary>
        /// <typeparam name="T">The type of the value returned by the selector</typeparam>
        /// <param name="selector">A transform function to apply to each start and end date.</param>
        /// <returns></returns>
        public IEnumerable<T> Select<T>(Func<DateTime, DateTime, T> selector) where T : class
        {
            return Select(p => selector(p.Earliest, p.To));
        }

        /// <inheritdoc />
        protected override NonEmptyBoundedPeriodSet MakeNonEmptySet(IList<IBoundedPeriod> list)
        {
            return new NonEmptyBoundedPeriodSet(list);
        }

        /// <inheritdoc />
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the relative complement of <paramref name="other"/> in <paramref name="one"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator -(BoundedPeriodSet one, IPeriodSet other)
        {
            return one.Minus(other);
        }

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the union of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator +(BoundedPeriodSet one, IPeriodSet other)
        {
            return one.Plus(other);
        }

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the intersection of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator *(BoundedPeriodSet one, IPeriodSet other)
        {
            return one.Cross(other);
        }

        /// <summary>
        /// The entire <see cref="DateTime"/> space, represented as a <see cref="BoundedPeriodSet"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
        /// </summary>
        public static readonly BoundedPeriodSet All = new BoundedPeriodSet(DateTime.MinValue);

        /// <summary>
        /// The empty set, represented as an empty <see cref="BoundedPeriodSet"/>
        /// </summary>
        public static readonly BoundedPeriodSet Empty = new BoundedPeriodSet();

        /// <summary>
        /// Converts a <see cref="BoundedPeriodSet"/> to a <see cref="OpenPeriodSet"/>
        /// </summary>
        /// <param name="bounded"></param>
        public static implicit operator OpenPeriodSet(BoundedPeriodSet bounded)
        {
            return new OpenPeriodSet(bounded);
        }

        /// <summary>
        /// Converts a non-empty <see cref="BoundedPeriodSet"/> to a <see cref="NonEmptyBoundedPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        public static explicit operator NonEmptyBoundedPeriodSet(BoundedPeriodSet set)
        {
            return new NonEmptyBoundedPeriodSet(set);
        }
    }
}
