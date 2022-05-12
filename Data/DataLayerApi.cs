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

        public abstract void CreateBoard(int width, int height, int count, int radius);
        public abstract void AddBall(double xPos, double yPos, double radius, double mass);
        public abstract void StartAnimation();
        public abstract void StopAnimation();
        public abstract Board GetBoard();
        public abstract List<BallApi> GetBalls();
        public abstract void ClearBalls();

        internal class DataLayer : DataLayerApi
        {
            private Board _board;

            public override void CreateBoard(int width, int height, int count, int radius)
            {
                _board = new Board(width, height);
                Random r = new();
                for (int i = 0; i < count; i++)
                {
                    double x;
                    double y;
                    double mass = r.NextDouble() + 1;
                    do
                    {
                        x = r.NextDouble() * (width - 2 * radius) + radius;
                        y = r.NextDouble() * (height - 2 * radius) + radius;

                    } while (!isBallInCoordinates(x, y, radius));

                    AddBall(x, y, radius, mass);
                }
            }

            public override void AddBall(double xPos, double yPos, double radius, double mass)
            {
                var ball = BallApi.CreateBall(xPos, yPos, radius, mass);
                _board.AddBall(ball);
            }

            public override void StartAnimation()
            {
                _board.StartAnimation();
            }

            public override void StopAnimation()
            {
                _board.StopAnimation();
            }

            public override Board GetBoard()
            {
                return _board;
            }

            public override List<BallApi> GetBalls()
            {
                return _board.GetBalls();
            }

            public override void ClearBalls()
            {
                _board.ClearBalls();
            }

            private bool isBallInCoordinates(double xPos, double yPos, double radius)
            {
                foreach (BallApi ball in GetBalls())
                {
                    double environment = Math.Sqrt((xPos - ball.xPos)
                        * (xPos - ball.xPos)
                        + (yPos - ball.yPos)
                        * (yPos - ball.yPos));
                    if (environment <= ball.radius + radius)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
