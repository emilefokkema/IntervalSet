using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <summary>
    /// Helps build up an <see cref="IIntervalSet{T}"/>
    /// </summary>
    /// <typeparam name="TInterval">the type of period to build</typeparam>
    /// <typeparam name="TSet">the type of <see cref="IIntervalSet{T}"/> to build</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<TSet, TInterval, T>
        where T : IEquatable<T>, IComparable<T>
        where TInterval : class
    {
        /// <summary>
        /// Builds a list of <typeparamref name="TInterval"/>s given a list of <see cref="Boundary{T}"/>s
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="containsNegativeInfinity"></param>
        /// <returns></returns>
        IEnumerable<TInterval> Build(IList<Boundary<T>> boundaries, bool containsNegativeInfinity);

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TInterval MakeStartingInterval(Start<T> from);

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> ending at <paramref name="end"/>
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        TInterval MakeEndingInterval(End<T> end);

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> starting at <paramref name="from"/> and ending at <paramref name="to"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        TInterval MakeStartEndingInterval(Start<T> from, End<T> to);


        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> "starting" at negative infinity
        /// </summary>
        /// <returns></returns>
        TInterval MakeStartingInterval();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> that represents an interval containing only one <typeparamref name="T"/>
        /// </summary>
        /// <param name="degenerate"></param>
        /// <returns></returns>
        TInterval MakeDegenerate(Degenerate<T> degenerate);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a given list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        TSet MakeSet(IList<TInterval> intervals);

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> based on a given non-empty list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        TInterval MakeNonEmptySet(IList<TInterval> list);
    }
}
