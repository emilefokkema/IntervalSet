﻿using System;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A period that has a start but no end yet
    /// </summary>
    /// <typeparam name="TEndingPeriod">the type of the period when it does have an end</typeparam>
    public interface IStartingPeriod<TEndingPeriod>
    {
        /// <summary>
        /// Returns a <typeparamref name="TEndingPeriod"/> that represents this period with an end
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        TEndingPeriod End(DateTime end);
    }
}
