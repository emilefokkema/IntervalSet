using System;

namespace IntervalSet
{
    /// <summary>
    /// positive or negative
    /// </summary>
    public enum Sign
    {
        /// <summary>
        /// positive
        /// </summary>
        Positive = 1,

        /// <summary>
        /// negative
        /// </summary>
        Negative = -1
    }

    /// <summary>
    /// Represents the (positive or negative) infinity of the <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Infinity<T> : IFormattable
    {
        /// <summary>
        /// positive or negative infinity as a <typeparamref name="T"/>
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new <see cref="Infinity{T}"/>
        /// </summary>
        public Infinity(T infinity)
        {
            Value = infinity;
        }

        /// <inheritdoc />
        public virtual string ToString(string format, IFormatProvider provider)
        {
            if (Value is IFormattable)
            {
                return ((IFormattable) Value).ToString(format, provider);
            }

            return Value.ToString();
        }

        /// <summary>
        /// Makes this <see cref="Infinity{T}"/> assignable to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="infinity"></param>
        public static implicit operator T(Infinity<T> infinity)
        {
            return infinity.Value;
        }
    }

    /// <summary>
    /// A default <see cref="Infinity{T}"/> for <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultInfinity<T> : Infinity<T>
    {
        private readonly int _sign;

        /// <summary>
        /// Initializes a new (positive or negative) default <see cref="Infinity{T}"/> for <typeparamref name="T"/>
        /// </summary>
        /// <param name="sign"></param>
        public DefaultInfinity(Sign sign):base(default(T))
        {
            _sign = (int) sign;
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider provider)
        {
            return (_sign * double.PositiveInfinity).ToString(format, provider);
        }
    }
}
