using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class DataComponentTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            var ball = BallApi.CreateBall(1, 2, 3, 4);

            Assert.AreEqual(1, ball.X);
            Assert.AreEqual(2, ball.Y);
            Assert.AreEqual(3, ball.Radius);
            Assert.AreEqual(4, ball.Mass);
        }

        [TestMethod]
        public void BallSetterAndGetterTest()
        {
            var ball = BallApi.CreateBall(1, 2, 3, 5);
            ball.X = 0;
            ball.Y = 1;
            ball.Radius = 2;
            ball.Mass = 3;

            Assert.AreEqual(0, ball.X);
            Assert.AreEqual(1, ball.Y);
            Assert.AreEqual(2, ball.Radius);
            Assert.AreEqual(3, ball.Mass);
        }

        [TestMethod]
        public void BallMoveTest()
        {
            var ball = BallApi.CreateBall(0, 0, 3, 4);
            double preX = ball.XDirection + ball.X;
            double preY = ball.YDirection + ball.Y;
            Assert.IsFalse(ball.IsMoving);
            ball.MoveBall();
            Assert.IsTrue(ball.IsMoving);
            Assert.AreEqual(ball.X, preX);
            Assert.AreEqual(ball.Y, preY);
        }

        [TestMethod]
        public void BoardConstructorTest()
        {
            var board = BoardApi.CreateBoard(1080, 1920);
            var ball = BallApi.CreateBall(1, 2, 3, 4);

            Assert.AreEqual(board.Height, 1080);
            Assert.AreEqual(board.Width, 1920);
            Assert.AreEqual(board.GetBalls().Count, 0);

            board.AddBall(ball);

            Assert.AreEqual(board.GetBalls().Count, 1);
        }

        [TestMethod]
        public void ColorTest()
        {
            var color = ColorApi.CreateColor();
            Assert.IsNotNull(color.PickColor());
        }
    }
}