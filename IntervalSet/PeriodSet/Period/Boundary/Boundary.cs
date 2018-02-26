using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public abstract class Boundary : IEquatable<Boundary>
    {
        public BoundaryKind Kind { get; }

        public DateTime Date { get; }

        protected Boundary(DateTime date, BoundaryKind kind)
        {
            Date = date;
            Kind = kind;
        }

        public bool Equals(Boundary other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Date == Date && other.Kind.Equals(Kind);
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
                return ((Kind != null ? Kind.GetHashCode() : 0) * 397) ^ Date.GetHashCode();
            }
        }
    }
}
