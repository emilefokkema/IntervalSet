using System;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// Positive infinity represented as a <see cref="Boundary{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PositiveInfinity<T> : Boundary<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="PositiveInfinity{T}"/> based on the <typeparamref name="T"/> that is positive infinity
        /// </summary>
        /// <param name="positiveInfinity"></param>
        public PositiveInfinity(T positiveInfinity):base(positiveInfinity, new EndKind(Inclusivity.Exclusive))
        {
        }
    }
}
