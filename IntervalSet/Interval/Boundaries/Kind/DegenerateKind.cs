﻿namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// The <see cref="Boundary"/> in question is not a limit point of the <see cref="IIntervalSet{T}"/> it is a <see cref="Boundary"/> of
    /// </summary>
    public class DegenerateKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateKind"/>
        /// </summary>
        public DegenerateKind():base(BoundaryDirection.None, Inclusivity.Inclusive)
        {
        }
    }
}