using System;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    public class NegativeInfinity<T> : Boundary<T>
        where T : IEquatable<T>
    {
        public NegativeInfinity(T negativeInfinity):base(negativeInfinity, new StartKind(Inclusivity.Exclusive))
        {
        }

        public static implicit operator NegativeInfinity<T>(T negativeInfinity)
        {
            return new NegativeInfinity<T>(negativeInfinity);
        }
    }
}
