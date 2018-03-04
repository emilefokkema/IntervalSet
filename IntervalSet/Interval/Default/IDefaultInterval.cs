using System;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default appearance of an interval of the <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDefaultInterval<T> : IIntervalSet<T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Whether this interval has a start
        /// </summary>
        bool HasStart { get; }

        /// <summary>
        /// The start of this interval (if <see cref="HasStart"/> returns <c>true</c>)
        /// </summary>
        T Start { get; }

        /// <summary>
        /// Whether this interval has an end
        /// </summary>
        bool HasEnd { get; }

        /// <summary>
        /// The end of this interval (if <see cref="HasEnd"/> returns <c>true</c>)
        /// </summary>
        T End { get; }
    }
}
