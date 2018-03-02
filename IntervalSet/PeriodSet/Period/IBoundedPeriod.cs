using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A connected period of time with a start date and an end date
    /// </summary>
    public interface IBoundedPeriod : IPeriodSet
    {
        /// <summary>
        /// The start date of this period
        /// </summary>
        DateTime Earliest { get; }

        /// <summary>
        /// The end date of this period
        /// </summary>
        DateTime To { get; }
    }
}
