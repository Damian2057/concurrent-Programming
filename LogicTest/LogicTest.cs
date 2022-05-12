using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace LogicTest
{
    [TestClass]
    public class BallTransformation
    {
        [TestMethod]
        public void BallTransformationConstructorTest()
        {
            var tb = BallTransformationApi.CreateBallTransformation(BallApi.CreateBall(0, 0, 1, 5));
            Assert.AreEqual(tb.X, 0);
            Assert.AreEqual(tb.Y, 0);
            Assert.AreEqual(tb.Radius, 1);
            Assert.AreEqual(tb.Mass, 5);
            Assert.IsNotNull(tb.Ball);
        }

        [TestMethod]
        public void LogicLayerTest()
        {
            var data = DataLayerAPI.CreateData();
            var tb = LogicLayerAPI.CreateLogic(data);
            Assert.IsNotNull(tb);
            Assert.IsNotNull(data);


            tb.CreateBoard(100, 100, 5, 5);
            Assert.AreEqual(tb.GetBalls().Count, 5);

            foreach (BallTransformationApi ball in tb.GetBalls())
            {
                Assert.IsTrue(ball.X >= ball.Radius && ball.X <= (100 - ball.Radius));
                Assert.IsTrue(ball.Y >= ball.Radius && ball.Y <= (100 - ball.Radius));
                Assert.AreEqual(5, ball.Radius);
            }
        }
        [TestMethod]
        public void ThreadMoveTest()
        {
            var tb = LogicLayerAPI.CreateLogic();
            tb.CreateBoard(100, 100, 5, 5);

            tb.StartAnimation();


            foreach (BallTransformationApi ball in tb.GetBalls())
            {
                Assert.IsTrue(ball.X >= ball.Radius && ball.X <= (100 - ball.Radius));
                Assert.IsTrue(ball.Y >= ball.Radius && ball.Y <= (100 - ball.Radius));
            }
            Thread.Sleep(200);
            foreach (BallTransformationApi ball in tb.GetBalls())
            {
                Assert.IsTrue(ball.X >= ball.Radius && ball.X <= (100 - ball.Radius));
                Assert.IsTrue(ball.Y >= ball.Radius && ball.Y <= (100 - ball.Radius));
            }

            tb.StopAnimation();
        }
    }
}