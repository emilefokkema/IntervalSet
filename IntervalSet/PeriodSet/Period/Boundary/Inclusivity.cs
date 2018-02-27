namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// Whether the location of a given <see cref="Boundary"/> is or is not a member of the <see cref="IPeriodSet"/>
    /// </summary>
    public enum Inclusivity
    {
        /// <summary>
        /// The <see cref="IPeriodSet"/> in question contains the <see cref="Boundary"/> in question
        /// </summary>
        Inclusive,

        /// <summary>
        /// The <see cref="IPeriodSet"/> in question does not contain the <see cref="Boundary"/> in question
        /// </summary>
        Exclusive
    }
}
