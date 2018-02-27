using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// Whether the location of a given <see cref="Boundary"/> is a member of the <see cref="IPeriodSet"/>
    /// </summary>
    [Flags]
    public enum Inclusivity
    {
        /// <summary>
        /// The <see cref="IPeriodSet"/> in question contains the <see cref="Boundary"/> in question
        /// </summary>
        Inclusive = 1,

        /// <summary>
        /// The <see cref="IPeriodSet"/> in question does not contain the <see cref="Boundary"/> in question
        /// </summary>
        Exclusive = 0
    }
}
