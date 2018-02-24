using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntervalSet;

namespace IntervalSetTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Class1();
            Assert.AreEqual(c.GetOne(), 1);
        }
    }
}
