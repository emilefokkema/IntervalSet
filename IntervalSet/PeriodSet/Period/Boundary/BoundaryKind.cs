using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// A kind of <see cref="Boundary"/> of an <see cref="IPeriodSet"/>
    /// </summary>
    public class BoundaryKind : IEquatable<BoundaryKind>
    {
        /// <summary>
        /// The <see cref="Inclusivity"/> of this kind
        /// </summary>
        public Inclusivity Inclusivity { get; }

        /// <summary>
        /// The <see cref="BoundaryDirection"/> of this kind
        /// </summary>
        public BoundaryDirection Direction { get; }

        /// <summary>
        /// Initializes a new <see cref="BoundaryKind"/> with a given <see cref="BoundaryDirection"/> and <see cref="Inclusivity"/>
        /// </summary>
        protected BoundaryKind(BoundaryDirection direction, Inclusivity inclusivity)
        {
            Direction = direction;
            Inclusivity = inclusivity;
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary"/> at location d of the result of subtracting another <see cref="IPeriodSet"/>
        /// with a <see cref="Boundary"/> at d from an <see cref="IPeriodSet"/> with this kind of <see cref="Boundary"/> at d
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <inheritdoc />
        public bool Equals(BoundaryKind other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Direction == Direction && other.Inclusivity == Inclusivity;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as BoundaryKind);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Inclusivity * 397) ^ (int)Direction;
            }
        }
    }
}
