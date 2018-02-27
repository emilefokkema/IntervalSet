using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class BoundaryKind : IEquatable<BoundaryKind>
    {
        public Inclusivity Inclusivity { get; }

        public BoundaryDirection Direction { get; }

        protected BoundaryKind(BoundaryDirection direction, Inclusivity inclusivity)
        {
            Direction = direction;
            Inclusivity = inclusivity;
        }

        public BoundaryKind Minus(BoundaryKind other)
        {
            if (Equals(other))
            {
                return null;
            }
            return new BoundaryKind(MinusDirection(other.Direction), MinusInclusivity(other.Inclusivity));
        }

        private Inclusivity MinusInclusivity(Inclusivity other)
        {
            if (Inclusivity == Inclusivity.Inclusive && other == Inclusivity.Inclusive)
            {
                return Inclusivity.Exclusive;
            }

            return Inclusivity;
        }

        private BoundaryDirection MinusDirection(BoundaryDirection other)
        {
            BoundaryDirection result = Direction;
            if (Direction.HasFlag(BoundaryDirection.Start) && other.HasFlag(BoundaryDirection.Start))
            {
                result ^= BoundaryDirection.Start;
            }

            if (Direction.HasFlag(BoundaryDirection.End) && other.HasFlag(BoundaryDirection.End))
            {
                result ^= BoundaryDirection.End;
            }

            return  result;
        }

        public bool Equals(BoundaryKind other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Direction == Direction && other.Inclusivity == Inclusivity;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as BoundaryKind);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Inclusivity * 397) ^ (int)Direction;
            }
        }
    }
}
