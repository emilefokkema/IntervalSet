using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <summary>
    /// Helps build up an <see cref="IIntervalSet{T}"/>
    /// </summary>
    /// <typeparam name="TInterval">the type of period to build</typeparam>
    /// <typeparam name="TStartingInterval">the type of period to start with</typeparam>
    /// <typeparam name="TSet">the type of <see cref="IIntervalSet{T}"/> to build</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<TSet, TInterval, TStartingInterval, T>
        where T : IEquatable<T>, IFormattable
        where TStartingInterval : TInterval, IStartingInterval<TInterval, T>
    {
        /// <summary>
        /// Builds a list of <typeparamref name="TInterval"/>s given a list of <see cref="Boundary{T}"/>s
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="currentInterval"></param>
        /// <returns></returns>
        IEnumerable<TInterval> Build(IList<Boundary<T>> boundaries, TStartingInterval currentInterval);

        /// <summary>
        /// Returns a <typeparamref name="TStartingInterval"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TStartingInterval MakeStartingInterval(Start<T> from);

        /// <summary>
        /// Returns a <typeparamref name="TStartingInterval"/> "starting" at negative infinity
        /// </summary>
        /// <returns></returns>
        TStartingInterval MakeStartingInterval();

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
