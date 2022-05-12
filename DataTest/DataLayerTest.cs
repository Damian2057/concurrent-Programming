using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {
        [TestMethod]
        public void DataLayerConstructorTest()
        {
            var data = DataLayerAPI.CreateData();
            Assert.IsNotNull(data);
            data.CreateBoard(700, 700, 10, 5);
            Assert.AreEqual(data.GetBalls().Count, 10);
            Assert.IsNotNull(data.GetBoard());
        }

        [TestMethod]
        public void DataLayerAddAndListTest()
        {
            var data = DataLayerAPI.CreateData();
            data.CreateBoard(700, 700, 10, 5);
            Assert.AreEqual(data.GetBalls().Count, 10);

            data.AddBall(0, 0, 5, 5);
            Assert.AreEqual(data.GetBalls().Count, 11);
        }

        [TestMethod]
        public void isBallCheckerTest()
        {
            var data = DataLayerAPI.CreateData();
            data.CreateBoard(0, 0, 0, 5);
            Assert.IsTrue(data.isBallInCoordinates(0, 0, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 1, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 2, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 3, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 4, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 5, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 6, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 7, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 8, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 9, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 10, 5));
            Assert.IsTrue(data.isBallInCoordinates(0, 11, 5));
        }
    }
}
