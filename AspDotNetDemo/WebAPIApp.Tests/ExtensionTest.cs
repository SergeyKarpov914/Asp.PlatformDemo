using Core.Lib.Extension;
using NUnit.Framework;

namespace WebAPIApp.Tests
{
    [TestFixture]
    public class ExtensionTest
    {
        [Test]
        public void Test1()
        {
            bool isTrue = true;

            bool isFalse = isTrue.Toggle();
        }
    }
}
