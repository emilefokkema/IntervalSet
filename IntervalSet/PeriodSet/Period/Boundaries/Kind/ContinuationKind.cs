using System;

namespace IntervalSet.PeriodSet.Period.Boundaries.Kind
{
    /// <summary>
    /// A <see cref="BoundaryKind"/> of a <see cref="DateTime"/> that is a member of an open subset of an <see cref="IPeriodSet"/>
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
