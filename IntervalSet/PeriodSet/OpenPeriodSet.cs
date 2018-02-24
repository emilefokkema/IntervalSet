using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// An <see cref="IPeriodSet"/> not all of the <see cref="INonEmptyPeriod" />s of which can be assumed to have an end date
    /// </summary>
    public class OpenPeriodSet : PeriodSet<OpenPeriodSet,NonEmptyOpenPeriodSet, OpenPeriodListBuilder,IOpenPeriod>
    {
        /// <inheritdoc />
        public OpenPeriodSet(IList<IOpenPeriod> list) : base(list)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="OpenPeriodSet"/> based on a given <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="set"></param>
        public OpenPeriodSet(IPeriodSet set):base(set)
        {
        }

        /// <inheritdoc />
        public OpenPeriodSet()
        {
        }

        /// <inheritdoc />
        public OpenPeriodSet(DateTime from) : base(from)
        {
        }

        /// <inheritdoc />
        public OpenPeriodSet(DateTime from, DateTime to) : base(from, to)
        { }

        /// <inheritdoc />
        public OpenPeriodSet(DateTime from, DateTime? to) : base(from, to)
        {
           
        }

        /// <summary>
        /// Loops through each of the <see cref="IOpenPeriod"/>s in this <see cref="OpenPeriodSet"/> and applies a given <see cref="Action{T,T}" /> to their respective start and end dates
        /// </summary>
        /// <param name="what"></param>
        public void ForEach(Action<DateTime, DateTime?> what)
        {
            ForEach(p => what(p.Earliest,p.To));
        }

        /// <summary>
        /// Projects each respective start and end date of the <see cref="IOpenPeriod"/>s in this <see cref="OpenPeriodSet"/> to a new form
        /// </summary>
        /// <typeparam name="T">The type of the value returned by the selector</typeparam>
        /// <param name="selector">A transform function to apply to each start and end date.</param>
        /// <returns></returns>
        public List<T> Select<T>(Func<DateTime, DateTime?, T> selector) where T : class
        {
            return Select(p => selector(p.Earliest, p.To)).ToList();
        }

        /// <inheritdoc />
        protected override NonEmptyOpenPeriodSet MakeNonEmptySet(IList<IOpenPeriod> list)
        {
            return new NonEmptyOpenPeriodSet(list);
        }

        /// <inheritdoc />
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
        }

        

        /// <summary>
        /// Returns an <see cref="OpenPeriodSet"/> representing the relative complement of <paramref name="other"/> in <paramref name="one"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static OpenPeriodSet operator -(OpenPeriodSet one, IPeriodSet other)
        {
            return one.Minus(other);
        }

        /// <summary>
        /// Returns an <see cref="OpenPeriodSet"/> representing the union of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static OpenPeriodSet operator +(OpenPeriodSet one, IPeriodSet other)
        {
            return one.Plus(other);
        }

        /// <summary>
        /// Returns an <see cref="OpenPeriodSet"/> representing the intersection of <paramref name="one"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static OpenPeriodSet operator *(OpenPeriodSet one, IPeriodSet other)
        {
            return one.Cross(other);
        }

        /// <summary>
        /// The entire <see cref="DateTime"/> space, represented as an <see cref="OpenPeriodSet"/> with <see cref="DateTime.MinValue"/> as start date
        /// </summary>
        public static readonly OpenPeriodSet All = new OpenPeriodSet(DateTime.MinValue);

        /// <summary>
        /// The empty set, represented as an empty <see cref="OpenPeriodSet"/>
        /// </summary>
        public static readonly OpenPeriodSet Empty = new OpenPeriodSet();

        /// <summary>
        /// Converts an <see cref="OpenPeriodSet"/> to a <see cref="BoundedPeriodSet"/>
        /// </summary>
        /// <param name="open"></param>
        public static implicit operator BoundedPeriodSet(OpenPeriodSet open)
        {
            return new BoundedPeriodSet(open);
        }

        /// <summary>
        /// Converts a non-empty <see cref="OpenPeriodSet"/> to a <see cref="NonEmptyOpenPeriodSet"/>
        /// </summary>
        /// <param name="open"></param>
        public static explicit operator NonEmptyOpenPeriodSet(OpenPeriodSet open)
        {
            return new NonEmptyOpenPeriodSet(open);
        }
    }
}
