using System;
using System.Collections.Generic;

namespace IntervalSet
{
    /// <summary>
    /// An <see cref="IIntervalSet{T}"/> that contains methods for looping and selecting
    /// </summary>
    /// <typeparam name="TInterval">The type representing the intervals in this <see cref="IIntervalSet{T}"/></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IEnumerableIntervalSet<TInterval, T> : IIntervalSet<T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Projects each <typeparamref name="TInterval"/> of this <see cref="IIntervalSet{T}"/> to a new form
        /// </summary>
        /// <typeparam name="TT">The type of the value returned by the selector</typeparam>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns></returns>
        IEnumerable<TT> Select<TT>(Func<TInterval, TT> selector) where TT : class;

        /// <summary>
        /// Loops through each of the <typeparamref name="TInterval"/>s in this <see cref="IIntervalSet{T}"/> and applies a given <see cref="Action{T}"/> to each
        /// </summary>
        /// <param name="what"></param>
        void ForEach(Action<TInterval> what);

        /// <summary>
        /// Gets the number of intervals in this <see cref="IIntervalSet{T}"/>
        /// </summary>
        int IntervalCount { get; }
    }
}
