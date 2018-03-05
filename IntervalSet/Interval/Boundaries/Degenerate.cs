using System;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary{T}"/> of a period consisting of a single <typeparamref name="T"/>
    /// </summary>
    [Serializable]
    public class Degenerate<T> : Boundary<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="Degenerate{T}"/> based on a given <paramref name="location"/>
        /// </summary>
        /// <param name="location"></param>
        public Degenerate(T location):base(location, new DegenerateKind())
        {
        }

        /// <summary>
        /// Deserializes a <see cref="Degenerate{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Degenerate(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
