using System;
using System.Collections.Generic;

namespace IntervalSet
{
    /// <summary>
    /// Builds <see cref="IIntervalSet{T}"/>s based on <typeparamref name="TInterval"/>s
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TInterval"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface ISetBuilder<out TSet, TInterval, T>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        TSet MakeSet(IList<TInterval> intervals);

        /// <summary>
        /// Returns a <typeparamref name="TInterval"/> representing the interval span of a list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        TInterval MakeNonEmptySet(IList<TInterval> intervals);
    }
}
