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
        /// The start of this interval (or some <see cref="Infinity{T}.Value"/>)
        /// </summary>
        T Start { get; }

        /// <summary>
        /// The end of this interval (or some <see cref="Infinity{T}.Value"/>)
        /// </summary>
        T End { get; }
    }
}
