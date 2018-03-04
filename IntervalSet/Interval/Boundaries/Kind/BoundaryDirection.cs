using System;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// What an <see cref="IIntervalSet{T}"/> looks like from a <see cref="Boundary{T}"/>
    /// </summary>
    [Flags]
    public enum BoundaryDirection
    {
        /// <summary>
        /// the <see cref="Boundary{T}"/> in question is not a limit point of the <see cref="IIntervalSet{T}"/>
        /// </summary>
        None = 0,

        /// <summary>
        /// the <see cref="Boundary{T}"/> in question is a limit point of the part of the <see cref="IIntervalSet{T}"/> larger than it
        /// </summary>
        Start = 1,

        /// <summary>
        /// the <see cref="Boundary{T}"/> in question is a limit point of the part of the <see cref="IIntervalSet{T}"/> less than it
        /// </summary>
        End = 2
    }
}
