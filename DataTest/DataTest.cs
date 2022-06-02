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
        public void BoardTest()
        {
            balls = BallApi.CreateBallsList(boardSize);
            Assert.AreEqual(boardSize, balls.BoardHeightAndWidth);

        }

    }


}