using System;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// An <see cref="IPeriodSet"/> that contains at least one connected period of time
    /// </summary>
    public interface INonEmptyPeriod : IPeriodSet
    {
        /// <summary>
        /// The start date of the first connected period of time in this <see cref="IPeriodSet"/>
        /// </summary>
        DateTime Earliest { get; }
    }
}
