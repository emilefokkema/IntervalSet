using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using IntervalSet;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;

namespace PeriodSet
{
    /// <inheritdoc />
    /// <summary>
    /// An <see cref="IIntervalSet{T}" /> of <see cref="DateTime"/> in which each period has an end of type <see cref="DateTime"/> and positive infinity is represented as <see cref="DateTime.MaxValue"/>
    /// </summary>
    [Serializable]
    public class BoundedPeriodSet : DefaultIntervalSet<BoundedPeriodSet, BoundedPeriodListBuilder, DateTime>
    {
        /// <inheritdoc />
        public BoundedPeriodSet(IList<IDefaultInterval<DateTime>> list):base(list)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="BoundedPeriodSet"/> based on a given <see cref="IIntervalSet{T}"/> of <see cref="DateTime"/>
        /// </summary>
        /// <param name="set"></param>
        public BoundedPeriodSet(IIntervalSet<DateTime> set):base(set)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="BoundedPeriodSet"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public BoundedPeriodSet(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from, DateTime to) : base(new Start<DateTime>(from, Inclusivity.Inclusive), new End<DateTime>(to, Inclusivity.Exclusive))
        { }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from, DateTime? to = null)
        {
            Start<DateTime> start = new Start<DateTime>(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                if (to.Value == from)
                {
                    IntervalList.Add(new DefaultDegenerateInterval<DateTime>(new Degenerate<DateTime>(from)));
                }
                else
                {
                    IntervalList.Add(new DefaultStartEndingInterval<DateTime>(start, new End<DateTime>(to.Value, Inclusivity.Exclusive)));
                }
            }
            else
            {
                IntervalList.Add(new DefaultStartingInterval<DateTime>(start));
            }
        }

        /// <inheritdoc />
        public BoundedPeriodSet()
        {
        }

        /// <inheritdoc />
        public BoundedPeriodSet(DateTime from) : base(new Start<DateTime>(from, Inclusivity.Inclusive))
        {
        }

        /// <summary>
        /// Loops through each of the <see cref="IDefaultInterval{T}"/>s in this <see cref="BoundedPeriodSet"/> and applies a given <see cref="Action{T,T}" /> to their respective start and end dates
        /// </summary>
        /// <param name="what"></param>
        public void ForEach(Action<DateTime, DateTime> what)
        {
            ForEach(p => what(p.Start,p.End));
        }

        /// <summary>
        /// Projects each respective start and end date of the <see cref="IDefaultInterval{T}"/>s in this <see cref="BoundedPeriodSet"/> to a new form
        /// </summary>
        /// <typeparam name="T">The type of the value returned by the selector</typeparam>
        /// <param name="selector">A transform function to apply to each start and end date.</param>
        /// <returns></returns>
        public IEnumerable<T> Select<T>(Func<DateTime, DateTime, T> selector) where T : class
        {
            return Select(p => selector(p.Start, p.End));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the relative complement of <paramref name="other"/> in <paramref name="one"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator -(BoundedPeriodSet one, IIntervalSet<DateTime> other)
        {
            return one.Minus(other);
        }

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the union of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator +(BoundedPeriodSet one, IIntervalSet<DateTime> other)
        {
            return one.Plus(other);
        }

        /// <summary>
        /// Returns a <see cref="BoundedPeriodSet"/> representing the intersection of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static BoundedPeriodSet operator *(BoundedPeriodSet one, IIntervalSet<DateTime> other)
        {
            return one.Cross(other);
        }

        /// <summary>
        /// The entire <see cref="DateTime"/> space, represented as a <see cref="BoundedPeriodSet"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
        /// </summary>
        public static readonly BoundedPeriodSet All = new BoundedPeriodSet(new DefaultEntireInterval<DateTime>());

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
