using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class DummyUnitTest
    {
        [Category("UnitTest")]
        [Test]
        public void AlwaysPassing()
        {
            Assert.IsTrue(true);
        }
    }
}
