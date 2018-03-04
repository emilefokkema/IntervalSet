using System;
using PeriodSet.Period.Boundaries.Kind;

namespace PeriodSet.Period.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary"/> of a period consisting of a single <see cref="DateTime"/>
    /// </summary>
    public class Degenerate : Boundary
    {
        /// <summary>
        /// Initializes a new <see cref="Degenerate"/> based on a given <paramref name="date"/>
        /// </summary>
        /// <param name="date"></param>
        public Degenerate(DateTime date):base(date, new DegenerateKind())
        {
        }
    }
}
