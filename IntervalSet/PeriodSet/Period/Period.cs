using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// An implementation of <see cref="IOpenPeriod"/> and <see cref="IBoundedPeriod"/> at once
    /// </summary>
    public class Period : IOpenPeriod, IBoundedPeriod
    {
        /// <inheritdoc />
        public DateTime Earliest { get; }
        private readonly DateTime _to;

        /// <summary>
        /// Initializes a new <see cref="Period"/> with a start date
        /// </summary>
        /// <param name="from"></param>
        public Period(DateTime from)
        {
            Earliest = from;
            _to = DateTime.MaxValue;
        }

        /// <summary>
        /// Initializes a new <see cref="Period"/> with a start date and an end date
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Period(DateTime from, DateTime to) : this(from)
        {
            _to = to;
        }

        private IPeriodSet ToPeriodSet()
        {
            return new BoundedPeriodSet(Earliest, _to);
        }

        /// <inheritdoc />
        public IEnumerable<DateTime> Boundaries {
            get
            {
                yield return Earliest;
                yield return _to;
            }
        }

        IPeriodSet IPeriodSet.Plus(IPeriodSet other)
        {
            return ToPeriodSet().Plus(other);
        }

        IPeriodSet IPeriodSet.Minus(IPeriodSet other)
        {
            return ToPeriodSet().Minus(other);
        }

        IPeriodSet IPeriodSet.Cross(IPeriodSet other)
        {
            return ToPeriodSet().Cross(other);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes)
        {
            return ToPeriodSet().Where(trueFrom, changes);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IEnumerable<DateTime> changes)
        {
            return ToPeriodSet().Where(trueEverywhereBetween, changes);
        }

        bool IPeriodSet.IsEmpty => false;


        /// <inheritdoc />
        public bool Equals(IPeriodSet other)
        {
            if (other == null)
            {
                return false;
            }
            var set = ToPeriodSet();
            return other.Minus(set).IsEmpty && set.Minus(other).IsEmpty;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            return Equals(other as IPeriodSet);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (_to.GetHashCode() * 397) ^ Earliest.GetHashCode();
            }
        }

        /// <inheritdoc />
        public bool ContainsDate(DateTime date)
        {
            return date >= Earliest && date < _to;
        }

        BoundaryKind IPeriodSet.Cross(DateTime date)
        {
            if (ContainsDate(date))
            {
                return BoundaryKind.Start;
            }
            if (Earliest == _to && date == Earliest)
            {
                return BoundaryKind.Start | BoundaryKind.End;
            }
            if (date == _to)
            {
                return BoundaryKind.End;
            }
            return BoundaryKind.None;
        }

        bool IPeriodSet.ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Earliest && to <= _to;
        }

        bool IPeriodSet.Intersects(IPeriodSet other)
        {
            return !ToPeriodSet().Cross(other).IsEmpty;
        }

        DateTime? IOpenPeriod.To => _to == DateTime.MaxValue ? null : (DateTime?)_to;
        DateTime IBoundedPeriod.To => _to;

        /// <summary>
        /// Converts a period to an <see cref="OpenPeriodSet"/>
        /// </summary>
        /// <param name="period"></param>
        public static implicit operator OpenPeriodSet(Period period)
        {
            IOpenPeriod open = period;
            return new OpenPeriodSet(open.Earliest, open.To);
        }

        /// <summary>
        /// Converts a period to a <see cref="BoundedPeriodSet"/>
        /// </summary>
        /// <param name="period"></param>
        public static implicit operator BoundedPeriodSet(Period period)
        {
            IBoundedPeriod bounded = period;
            return new BoundedPeriodSet(bounded.Earliest, bounded.To);
        }

    }
}
