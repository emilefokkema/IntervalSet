using System;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// Whether the location of a given <see cref="Boundary"/> is a member of the <see cref="IIntervalSet{T}"/>
    /// </summary>
    [Flags]
    public enum Inclusivity
    {
        /// <summary>
        /// The <see cref="IIntervalSet{T}"/> in question contains the <see cref="Boundary"/> in question
        /// </summary>
        Inclusive = 1,

        /// <summary>
        /// The <see cref="IIntervalSet{T}"/> in question does not contain the <see cref="Boundary"/> in question
        /// </summary>
        Exclusive = 0
    }
}
