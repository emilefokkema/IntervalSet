using System;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// Negative infinity represented as a <see cref="Boundary{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NegativeInfinity<T> : Boundary<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="NegativeInfinity{T}"/> based on the <typeparamref name="T"/> that is negative infinity
        /// </summary>
        /// <param name="negativeInfinity"></param>
        public NegativeInfinity(T negativeInfinity):base(negativeInfinity, new StartKind(Inclusivity.Exclusive))
        {
        }
    }
}
