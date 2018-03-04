using System;
using System.Collections.Generic;
using PeriodSet.Period.Boundaries;
using PeriodSet.Period.Boundaries.Kind;

namespace PeriodSet
{
    /// <summary>
    /// A subset of the <see cref="DateTime"/> space consisting of connected periods of time that each have a start date
    /// (inclusive)
    /// </summary>
    public interface IPeriodSet : IEquatable<IPeriodSet>, IFormattable
    {
        /// <summary>
        /// Gets whether this subset of the <see cref="DateTime"/> space is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns an <see cref="IPeriodSet"/> representing the relative complement of another <see cref="IPeriodSet"/> in this one
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        IPeriodSet Minus(IPeriodSet other);

        /// <summary>
        /// Returns an <see cref="IPeriodSet"/> representing the union of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        IPeriodSet Plus(IPeriodSet other);

        /// <summary>
        /// Returns an <see cref="IPeriodSet"/> representing the intersection of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        IPeriodSet Cross(IPeriodSet other);

        /// <summary>
        /// Returns an <see cref="IPeriodSet"/> representing the subset of this <see cref="IPeriodSet"/> on which a given predicate is
        /// true. It accepts an optional list of dates on which to update the value of the predicate.
        /// </summary>
        /// <param name="trueFrom">returns the updated value of the predicate on the given date</param>
        /// <param name="changes">a list of dates on which to update the value of the predicate</param>
        /// <returns></returns>
        IPeriodSet Where(Func<DateTime,bool> trueFrom, IList<DateTime> changes = null);

        /// <summary>
        /// Returns an <see cref="IPeriodSet"/> representing the subset of this <see cref="IPeriodSet"/> on which a given predicate is
        /// true. It accepts an optional list of dates between which to evaluate the predicate
        /// </summary>
        /// <param name="trueEverywhereBetween">return whether the predicate is true between two given boundaries</param>
        /// <param name="changes">the period boundaries</param>
        /// <returns></returns>
        IPeriodSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IList<DateTime> changes = null);

        /// <summary>
        /// Returns whether an interval from <paramref name="from"/> to <paramref name="to"/> (exclusive) is entirely contained in this
        /// <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        bool ContainsPeriod(DateTime from, DateTime to);

        /// <summary>
        /// Returns whether a given date is in this subset of the <see cref="DateTime"/> space
        /// </summary>
        /// <param name="date">the given date</param>
        /// <returns></returns>
        bool ContainsDate(DateTime date);

        /// <summary>
        /// Returns true if this <see cref="IPeriodSet"/> contains an <see cref="IPeriodSet"/> containing and end date and no start date
        /// </summary>
        /// <returns></returns>
        bool ContainsNegativeInfinity();

        /// <summary>
        /// Returns whether <paramref name="date"/> is a start, an end, both or neither of this <see cref="IPeriodSet"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        BoundaryKind Cross(DateTime date);

        /// <summary>
        /// Gets the <see cref="DateTime"/>s that are a start date or an end date of any of the connected periods in this <see cref="IPeriodSet"/>
        /// </summary>
        IEnumerable<Boundary> Boundaries { get; }

        /// <summary>
        /// Returns whether this <see cref="IPeriodSet"/> has a non-empty intersection with <paramref name="other"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool Intersects(IPeriodSet other);

    }

    /// <summary>
    /// An <see cref="IPeriodSet"/> for which return values of set operations are of a particular type
    /// </summary>
    /// <typeparam name="TSet">The type of the return value of a set operation on this <see cref="IPeriodSet"/></typeparam>
    public interface IPeriodSet<out TSet> : IPeriodSet
    {
        /// <summary>
        /// Returns an <typeparamref name="TSet"/> representing the relative complement of another <see cref="IPeriodSet"/> in this one
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        new TSet Minus(IPeriodSet other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the union of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        new TSet Plus(IPeriodSet other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the intersection of this <see cref="IPeriodSet"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IPeriodSet"/></param>
        /// <returns></returns>
        new TSet Cross(IPeriodSet other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the subset of this <see cref="IPeriodSet"/> on which a given predicate is
        /// true. It accepts an optional list of dates on which to update the value of the predicate.
        /// </summary>
        /// <param name="trueFrom">returns the updated value of the predicate on the given date</param>
        /// <param name="changes">a list of dates on which to update the value of the predicate</param>
        /// <returns></returns>
        new TSet Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes = null);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the subset of this <see cref="IPeriodSet"/> on which a given predicate is
        /// true. It accepts an optional list of dates between which to evaluate the predicate
        /// </summary>
        /// <param name="trueEverywhereBetween">return whether the predicate is true between two given boundaries</param>
        /// <param name="changes">the period boundaries</param>
        /// <returns></returns>
        new TSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IList<DateTime> changes = null);
    }

    /// <summary>
    /// A thing that is either empty or not empty
    /// </summary>
    /// <typeparam name="TNonEmpty">The type representing a non-empty version of this instance</typeparam>
    public interface IEmptyOrNot<TNonEmpty>
    {
        /// <summary>
        /// Returns whether this instance is non-empty and if so, sets a <typeparamref name="TNonEmpty"/> to <paramref name="nonEmpty"/>
        /// </summary>
        /// <param name="nonEmpty"></param>
        /// <returns></returns>
        bool IsNonEmpty(out TNonEmpty nonEmpty);
    }
}
