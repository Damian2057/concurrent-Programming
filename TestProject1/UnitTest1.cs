using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(FirstProgram.Program.toTestMethod(), "Hello World");
            Assert.AreNotEqual(FirstProgram.Program.toTestMethod(), "Goodbye World");
        }
    }
}