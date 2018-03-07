using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IIntervalSet{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIntervalBuilder"></typeparam>
    [Serializable]
    public class DefaultIntervalSet<TSet, TIntervalBuilder, T> : MultipleIntervalSet<TSet, TIntervalBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new, empty, <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        public DefaultIntervalSet()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given list of <see cref="IDefaultInterval{T}"/>s
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultIntervalSet(IList<IDefaultInterval<T>> intervals):base(intervals)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public DefaultIntervalSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given start <typeparamref name="T"/> and end <typeparamref name="T"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultIntervalSet(T from, T to):base(new Start<T>(from, Inclusivity.Inclusive), new End<T>(to, Inclusivity.Exclusive))
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        
    }

    [Serializable]
    public class DefaultIntervalSet<TIntervalBuilder, T> : DefaultIntervalSet<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new, empty, <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        public DefaultIntervalSet()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given list of <see cref="IDefaultInterval{T}"/>s
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultIntervalSet(IList<IDefaultInterval<T>> intervals) : base(intervals)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public DefaultIntervalSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given start <typeparamref name="T"/> and end <typeparamref name="T"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultIntervalSet(T from, T to) : base(from, to)
        {
        }

        /// <summary>
        /// A <see cref="DefaultIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
        /// </summary>
        public static DefaultIntervalSet<TIntervalBuilder,T> All = new DefaultIntervalSet<TIntervalBuilder,T>(new List<IDefaultInterval<T>> {new DefaultEntireInterval<TIntervalBuilder,T>()});

        /// <summary>
        /// A <see cref="DefaultIntervalSet{T}"/> representing the empty set as a set containing only <typeparamref name="T"/>s
        /// </summary>
        public static DefaultIntervalSet<TIntervalBuilder,T> Empty = new DefaultIntervalSet<TIntervalBuilder,T>();
    }
}
