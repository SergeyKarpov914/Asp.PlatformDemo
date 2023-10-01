using Clio.Demo.Extension;
using Core.Lib.Extension;
using NUnit.Framework;

namespace Clio.Demo.Core.Tests
{
    [TestFixture]
    public class StringTest
    {
        //tests EqualNoCase function
        [Test]
        public void EqualsNoCase1()
        {
            string self = "Sergey";
            string other = "sergey";

            bool result = self.EqualsNoCase(other);

            Assert.AreEqual(result, true);
        }

        [Test]
        public void EqualsNoCase2()
        {
            string self = "Sergey";
            string other = "Sergey";

            bool result = self.EqualsNoCase(other);

            Assert.AreEqual(result, true);
        }

        [Test]
        public void EqualsNoCase3()
        {
            string self = "Nickolas";
            string other = "Sergey";

            bool result = self.EqualsNoCase(other);

            Assert.AreEqual(result, true);
        }

        [Test]
        public void EqualsNoCase4()
        {
            string self = null;
            string other = "sergey";

            bool result = self.EqualsNoCase(other);

            Assert.AreEqual(result, true);
        }

        //tests IsEmpty

        [Test]
        public void IsEmpty1()
        {
            string str = "";
            bool result = str.IsEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty2()
        {
            string str = null;
            bool result = str.IsEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty3()
        {
            string str = "notEmpty";
            bool result = str.IsEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty4()
        {
            string str = "   "; // 3 spaces
            bool result = str.IsEmpty();

            Assert.IsTrue(result);
        }

        //tests IsNotEmpty

        [Test]
        public void IsNotEmpty1()
        {
            string str = "";
            bool result = str.IsNotEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsNotEmpty2()
        {
            string str = null;
            bool result = str.IsNotEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsNotEmpty3()
        {
            string str = "notEmpty";
            bool result = str.IsNotEmpty();

            Assert.IsTrue(result);
        }
        [Test]
        public void IsNotEmpty4()
        {
            string str = "   "; // 3 spaces
            bool result = str.IsNotEmpty();

            Assert.IsTrue(result);
        }

        //tests NoLongerThan

        [Test]
        public void NoLongerThan1()
        {
            string str = "testing"; // 7 characters
            int max = 7;
            string result = str.NoLongerThan(max);

            Assert.LessOrEqual(result.Length, max);
        }

        [Test]
        public void NoLongerThan2()
        {
            string str = "testing"; // 7 characters
            int max = 5;
            string result = str.NoLongerThan(max);

            Assert.LessOrEqual(result.Length, max);
        }

        [Test]
        public void NoLongerThan3()
        {
            string str = "testing"; // 7 characters
            int max = 8;
            string result = str.NoLongerThan(max);

            Assert.LessOrEqual(result.Length, max);
        }

        [Test]
        public void NoLongerThan4()
        {
            string str = null; // null case
            int max = 7;
            Assert.DoesNotThrow(() => str.NoLongerThan(max));

            string result = str.NoLongerThan(max);
            Assert.LessOrEqual(result.Length, max);
        }

        [Test]
        public void NoLongerThan5()
        {
            string str = "test";
            int max = -1;
            string result = str.NoLongerThan(max);

            Assert.LessOrEqual(result.Length, max);
        }

        [Test]
        public void NoLongerThan6()
        {
            string str = "test";
            int max = 0;
            string result = str.NoLongerThan(max);

            Assert.LessOrEqual(result.Length, max);
        }


    }
}
