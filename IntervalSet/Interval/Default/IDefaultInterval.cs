using System;
using IntervalSet.Interval.Boundaries;

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
        /// The start of this interval
        /// </summary>
        Boundary<T> StartingBoundary { get; }

        /// <summary>
        /// The start of this interval
        /// </summary>
        T Start { get; }

        /// <summary>
        /// The end of this interval
        /// </summary>
        Boundary<T> EndingBoundary { get; }

        /// <summary>
        /// The end of this interval
        /// </summary>
        T End { get; }
    }
}
