using System;

namespace IntervalSet.PeriodSet.Period.Boundary.Kind
{
    /// <summary>
    /// A kind of <see cref="Boundary"/> of an <see cref="IPeriodSet"/>
    /// </summary>
    public class BoundaryKind : IEquatable<BoundaryKind>
    {
        /// <summary>
        /// The <see cref="Kind.Inclusivity"/> of this kind
        /// </summary>
        public Inclusivity Inclusivity { get; }

        /// <summary>
        /// The <see cref="BoundaryDirection"/> of this kind
        /// </summary>
        public BoundaryDirection Direction { get; }

        /// <summary>
        /// Initializes a new <see cref="BoundaryKind"/> with a given <see cref="BoundaryDirection"/> and <see cref="Kind.Inclusivity"/>
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
        /// <returns></returns>
        public BoundaryKind Minus(BoundaryKind other)
        {
            if (Equals(other))
            {
                return null;
            }
            return new BoundaryKind(Direction & ~other.Direction, Inclusivity & ~other.Inclusivity);
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary"/> at location d of the result of adding another <see cref="IPeriodSet"/>
        /// with a <see cref="Boundary"/> at d to an <see cref="IPeriodSet"/> with this kind of <see cref="Boundary"/> at d
        /// </summary>
        /// <returns></returns>
        public BoundaryKind Plus(BoundaryKind other)
        {
            return new BoundaryKind(Direction | other.Direction, Inclusivity | other.Inclusivity);
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary"/> at location d of the result of intersecting another <see cref="IPeriodSet"/>
        /// with a <see cref="Boundary"/> at d with an <see cref="IPeriodSet"/> with this kind of <see cref="Boundary"/> at d
        /// </summary>
        /// <returns></returns>
        public BoundaryKind Cross(BoundaryKind other)
        {
            BoundaryDirection crossDirection = Direction & other.Direction;
            Inclusivity crossInclusivity = Inclusivity & other.Inclusivity;
            if (crossDirection == BoundaryDirection.None && crossInclusivity == Inclusivity.Exclusive)
            {
                return null;
            }
            return new BoundaryKind(crossDirection, crossInclusivity);
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
