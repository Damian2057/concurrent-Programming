using System.Numerics;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        private BallApi balls;
        private readonly Vector2 boardSize = new Vector2(800, 600);

        [TestMethod]
        public void BoardTest1()
        {
            balls = BallApi.CreateBallsList(boardSize);
            Assert.AreEqual(boardSize, balls.BoardHeightAndWidth);

        }

        [TestMethod]
        public void BoardTest2()
        {
            var ballsVar = BallApi.CreateBallsList(boardSize);
            ballsVar.AddBalls(5);
            Assert.IsNotNull(ballsVar);
            Assert.AreEqual(boardSize, ballsVar.BoardHeightAndWidth);

        }

    }
}