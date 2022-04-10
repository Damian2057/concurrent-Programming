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

            ballsManager.ClearMap();
            Assert.AreEqual(0, ballsManager.GetAllBalls().Count);
            ballsManager.SummonBalls(5);
            Assert.AreEqual(5, ballsManager.GetAllBalls().Count);
            ballsManager.SummonBalls(3);
            Assert.AreEqual(8, ballsManager.GetAllBalls().Count);
        }

        [DataTestMethod]
        [DataRow(1,51,1,1,1)]
        [DataRow(1, 1, 51, 1, 1)]
        [DataRow(1, 51, 51, 1, 1)]
        [DataRow(1, -5, 5, 1, 1)]
        [DataRow(1, 5, -5, 1, 1)]
        [DataRow(1, -5, -5, 1, 1)]
        [DataRow(1, 5, 5, -51, 1)]
        [DataRow(1, 5, 5, 1, -51)]
        [DataRow(1, 5, 5, -51, -51)]
        public void BallManagerExceptionTest(int ID,int xPos, int yPos, int xD, int yD)
        {
            BallsManager ballsManager = new BallsManager(50, 50);
            Assert.ThrowsException<InvalidDataException>(() => ballsManager.CreateBall(ID,xPos, yPos,xD,yD));
            Assert.AreEqual(0,ballsManager.GetAllBalls().Count);
        }

        [TestMethod]
        public void TickTest()
        {
            BallsManager ballsManager = new BallsManager(150, 100);
            //XY CORDS
            ballsManager.SummonBalls(2);
            int x = ballsManager.GetBallByID(1).XPos;
            int xD = ballsManager.GetBallByID(1).XDirectory;

            int newX = 0;
            int newxD = 0;

            if (ballsManager.GetBallByID(1).XPos
                + ballsManager.GetBallByID(1).XDirectory
                + ballsManager.GetBallsRadius() > ballsManager.GetMapWidth() || ballsManager.GetBallByID(1).XPos
                + ballsManager.GetBallByID(1).XDirectory
                + ballsManager.GetBallsRadius() < 0)
            {
                newxD = xD * (-1);
            }
            else
            {
                newxD = xD;
            }
            newX = x + newxD;
            ballsManager.DoTick();
            Assert.AreEqual(ballsManager.GetBallByID(1).XPos, newX);

            int y = ballsManager.GetBallByID(1).YPos;
            int yD = ballsManager.GetBallByID(1).YDirectory;

            int newy = 0;
            int newyD = 0;


            if (ballsManager.GetBallByID(1).YPos
                + ballsManager.GetBallByID(1).YDirectory
                + ballsManager.GetBallsRadius() > ballsManager.GetMapHeight() || ballsManager.GetBallByID(1).YPos
                + ballsManager.GetBallByID(1).YDirectory
                + ballsManager.GetBallsRadius() < 0)
            {
                newyD = yD * (-1);
            }
            else
            {
                newyD = yD;
            }
            newy = y + newyD;
            ballsManager.DoTick();
            Assert.AreEqual(ballsManager.GetBallByID(1).YPos, newy);


        }
    }
}