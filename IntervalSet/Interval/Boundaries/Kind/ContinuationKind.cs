using System;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// A <see cref="BoundaryKind"/> of a <c>T</c> that is a member of an open subset of an <see cref="IIntervalSet{T}"/>
    /// </summary>
    public class ContinuationKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="ContinuationKind"/>
        /// </summary>
        public ContinuationKind():base(BoundaryDirection.Start | BoundaryDirection.End, Inclusivity.Inclusive)
        {
        }
    }
}
