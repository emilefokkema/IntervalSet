using System;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary{T}"/> at the start of a period
    /// </summary>
    [Serializable]
    public class Start<T> : Boundary<T>
        where T : IEquatable<T>
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

        /// <summary>
        /// Deserializes a <see cref="Start{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Start(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
