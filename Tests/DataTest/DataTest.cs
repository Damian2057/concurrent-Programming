﻿using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {

            var ball = BallApi.CreateBall(0,0, 1,0, 2, 3);
            Assert.AreEqual(0, ball.XPos);
            Assert.AreEqual(1, ball.YPos);
            Assert.AreEqual(2, ball.XDirection);
            Assert.AreEqual(3, ball.YDirection);
            Assert.AreEqual(0, ball.BallID);
            Assert.AreEqual(0, ball.Radius);

            Assert.IsTrue(ball.XPos >= 0);
            Assert.IsTrue(ball.YPos >= 0);
            Assert.IsTrue(ball.XDirection >= 0);
            Assert.IsTrue(ball.YDirection >= 0);
            Assert.IsTrue(ball.BallID >= 0);
            Assert.IsTrue(ball.Radius >= 0);
            Assert.IsNotNull(ball.color);
        }

        [TestMethod]
        public void ObjectStorageTest()
        {

            var objectStorage = BallRepositoryApi.CreateRepository();

            var ball = BallApi.CreateBall(1,0, 1,0, 2, 3);
            objectStorage.AddBall(ball);

            Assert.AreEqual(objectStorage.GetAllBalls().Count, 1);
            var ball2 = BallApi.CreateBall(2,0, 1,0, 2, 3);
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
            //test for a color selected from a premade array
            var ball = BallApi.CreateBall(1, 0, 1, 0, 2, 3); 
            Assert.IsTrue(ball.color != "");
            Assert.IsTrue(ball.color is not null);
            Assert.IsTrue(ball.color.Length == 7);
            Assert.IsTrue(ball.color.StartsWith("#"));

            //test for a pseudorandom color
            var ball2 = BallApi.CreateBall(1337, 5, 5, 5, 5, 5);
            Assert.IsTrue(ball2.color != "");
            Assert.IsTrue(ball2.color is not null);
            Assert.IsTrue(ball2.color.Length == 7);
            Assert.IsTrue(ball2.color.StartsWith("#"));
        }
    }
}