using System;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    public class PositiveInfinity<T> : Boundary<T>
        where T : IEquatable<T>
    {
        public PositiveInfinity(T positiveInfinity):base(positiveInfinity, new EndKind(Inclusivity.Exclusive))
        {
        }

        public static implicit operator PositiveInfinity<T>(T positiveInfinity)
        {
            return new PositiveInfinity<T>(positiveInfinity);
        }
    }
}
