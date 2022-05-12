using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            var ball = BallApi.CreateBall(1, 2, 3, 4);

            Assert.AreEqual(1, ball.X);
            Assert.AreEqual(2, ball.Y);
            Assert.AreEqual(3, ball.Radius);
            Assert.AreEqual(4, ball.Mass);

            //change ball properties
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
    }
}