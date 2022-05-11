using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataLayerApi
    {
        public static DataLayerApi CreateData()
        {
            return new DataLayer();
        }

        public abstract void CreateBoard(int width, int height);
        public abstract void AddBall(double xPos, double yPos, double radius, double mass);
        public abstract List<BallApi> GetBalls();
        public abstract void ClearBalls();

        internal class DataLayer : DataLayerApi
        {
            private Board _board;

            public DataLayer()
            {

            }

            public override void AddBall(double xPos, double yPos, double radius, double mass)
            {
                var ball = BallApi.CreateBall(xPos, yPos, radius, mass);
                _board.AddBall(ball);
            }

            public override void ClearBalls()
            {
                _board.ClearBalls();
            }

            public override void CreateBoard(int width, int height)
            {
                _board = new Board(width, height);
            }

            public override List<BallApi> GetBalls()
            {
                return _board.GetBalls();
            }
        }
    }
}
