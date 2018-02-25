using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// An <see cref="INonEmptyPeriod"/> with a (possible) end date
    /// </summary>
    public interface IOpenPeriod : IPeriodSet
    {
        DateTime Earliest { get; }

        /// <summary>
        /// The (possible) end date of this period
        /// </summary>
        DateTime? To { get; }
    }
}
