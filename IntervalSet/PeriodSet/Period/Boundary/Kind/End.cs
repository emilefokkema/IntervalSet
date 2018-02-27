namespace IntervalSet.PeriodSet.Period.Boundary.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary"/> at the end of a connected period of time in an <see cref="IPeriodSet"/>
    /// </summary>
    public class End : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="End"/>
        /// </summary>
        /// <param name="inclusivity"></param>
        public End(Inclusivity inclusivity):base(BoundaryDirection.End, inclusivity)
        {
        }
    }
}
