using System;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet.Period.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary"/> at the start of a period
    /// </summary>
    public class Start : Boundary
    {
        /// <summary>
        /// Initializes a new <see cref="Start"/> based on a given <paramref name="date"/> and <paramref name="inclusivity"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inclusivity"></param>
        public Start(DateTime date, Inclusivity inclusivity):base(date, new StartKind(inclusivity))
        {
        }
    }
}
