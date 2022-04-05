using LogicLayer;
using LogicLayer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.LogicTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void BallsManagerConstructorTest()
        {
            BallsManager ballsManager = new BallsManager(150, 100);
            Assert.AreEqual(ballsManager.GetMapWidth(),150);
            Assert.AreEqual(ballsManager.GetMapHeight(), 100);
        }

        [TestMethod]
        public void BallsManagerStorageManagementTest()
        {
            BallsManager ballsManager = new BallsManager(150, 100);
            Assert.AreEqual(0, ballsManager.GetAllBalls().Count);
            ballsManager.CreateBall(0,5,5,10,10);
            Assert.AreEqual(1, ballsManager.GetAllBalls().Count);
            Assert.IsTrue(ballsManager.IsBallWithID(0));
            Assert.IsFalse(ballsManager.IsBallWithID(1));
            Assert.AreEqual(1,ballsManager.AutoID());
            ballsManager.ClearMap();
            Assert.AreEqual(0, ballsManager.GetAllBalls().Count);
        }

        [TestMethod]
        public void BallRandomGeneratorTest()
        {
            BallsManager ballsManager = new BallsManager(150, 100);
            ballsManager.GenerateRandomBall();
            ballsManager.GenerateRandomBall();
            ballsManager.GenerateRandomBall();
            Assert.AreEqual(3, ballsManager.GetAllBalls().Count);
            Assert.IsTrue(ballsManager.IsBallWithID(1));
            Assert.IsTrue(ballsManager.IsBallWithID(2));
            Assert.IsTrue(ballsManager.IsBallWithID(3));
            Assert.ThrowsException<InvalidDataException>(() => ballsManager.GetBallByID(4));
            Assert.ThrowsException<InvalidDataException>(() => ballsManager.RemoveBallByID(4));
        }

        [TestMethod]
        public void TickTest()
        {
            //TICK TEST HERE
        }
    }
}