using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    internal class LogicLayer : LogicLayerApi
    {
        private readonly DataLayerApi _data;
        private bool _movingFlag = false;
        private object lockk = new object();
        private List<Thread> _threads;
        private List<BallTransform> _ballsTransformations;

        public LogicLayer(DataLayerApi data)
        {
            _data = data;
        }

        public override void CreateMap(int width, int height, int numberOfBalls, int radius)
        {
            _ballsTransformations = new();
            _data.CreateBoard(width,height);

            for(int i = 0; i < numberOfBalls; i++)
            {

            }
        }

        private bool isBallInCoordinates(double xPos, double yPos, double radius)
        {
            foreach (BallApi ball in _data.GetBalls())
            {
                double environment = Math.Sqrt((xPos - ball.xPos) 
                    * (xPos - ball.xPos) 
                    + (yPos - ball.yPos) 
                    * (yPos - ball.yPos));
                if(environment <= ball.radius + radius)
                {
                    return false;
                }
            }

            return true;
        }

        public override List<BallTransform> GetBalls()
        {
            return _ballsTransformations;
        }

        public override void ResumeBalls()
        {
            if(!_movingFlag)
            {
                _movingFlag = true;
                foreach(Thread t in _threads)
                {
                    t.Start();
                }
            }
        }

        public override void StopBalls()
        {
            _movingFlag = false;
        }
    }
}
