namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a member of an <see cref="IPeriodSet"/> that is not a limit point of that set
    /// </summary>
    public class Degenerate : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="Degenerate"/>
        /// </summary>
        public Degenerate():base(BoundaryDirection.None, Inclusivity.Inclusive)
        {
        }
    }
}
