using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet
{
    /// <summary>
    /// A subset of the <typeparamref name="T"/> space consisting of intervals
    /// </summary>
    public interface IIntervalSet<T> : IEquatable<IIntervalSet<T>>, IFormattable
        where T:IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Gets whether this subset of the <typeparamref name="T"/> space is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns an <see cref="IIntervalSet{T}"/> representing the relative complement of another <see cref="IIntervalSet{T}"/> in this one
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        IIntervalSet<T> Minus(IIntervalSet<T> other);

        /// <summary>
        /// Returns an <see cref="IIntervalSet{T}"/> representing the union of this <see cref="IIntervalSet{T}"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        IIntervalSet<T> Plus(IIntervalSet<T> other);

        /// <summary>
        /// Returns an <see cref="IIntervalSet{T}"/> representing the intersection of this <see cref="IIntervalSet{T}"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        IIntervalSet<T> Cross(IIntervalSet<T> other);

        /// <summary>
        /// Returns an <see cref="IIntervalSet{T}"/> representing the subset of this <see cref="IIntervalSet{T}"/> on which a given predicate is
        /// true. It accepts an optional list of <typeparamref name="T"/>s on which to update the value of the predicate.
        /// </summary>
        /// <param name="trueFrom">returns the updated value of the predicate on the given <typeparamref name="T"/></param>
        /// <param name="changes">a list of <typeparamref name="T"/>s on which to update the value of the predicate</param>
        /// <returns></returns>
        IIntervalSet<T> Where(Func<T,bool> trueFrom, IList<T> changes = null);

        /// <summary>
        /// Returns an <see cref="IIntervalSet{T}"/> representing the subset of this <see cref="IIntervalSet{T}"/> on which a given predicate is
        /// true. It accepts an optional list of <typeparamref name="T"/>s between which to evaluate the predicate
        /// </summary>
        /// <param name="trueEverywhereBetween">returns whether the predicate is true between two given <typeparamref name="T"/>s</param>
        /// <param name="changes">the interval boundaries</param>
        /// <returns></returns>
        IIntervalSet<T> Where(Func<T,T,bool> trueEverywhereBetween, IList<T> changes = null);

        /// <summary>
        /// Returns whether an interval from <paramref name="from"/> to <paramref name="to"/> is entirely contained in this
        /// <see cref="IIntervalSet{T}"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        bool ContainsInterval(T from, T to);

        /// <summary>
        /// Returns whether a given <typeparamref name="T"/> is in this subset of the <typeparamref name="T"/> space
        /// </summary>
        /// <param name="item">the given <typeparamref name="T"/></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// Returns true if this <see cref="IIntervalSet{T}"/> contains an <see cref="IIntervalSet{T}"/> containing an end and no start
        /// </summary>
        /// <returns></returns>
        bool ContainsNegativeInfinity();

        /// <summary>
        /// Returns whether <paramref name="location"/> is a start, an end, both or neither of this <see cref="IIntervalSet{T}"/>
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        BoundaryKind Cross(T location);

        /// <summary>
        /// Gets the <typeparamref name="T"/>s that are a start or an end of any of the intervals in this <see cref="IIntervalSet{T}"/>
        /// </summary>
        IEnumerable<Boundary<T>> Boundaries { get; }

        /// <summary>
        /// Returns whether this <see cref="IIntervalSet{T}"/> has a non-empty intersection with <paramref name="other"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool Intersects(IIntervalSet<T> other);
    }

    /// <summary>
    /// An <see cref="IIntervalSet{T}"/> for which the result of set-theoretical operations are of a particular type
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IIntervalSet<TSet, T> : IIntervalSet<T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the relative complement of another <see cref="IIntervalSet{T}"/> in this one
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        new TSet Minus(IIntervalSet<T> other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the union of this <see cref="IIntervalSet{T}"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        new TSet Plus(IIntervalSet<T> other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the intersection of this <see cref="IIntervalSet{T}"/> and another
        /// </summary>
        /// <param name="other">the other <see cref="IIntervalSet{T}"/></param>
        /// <returns></returns>
        new TSet Cross(IIntervalSet<T> other);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the subset of this <see cref="IIntervalSet{T}"/> on which a given predicate is
        /// true. It accepts an optional list of <typeparamref name="T"/>s on which to update the value of the predicate.
        /// </summary>
        /// <param name="trueFrom">returns the updated value of the predicate on the given <typeparamref name="T"/></param>
        /// <param name="changes">a list of <typeparamref name="T"/>s on which to update the value of the predicate</param>
        /// <returns></returns>
        new TSet Where(Func<T, bool> trueFrom, IList<T> changes = null);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> representing the subset of this <see cref="IIntervalSet{T}"/> on which a given predicate is
        /// true. It accepts an optional list of <typeparamref name="T"/>s between which to evaluate the predicate
        /// </summary>
        /// <param name="trueEverywhereBetween">return whether the predicate is true between two given boundaries</param>
        /// <param name="changes">the interval boundaries</param>
        /// <returns></returns>
        new TSet Where(Func<T, T, bool> trueEverywhereBetween, IList<T> changes = null);
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
