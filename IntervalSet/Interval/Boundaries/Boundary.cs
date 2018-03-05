using System;
using System.Globalization;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// Represents the boundary of an interval in an <see cref="IIntervalSet{T}"/>
    /// </summary>
    [Serializable]
    public class Boundary<T> : IEquatable<Boundary<T>>, IFormattable, ISerializable
        where T:IEquatable<T>
    {
        /// <summary>
        /// This <see cref="Boundary{T}"/>'s <see cref="BoundaryKind"/>
        /// </summary>
        public BoundaryKind Kind { get; }

        /// <summary>
        /// Whether this <see cref="Boundary{T}"/> includes <see cref="Location"/>
        /// </summary>
        public bool Inclusive => Kind.Inclusivity == Inclusivity.Inclusive;

        /// <summary>
        /// Whether this <see cref="Boundary{T}"/> is a <see cref="BoundaryDirection.Start"/>
        /// </summary>
        public bool IsStart => Kind.Direction.HasFlag(BoundaryDirection.Start);

        /// <summary>
        /// Whether this <see cref="Boundary{T}"/> is an <see cref="BoundaryDirection.End"/>
        /// </summary>
        public bool IsEnd => Kind.Direction.HasFlag(BoundaryDirection.End);

        /// <summary>
        /// The boundary is not really a boundary: it is a member of an open subset of the <see cref="IIntervalSet{T}"/>
        /// </summary>
        public bool IsContinuation => IsStart && IsEnd && Inclusive;

        /// <summary>
        /// The location of this <see cref="Boundary{T}"/>
        /// </summary>
        public T Location { get; }

        /// <summary>
        /// Initializes a new <see cref="Boundary{T}"/> on a given <see cref="DateTime"/> and a given <see cref="BoundaryKind"/>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="kind"></param>
        public Boundary(T location, BoundaryKind kind)
        {
            Location = location;
            Kind = kind;
        }

        /// <summary>
        /// Deserializes a <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Boundary(SerializationInfo info, StreamingContext context)
        {
            Location = (T)info.GetValue("Location", typeof(T));
            Kind = (BoundaryKind) info.GetValue("Kind", typeof(BoundaryKind));
        }

        private string GetLeftBracket()
        {
            return Inclusive ? "[" : "(";
        }

        private string GetRightBracket()
        {
            return Inclusive ? "]" : ")";
        }

        /// <inheritdoc />
        public bool Equals(Boundary<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return Location.Equals(other.Location) && Kind.Equals(other.Kind);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as Boundary<T>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Kind != null ? Kind.GetHashCode() : 0) * 397) ^ Location.GetHashCode();
            }
        }

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Kind", Kind);
            info.AddValue("Location", Location);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider)
        {
            string result;
            if (Location is IFormattable)
            {
                result = ((IFormattable)Location).ToString(format, formatProvider);
            }
            else
            {
                result = Location.ToString();
            }


            if (IsStart)
            {
                if (IsEnd)
                {
                    return result + GetRightBracket() + GetLeftBracket() + result;
                }
                return GetLeftBracket() + result;
            }
            if (IsEnd)
            {
                return result + GetRightBracket();
            }
            return GetLeftBracket() + result + ", " + result + GetRightBracket();
        }
    }
}
