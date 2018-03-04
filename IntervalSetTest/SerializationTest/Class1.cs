using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using IntSet = IntervalSet.Default.DefaultIntervalSet<int>;

namespace IntervalSetTest.SerializationTest
{
    [TestFixture]
    public class SerializationTest
    {
        [Test]
        public void Test1()
        {
            IntSet set = new IntSet(1,3).Plus(new IntSet(5,8));
            string serialized = JsonConvert.SerializeObject((ISerializable)set);

            IntSet deserialized = JsonConvert.DeserializeObject<IntSet>(serialized);
        }
    }
}
