using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            Ball ball = new Ball(0,0, 1,0, 2, 3);
            Assert.AreEqual(0, ball.XPos);
            Assert.AreEqual(1, ball.YPos);
            Assert.AreEqual(2, ball.XDirection);
            Assert.AreEqual(3, ball.YDirection);
            Assert.AreEqual(0, ball.GetID());
            Assert.AreEqual(0, ball.Radius);

            Assert.IsTrue(ball.XPos >= 0);
            Assert.IsTrue(ball.YPos >= 0);
            Assert.IsTrue(ball.XDirection >= 0);
            Assert.IsTrue(ball.YDirection >= 0);
            Assert.IsTrue(ball.GetID() >= 0);
            Assert.IsTrue(ball.Radius >= 0);
        }

        [TestMethod]
        public void ObjectStorageTest()
        {
            ObjectStorage<Ball> objectStorage = new();

            Ball ball = new Ball(1,0, 1,0, 2, 3);
            objectStorage.AddBall(ball);

            Assert.AreEqual(objectStorage.GetAllBalls().Count, 1);
            Ball ball2 = new Ball(2,0, 1,0, 2, 3);
            objectStorage.AddBall(ball2);

            Assert.AreEqual(objectStorage.GetAllBalls().Count, 2);

            objectStorage.RemoveBall(ball);

            Assert.AreEqual(objectStorage.GetAllBalls().Count, 1);

            objectStorage.ClearStorage();

            Assert.AreEqual(objectStorage.GetAllBalls().Count, 0);
        }

        [TestMethod]
        public void ColorTest()
        {
            Ball ball = new Ball(1, 0, 1, 0, 2, 3);
            Assert.IsTrue(ball.color != "");
            Assert.IsTrue(ball.color is not null);
        }
    }
}