namespace IntervalSet.PeriodSet.Period.Boundary.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary"/> at the start of a connected period of time in an <see cref="IPeriodSet"/>
    /// </summary>
    public class Start : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="Start"/>
        /// </summary>
        /// <param name="inclusivity"></param>
        public Start(Inclusivity inclusivity):base(BoundaryDirection.Start, inclusivity)
        {
        }
    }
}
