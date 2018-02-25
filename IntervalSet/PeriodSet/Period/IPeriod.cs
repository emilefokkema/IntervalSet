using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// An <see cref="INonEmptyPeriod"/> with a (possible) end date
    /// </summary>
    public interface IOpenPeriod : INonEmptyPeriod
    {
        /// <summary>
        /// The (possible) end date of this period
        /// </summary>
        DateTime? To { get; }
    }

    /// <summary>
    /// An <see cref="INonEmptyPeriod"/> with an end date
    /// </summary>
    public interface IBoundedPeriod : INonEmptyPeriod
    {
        /// <summary>
        /// The end date of this period
        /// </summary>
        DateTime To { get; }
    }
}
