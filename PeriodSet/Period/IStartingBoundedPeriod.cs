using System;
using IntervalSet.Interval;

namespace PeriodSet.Period
{
    /// <summary>
    /// An <see cref="IBoundedPeriod"/> without an end
    /// </summary>
    public interface IStartingBoundedPeriod : IStartingInterval<IBoundedPeriod, DateTime>, IBoundedPeriod
    {
    }
}
