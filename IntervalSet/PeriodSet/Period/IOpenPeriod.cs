using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A connected period of time with a start date and a (possible) end date
    /// </summary>
    public interface IOpenPeriod : IPeriodSet
    {
        /// <summary>
        /// The start date of this period
        /// </summary>
        DateTime Earliest { get; }

        /// <summary>
        /// The (possible) end date of this period
        /// </summary>
        DateTime? To { get; }
    }
}
