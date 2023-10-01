using Clio.Demo.Extension;
using Core.Lib.Extension;
using NUnit.Framework;

namespace Clio.Demo.Core.Tests
{
    [TestFixture]
    public class BoolExTest
    {
        [Test]
        public void Toggle1() // part of BoolEx.cs
        {
            bool isTrue = true;
            bool isFalse = isTrue.Toggle();

            Assert.AreEqual(isFalse, false);
        }

        [Test]
        public void Toggle2() // part of BoolEx.cs
        {
            bool isTrue = true;
            bool isFalse = isTrue.Toggle().Toggle();

            Assert.AreEqual(isFalse, false);
        }

    }
}
