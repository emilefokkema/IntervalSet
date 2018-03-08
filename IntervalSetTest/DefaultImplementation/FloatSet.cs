using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IntervalSet;
using IntervalSet.Default;
using IntervalSet.Interval.Default;

namespace IntervalSetTest.DefaultImplementation
{
    public class FloatBuilder : AbstractDefaultBuilder<FloatSet, FloatBuilder, float>
    {
        public override FloatSet MakeSet(IList<IDefaultInterval<float>> intervals)
        {
            return new FloatSet(intervals);
        }

        public override IDefaultInterval<float> MakeNonEmptySet(IList<IDefaultInterval<float>> intervals)
        {
            return new NonEmptyFloatSet(intervals);
        }
    }

    [Serializable]
    public class FloatSet : DefaultIntervalSet<FloatSet, FloatBuilder, float>
    {
        public FloatSet(IList<IDefaultInterval<float>> intervals):base(intervals)
        {
        }

        public FloatSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FloatSet(float from, float to):base(from, to)
        {
        }
    }

    public class NonEmptyFloatSet : DefaultNonEmptyIntervalSet<FloatSet, FloatBuilder, float>
    {
        public NonEmptyFloatSet(IList<IDefaultInterval<float>> intervals):base(intervals)
        {
        }
    }
}
