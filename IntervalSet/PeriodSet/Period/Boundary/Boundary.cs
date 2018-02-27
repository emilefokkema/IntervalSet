using System;
using IntervalSet.PeriodSet.Period.Boundary.Kind;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    /// <summary>
    /// Represents the boundary of a connected period of time in a <see cref="IPeriodSet"/>
    /// </summary>
    public abstract class Boundary
    {
        /// <summary>
        /// This <see cref="Boundary"/>'s <see cref="BoundaryKind"/>
        /// </summary>
        public BoundaryKind Kind { get; }

        /// <summary>
        /// The location of this <see cref="Boundary"/>
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Initializes a new <see cref="Boundary"/> on a given <see cref="DateTime"/> and a given <see cref="BoundaryKind"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="kind"></param>
        protected Boundary(DateTime date, BoundaryKind kind)
        {
            Date = date;
            Kind = kind;
        }
    }
}
