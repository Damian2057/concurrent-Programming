using System.Linq;
using LogicLayer;
using LogicLayer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.LogicTest
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void BallServiceTest()
        {
            var service = BallServiceApi.CreateLogic(150, 100);
            service.SummonBalls(10);
            Assert.AreEqual(service.GetAllBalls().Count, 10);
            Assert.AreEqual(service.GetBallByID(1).BallID, 1);

            service.RemoveBallByID(1);
            Assert.AreEqual(service.GetAllBalls().Count, 9);
            Assert.ThrowsException<InvalidDataException>(() => service.GetBallByID(1));

            int x1 = service.GetBallByID(2).XPos;
            int y1 = service.GetBallByID(2).YPos;
            service.DoTick();
            Assert.IsTrue(service.GetBallByID(2).XPos != x1 || service.GetBallByID(2).YPos != y1);

            service.ClearMap();
            Assert.AreEqual(service.GetAllBalls().Count, 0);
            Assert.ThrowsException<InvalidDataException>(() => service.GetBallByID(5));
        }

        [TestMethod]
        public void TickTest()
        {
            var ballsManager = BallServiceApi.CreateLogic(150, 100);
            //XY CORDS
            ballsManager.SummonBalls(2);
            int xCurrentPos = ballsManager.GetBallByID(1).XPos;
            int xHeading = ballsManager.GetBallByID(1).XDirection;

            int xPredictedPos = 0;
            int xPredictedHeading = 0;

            if (ballsManager.GetBallByID(1).XPos
                + ballsManager.GetBallByID(1).XDirection
                + ballsManager.GetBallByID(1).Radius > ballsManager.GetMapWidth() || ballsManager.GetBallByID(1).XPos
                + ballsManager.GetBallByID(1).XDirection
                + ballsManager.GetBallByID(1).Radius < 2*ballsManager.GetBallByID(1).Radius) // tu chyba powinno byc 0
            {
                xPredictedHeading = xHeading * (-1);
            }
            else
            {
                xPredictedHeading = xHeading;
            }
            xPredictedPos = xCurrentPos + xPredictedHeading;
            ballsManager.DoTick();
            Assert.AreEqual(ballsManager.GetBallByID(1).XPos, xPredictedPos);

            int yCurrentPos = ballsManager.GetBallByID(1).YPos;
            int yHeading = ballsManager.GetBallByID(1).YDirection;

            int yPredictedPos = 0;
            int yPredictedHeading = 0;


            if (ballsManager.GetBallByID(1).YPos
                + ballsManager.GetBallByID(1).YDirection
                + ballsManager.GetBallByID(1).Radius > ballsManager.GetMapHeight() || ballsManager.GetBallByID(1).YPos
                + ballsManager.GetBallByID(1).YDirection
                + ballsManager.GetBallByID(1).Radius < 2 * ballsManager.GetBallByID(1).Radius)
            {
                yPredictedHeading = yHeading * (-1);
            }
            else
            {
                yPredictedHeading = yHeading;
            }
            yPredictedPos = yCurrentPos + yPredictedHeading;
            ballsManager.DoTick();
            Assert.AreEqual(ballsManager.GetBallByID(1).YPos, yPredictedPos);


        }
    }
}