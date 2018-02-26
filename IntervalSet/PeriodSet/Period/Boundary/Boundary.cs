using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public abstract class Boundary : IEquatable<Boundary>
    {
        public Inclusivity Inclusivity { get; }

        public DateTime Date { get; }

        public BoundaryKind Kind { get; }

        protected Boundary(DateTime date, BoundaryKind kind, Inclusivity inclusivity)
        {
            Date = date;
            Kind = kind;
            Inclusivity = inclusivity;
        }

        public bool Equals(Boundary other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Date == Date && other.Inclusivity == Inclusivity && other.Kind == Kind;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(other as Boundary);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Inclusivity;
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Kind;
                return hashCode;
            }
        }
    }
}
