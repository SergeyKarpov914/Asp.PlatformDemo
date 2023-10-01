using NUnit.Framework;

namespace WebAPIApp.Tests
{
    [TestFixture]
    public class WebAPIappTest1
    {
        [Test]
        public void Test1()
        {
            int x = 0;
            int y = x * 3;

            Assert.AreEqual(y, 0);
        }
    }
}
