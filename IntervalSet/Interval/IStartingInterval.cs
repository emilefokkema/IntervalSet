using System;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval
{
    /// <summary>
    /// An interval that has no end
    /// </summary>
    /// <typeparam name="TEndingInterval">the type of the interval when it does have an end</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IStartingInterval<TEndingInterval, T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Returns a <typeparamref name="TEndingInterval"/> that represents this interval with an end
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        TEndingInterval End(End<T> end);
    }
}
