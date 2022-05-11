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
        private readonly DataLayerApi _logic;
        private bool _movingFlag = false;
        private object lockk = new object();
        private List<Thread> _threads;
        private List<BallTransform> _ballsTransformations;

        public LogicLayer(DataLayerApi logic)
        {
            _logic = logic;
        }

        public override void CreateMap(int width, int height, int numberOfBalls, int radius)
        {
            _ballsTransformations = new();
            _logic.CreateBoard(width,height);

            for(int i = 0; i < numberOfBalls; i++)
            {

            }
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
