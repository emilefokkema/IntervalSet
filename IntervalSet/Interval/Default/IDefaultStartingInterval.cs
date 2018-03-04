using System;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// The default appearance of an <see cref="IDefaultInterval{T}"/> that has no end
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDefaultStartingInterval<T> : IStartingInterval<IDefaultInterval<T>, T>, IDefaultInterval<T>
        where T : IComparable<T>, IEquatable<T>
    {
    }
}
