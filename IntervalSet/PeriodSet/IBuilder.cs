using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// Helps build up an <see cref="IPeriodSet"/>
    /// </summary>
    /// <typeparam name="TPeriod">the type of period to build</typeparam>
    /// <typeparam name="TStartingPeriod">the type of period to start with</typeparam>
    /// <typeparam name="TSet">the type of <see cref="IPeriodSet"/> to build</typeparam>
    public interface IBuilder<TSet, TPeriod, TStartingPeriod>
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// Builds a list of <typeparamref name="TPeriod"/>s given a list of <see cref="Boundary"/>s
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="currentPeriod"></param>
        /// <returns></returns>
        IEnumerable<TPeriod> Build(IList<Boundary> boundaries, TStartingPeriod currentPeriod);

        /// <summary>
        /// Returns a <typeparamref name="TStartingPeriod"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TStartingPeriod MakeStartingPeriod(Start from);

        /// <summary>
        /// Returns a <typeparamref name="TStartingPeriod"/> "starting" at negative infinity
        /// </summary>
        /// <returns></returns>
        TStartingPeriod MakeStartingPeriod();

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> that represents a period containing only one date
        /// </summary>
        /// <param name="degenerate"></param>
        /// <returns></returns>
        TPeriod MakeDegenerate(Degenerate degenerate);

        /// <summary>
        /// Returns a <typeparamref name="TSet"/> based on a given list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        TSet MakeSet(IList<TPeriod> periods);

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> based on a given non-empty list of <typeparamref name="TPeriod"/>s
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        TPeriod MakeNonEmptySet(IList<TPeriod> list);
    }
}
