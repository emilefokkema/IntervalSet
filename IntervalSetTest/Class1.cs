using NUnit.Framework;
using FluentAssertions;

namespace IntervalSetTest
{
    [TestFixture]
    public class Test2
    {
        [Test]
        public void Test_another()
        {
            new IntervalSet.Class1().GetOne().Should().Be(2);
        }
    }
}
