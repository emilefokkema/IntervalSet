using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// A <see cref="BoundaryKind"/> of a <see cref="DateTime"/> that is a member of an open subset of an <see cref="IPeriodSet"/>
    /// </summary>
    public class Continuation : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="Continuation"/>
        /// </summary>
        public Continuation():base(BoundaryDirection.Start | BoundaryDirection.End, Inclusivity.Inclusive)
        {
        }
    }
}
