using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <summary>
    /// Helps build up an <see cref="IIntervalSet{T}"/>
    /// </summary>
    /// <typeparam name="TInterval">the type of period to build</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<TInterval, T>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// Builds a list of <typeparamref name="TInterval"/>s given a list of <see cref="Boundary{T}"/>s
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="containsNegativeInfinity"></param>
        /// <returns></returns>
        IEnumerable<TInterval> Build<TBuilder>(IList<Boundary<T>> boundaries, bool containsNegativeInfinity) where TBuilder : IBuilder<TInterval, T>, new();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TInterval MakeStartingInterval<TBuilder>(Start<T> from) where TBuilder : IBuilder<TInterval, T>, new();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> ending at <paramref name="end"/>
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        TInterval MakeEndingInterval<TBuilder>(End<T> end) where TBuilder : IBuilder<TInterval, T>, new();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> starting at <paramref name="from"/> and ending at <paramref name="to"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        TInterval MakeStartEndingInterval<TBuilder>(Start<T> from, End<T> to) where TBuilder : IBuilder<TInterval, T>, new();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> "starting" at negative infinity
        /// </summary>
        /// <returns></returns>
        TInterval MakeStartingInterval<TBuilder>() where TBuilder : IBuilder<TInterval, T>, new();

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> that represents an interval containing only one <typeparamref name="T"/>
        /// </summary>
        /// <param name="degenerate"></param>
        /// <returns></returns>
        TInterval MakeDegenerate<TBuilder>(Degenerate<T> degenerate) where TBuilder : IBuilder<TInterval, T>, new();

        Infinity<T> NegativeInfinity { get; }

        Infinity<T> PositiveInfinity { get; }
    }
}
