﻿using System;
using IntervalSet;

namespace PeriodSet.Period
{
    /// <summary>
    /// A connected period of time with a start date and a (possible) end date
    /// </summary>
    public interface IOpenPeriod : IIntervalSet<DateTime>
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
