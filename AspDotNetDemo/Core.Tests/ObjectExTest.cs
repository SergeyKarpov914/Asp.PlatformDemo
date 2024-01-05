using Clio.Demo.Core.Lib.Extension;
using NUnit.Framework;

namespace Clio.Demo.Core.Tests
{
    [TestFixture]
    public class ObjectExTest
    {

        //ObjectExTest - Inject<T>(this T source, out object target)
        [Test]
        public void Inject1NullCase1()
        {
            object target = null;
            object source = "source";

            (source as string).Inject<string>(out target);

            Assert.AreEqual(target, source);
        }
        [Test]
        public void Inject1PassCase1()
        {
            object target = "target";
            object source = "source";

            (source as string).Inject<string>(out target);

            Assert.AreEqual(target, source);
        }
        [Test]
        public void Inject1NullCase2()
        {
            object target = "target";
            object source = null;

            (source as string).Inject<string>(out target);

            Assert.AreEqual(target, source);
        }

        //ObjectExTest - Inject<T>(this T source, out T target)
        [Test]
        public void Inject2PassCase1()
        {

        }
        [Test]
        public void Inject2FailCase1()
        {

        }
        [Test]
        public void Inject2NullCase1()
        {

        }

        //ObjectExTest - SetPropertyByName(...)
        [Test]
        public void SetPropertyByNamePassCase1()
        {

        }
        [Test]
        public void SetPropertyByNameFailCase1()
        {

        }
        [Test]
        public void SetPropertyByNameNullCase1()
        {

        }

        //ObjectExTest - GetPropertyValue(...)
        [Test]
        public void GetPropertyValuePassCase1()
        {

        }
        [Test]
        public void GetPropertyValueFailCase1()
        {

        }
        [Test]
        public void GetPropertyValueNullCase1()
        {

        }

        //ObjectExTest - Stamp(...)
        [Test]
        public void StampPassCase1()
        {

        }
        [Test]
        public void StampFailCase1()
        {

        }
        [Test]
        public void StampNullCase1()
        {

        }

        //ObjectExTest - TypeName(...)
        [Test]
        public void TypeNamePassCase1()
        {

        }
        [Test]
        public void TypeNameFailCase1()
        {

        }
        [Test]
        public void TypeNameNullCase1()
        {

        }

        //ObjectExTest - Location(...)
        [Test]
        public void LocationPassCase1()
        {

        }
        [Test]
        public void LocationFailCase1()
        {

        }
        [Test]
        public void LocationNullCase1()
        {

        }

        //ObjectExTest - AssemblyInfo(...)
        [Test]
        public void AssemblyInfoPassCase1()
        {

        }
        [Test]
        public void AssemblyInfoFailCase1()
        {

        }
        [Test]
        public void AssemblyInfoNullCase1()
        {

        }

        //ObjectExTest - AssemblyVersion(...)
        [Test]
        public void AssemblyVersionPassCase1()
        {

        }
        [Test]
        public void AssemblyVersionFailCase1()
        {

        }
        [Test]
        public void AssemblyVersionNullCase1()
        {

        }

        //ObjectExTest - Clone(...)
        [Test]
        public void ClonePassCase1()
        {

        }
        [Test]
        public void CloneFailCase1()
        {

        }
        [Test]
        public void CloneNullCase1()
        {

        }

    }
}
