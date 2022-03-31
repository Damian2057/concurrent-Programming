using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("Hello world", Data.Bullet.GetText());
        }

        [TestMethod]
        public void TestMethod2()
        {

        }

        [TestMethod]
        public void TestMethod3()
        {

        }
    }
}