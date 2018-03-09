using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSetTest.DefaultImplementation
{
    public class DoubleBuilder : DefaultBuilder<DoubleBuilder, double>
    {
        protected override double PositiveInfinity => double.PositiveInfinity;
        protected override double NegativeInfinity => double.NegativeInfinity;
        public override Start<double> MakeStart(double @from)
        {
            return new Start<double>(from, Inclusivity.Exclusive);
        }
    }

}
