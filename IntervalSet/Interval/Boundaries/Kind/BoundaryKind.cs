using System;
using System.Runtime.Serialization;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// A kind of <see cref="Boundary{T}"/> of an <see cref="IIntervalSet{T}"/>
    /// </summary>
    [Serializable]
    public class BoundaryKind : IEquatable<BoundaryKind>, ISerializable
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
        /// Deserializes a <see cref="BoundaryKind"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BoundaryKind(SerializationInfo info, StreamingContext context)
        {
            Direction = (BoundaryDirection) info.GetValue("Direction", typeof(BoundaryDirection));
            Inclusivity = (Inclusivity)info.GetValue("Inclusivity", typeof(Inclusivity));
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary{T}"/> at location d of the complement of an <see cref="IIntervalSet{T}"/> with this kind of <see cref="Boundary{T}"/> at d
        /// </summary>
        /// <returns></returns>
        public BoundaryKind Complement()
        {
            return new BoundaryKind((BoundaryDirection.Start | BoundaryDirection.End) & ~Direction, (Inclusivity.Inclusive|Inclusivity.Exclusive) & ~Inclusivity);
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary{T}"/> at location d (if any) of the interior of an <see cref="IIntervalSet{T}"/> with this kind of <see cref="Boundary{T}"/> at d
        /// </summary>
        /// <returns></returns>
        public BoundaryKind Open()
        {
            if (Direction == BoundaryDirection.None)
            {
                return null;
            }
            return new BoundaryKind(Direction, Inclusivity.Exclusive);
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary{T}"/> at location d of the result of adding another <see cref="IIntervalSet{T}"/>
        /// with a <see cref="Boundary{T}"/> at d to an <see cref="IIntervalSet{T}"/> with this kind of <see cref="Boundary{T}"/> at d
        /// </summary>
        /// <returns></returns>
        public BoundaryKind Plus(BoundaryKind other)
        {
            return new BoundaryKind(Direction | other.Direction, Inclusivity | other.Inclusivity);
        }

        /// <summary>
        /// Returns the <see cref="BoundaryKind"/> of the <see cref="Boundary{T}"/> at location d of the result of intersecting another <see cref="IIntervalSet{T}"/>
        /// with a <see cref="Boundary{T}"/> at d with an <see cref="IIntervalSet{T}"/> with this kind of <see cref="Boundary{T}"/> at d
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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Inclusivity", Inclusivity);
            info.AddValue("Direction", Direction);
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
