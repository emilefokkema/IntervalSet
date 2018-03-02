using System;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet.Period.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary"/> at the end of a period
    /// </summary>
    public class End : Boundary
    {
        /// <summary>
        /// Initializes a new <see cref="End"/> based on a given <paramref name="date"/> and <paramref name="inclusivity"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inclusivity"></param>
        public End(DateTime date, Inclusivity inclusivity):base(date, new EndKind(inclusivity))
        {
        }

        /// <summary>
        /// Initializes a new <see cref="End"/> base on a given <see cref="Boundary"/>
        /// </summary>
        /// <param name="boundary"></param>
        public End(Boundary boundary):this(boundary.Date, boundary.Kind.Inclusivity)
        {
        }
    }
}
