using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// An <see cref="IPeriodSet"/> that contains methods for looping and selecting
    /// </summary>
    /// <typeparam name="TPeriod">The type representing the connected periods of time in this <see cref="IPeriodSet"/></typeparam>
    public interface IEnumerablePeriodSet<out TPeriod> : IPeriodSet
    {
        /// <summary>
        /// Projects each <typeparamref name="TPeriod"/> of this <see cref="IPeriodSet"/> to a new form
        /// </summary>
        /// <typeparam name="TT">The type of the value returned by the selector</typeparam>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns></returns>
        IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector) where TT : class;

        /// <summary>
        /// Loops through each of the <typeparamref name="TPeriod"/>s in this <see cref="IPeriodSet"/> and applies a given <see cref="Action{T}"/> to each
        /// </summary>
        /// <param name="what"></param>
        void ForEach(Action<TPeriod> what);

        /// <summary>
        /// Gets the number of connected periods of time in this <see cref="IPeriodSet"/>
        /// </summary>
        int PeriodCount { get; }
    }
}
