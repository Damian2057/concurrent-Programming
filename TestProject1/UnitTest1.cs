using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("Test", FirstProgram.Program.toTestMethod());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(8, FirstProgram.Program.add(5,3));
            Assert.AreNotEqual(8, FirstProgram.Program.add(5, 4));
            Assert.AreEqual(8, FirstProgram.Program.add(5.5, 2.5));
            Assert.AreEqual(-8, FirstProgram.Program.add(-9.5, 1.5));
        }
    }
}