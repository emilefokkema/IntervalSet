using System;

namespace IntervalSet.PeriodSet.Period.Boundary.Kind
{
    /// <summary>
    /// What a <see cref="IPeriodSet"/> looks like from a <see cref="Boundary"/>
    /// </summary>
    [Flags]
    public enum BoundaryDirection
    {
        /// <summary>
        /// the <see cref="Boundary"/> in question is not a limit point of the <see cref="IPeriodSet"/>
        /// </summary>
        None = 0,

        /// <summary>
        /// the <see cref="Boundary"/> in question is a limit point of the part of the <see cref="IPeriodSet"/> larger than it
        /// </summary>
        Start = 1,

        /// <summary>
        /// the <see cref="Boundary"/> in question is a limit point of the part of the <see cref="IPeriodSet"/> less than it
        /// </summary>
        End = 2
    }
}
