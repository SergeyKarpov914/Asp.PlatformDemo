using Clio.Demo.Extension;
using NUnit.Framework;

namespace Clio.Demo.Core.Tests
{
    [TestFixture]
    public class CollectionExTest
    {
        [Test]
        public void IsEmpty1()
        {
            IEnumerable<int> ints = new List<int>();
            bool result = ints.IsEmpty<int>();

            Assert.AreEqual(result, true);
        }
        [Test]
        public void IsEmpty2()
        {
            IEnumerable<int> ints = Enumerable.Empty<int>();
            //ints.
            bool result = ints.IsEmpty<int>();

            Assert.AreEqual(result, true);
        }
    }
}
