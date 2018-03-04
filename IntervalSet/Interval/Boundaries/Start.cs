using System;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary{T}"/> at the start of a period
    /// </summary>
    public class Start<T> : Boundary<T>
        where T : IEquatable<T>, IFormattable
    {
        /// <summary>
        /// Initializes a new <see cref="Start{T}"/> based on a given <paramref name="location"/> and <paramref name="inclusivity"/>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inclusivity"></param>
        public Start(T location, Inclusivity inclusivity):base(location, new StartKind(inclusivity))
        {
        }

        /// <summary>
        /// Initializes a new <see cref="Start{T}"/> based on a given <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="boundary"></param>
        public Start(Boundary<T> boundary):this(boundary.Location, boundary.Kind.Inclusivity)
        {
        }
    }
}
