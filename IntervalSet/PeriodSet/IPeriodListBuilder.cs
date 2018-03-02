using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// Builds up a list of periods
    /// </summary>
    /// <typeparam name="TPeriod"></typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public interface IPeriodListBuilder<TPeriod, TStartingPeriod>
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// Builds a list of <typeparamref name="TPeriod"/>s given a list of <see cref="Boundary"/>s
        /// </summary>
        /// <param name="boundaries"></param>
        /// <returns></returns>
        IEnumerable<TPeriod> Build(IList<Boundary> boundaries);

        /// <summary>
        /// Returns a <typeparamref name="TStartingPeriod"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TStartingPeriod MakeStartingPeriod(Boundary from);

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> that represents a period containing only one date
        /// </summary>
        /// <param name="degenerate"></param>
        /// <returns></returns>
        TPeriod MakeDegenerate(Degenerate degenerate);
    }
}
