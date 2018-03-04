using System;
using IntervalSet.Interval;

namespace PeriodSet.Period
{
    /// <summary>
    /// An <see cref="IOpenPeriod"/> without an end
    /// </summary>
    public interface IStartingOpenPeriod : IStartingInterval<IOpenPeriod, DateTime>, IOpenPeriod
    {
    }
}
