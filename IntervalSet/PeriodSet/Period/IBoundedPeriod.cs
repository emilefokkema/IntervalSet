using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// An <see cref="INonEmptyPeriod"/> with an end date
    /// </summary>
    public interface IBoundedPeriod : IPeriodSet
    {
        DateTime Earliest { get; }

        /// <summary>
        /// The end date of this period
        /// </summary>
        DateTime To { get; }
    }
}
