using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace LogicTest
{
    [TestClass]

    public class LogicTest
    {
        [TestMethod]
        public void LogicTest1()
        {
            Vector2 board = new(200, 100);
            var logic = LogicAPi.CreateBallsLogic(board);

            Assert.IsNotNull(logic);
        }

        [TestMethod]
        public void LogicTest2()
        {
            Vector2 board = new(300, 300);
            var logic = LogicAPi.CreateBallsLogic(board);

            Assert.IsNotNull(logic);


        }

        [TestMethod]
        public void LogicTest3()
        {
            Vector2 board = new(200, 250);
            var logic = LogicAPi.CreateBallsLogic(board);

            Assert.IsNotNull(logic);
        }

    }
}