using System;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet.Period.Boundaries
{
    /// <summary>
    /// Represents the boundary of a connected period of time in a <see cref="IPeriodSet"/>
    /// </summary>
    public class Boundary : IEquatable<Boundary>, IFormattable
    {
        /// <summary>
        /// This <see cref="Boundary"/>'s <see cref="BoundaryKind"/>
        /// </summary>
        public BoundaryKind Kind { get; }

        /// <summary>
        /// Whether this <see cref="Boundary"/> includes <see cref="Date"/>
        /// </summary>
        public bool Inclusive => Kind.Inclusivity == Inclusivity.Inclusive;

        /// <summary>
        /// Whether this <see cref="Boundary"/> is a <see cref="BoundaryDirection.Start"/>
        /// </summary>
        public bool IsStart => Kind.Direction.HasFlag(BoundaryDirection.Start);

        /// <summary>
        /// Whether this <see cref="Boundary"/> is an <see cref="BoundaryDirection.End"/>
        /// </summary>
        public bool IsEnd => Kind.Direction.HasFlag(BoundaryDirection.End);

        /// <summary>
        /// The boundary is not really a boundary: it is a member of an open subset of the <see cref="IPeriodSet"/>
        /// </summary>
        public bool IsContinuation => IsStart && IsEnd && Inclusive;

        /// <summary>
        /// The location of this <see cref="Boundary"/>
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Initializes a new <see cref="Boundary"/> on a given <see cref="DateTime"/> and a given <see cref="BoundaryKind"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="kind"></param>
        public Boundary(DateTime date, BoundaryKind kind)
        {
            Date = date;
            Kind = kind;
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
        public bool Equals(Boundary other)
        {
            if (other == null)
            {
                return false;
            }

            return Date == other.Date && Kind.Equals(other.Kind);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as Boundary);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Kind != null ? Kind.GetHashCode() : 0) * 397) ^ Date.GetHashCode();
            }
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = Date.ToString(format, formatProvider);
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
