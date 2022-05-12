using Data;
using System.ComponentModel;

namespace Logic
{
    public abstract class LogicLayerAPI
    {
        public static LogicLayerAPI CreateLogic(DataLayerAPI? data = default)
        {
            return new LogicLayer(data ?? DataLayerAPI.CreateData());
        }

        public abstract void CreateBoard(int height, int width, int numberOfBalls, int radiusOfBalls);
        public abstract void StartAnimation();
        public abstract void StopAnimation();

        public abstract List<BallTransformationApi> GetBalls();
        private List<BallTransformationApi> ballOperators;

        internal class LogicLayer : LogicLayerAPI
        {

            private readonly DataLayerAPI _dataLayer;

            internal LogicLayer(DataLayerAPI dataLayerAbstractAPI)
            {
                _dataLayer = dataLayerAbstractAPI;
            }

            public override void CreateBoard(int height, int width, int numberOfBalls, int radiusOfBalls)
            {
                ballOperators = new();
                _dataLayer.CreateBoard(height, width, numberOfBalls, radiusOfBalls);

                foreach (BallApi ball in _dataLayer.GetBalls())
                {
                    ballOperators.Add(BallTransformationApi.CreateBallTransformation(ball));
                    ball.PropertyChanged += isMoving;
                }

            }

            public override void StartAnimation()
            {
                _dataLayer.StartThreads();
            }

            public override void StopAnimation()
            {
                _dataLayer.StopAnimation();
            }

            public override List<BallTransformationApi> GetBalls()
            {
                return ballOperators;
            }

            public void isMoving(object sender, PropertyChangedEventArgs e)
            {
                BallApi ball = (BallApi)sender;
                if (e.PropertyName == "Position")
                {
                    SetDirectory(ball);
                    ball.IsMoving = false;
                }

            }

            private void SetDirectory(BallApi ball)
            {
                WindowbarrierChecker(ball, _dataLayer.GetBoard());
                BallApi colisionBall = CheckDirection(ball);
                if (colisionBall != null)
                {
                    double newXFirst;
                    double newXSecond;
                    double newYFirst;
                    double newYSecond;

                    newXFirst = (ball.XDirection * (ball.Mass - colisionBall.Mass)
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * colisionBall.Mass * colisionBall.XDirection) 
                        / (ball.Mass + colisionBall.Mass));
                    newYFirst = (ball.YDirection * (ball.Mass - colisionBall.Mass) 
                        / (ball.Mass + colisionBall.Mass) + (2 * colisionBall.Mass 
                        * colisionBall.YDirection) / (ball.Mass + colisionBall.Mass));

                    newXSecond = (colisionBall.XDirection 
                        * (colisionBall.Mass - ball.Mass) 
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * ball.Mass * ball.XDirection) 
                        / (ball.Mass + colisionBall.Mass));
                    newYSecond = (colisionBall.YDirection
                        * (colisionBall.Mass - ball.Mass) 
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * ball.Mass * ball.YDirection) 
                        / (ball.Mass + colisionBall.Mass));

                    ball.XDirection = newXFirst;
                    ball.YDirection = newYFirst;

                    colisionBall.XDirection = newXSecond;
                    colisionBall.YDirection = newYSecond;
                }
            }

            private void WindowbarrierChecker(BallApi ball, BoardApi board)
            {
                if (!(ball.X >= ball.Radius && ball.X <= board.Width - ball.Radius))
                {
                    if (ball.XDirection > 0)
                        ball.X = board.Width - ball.Radius;
                    else
                        ball.X = ball.Radius;

                    ball.XDirection *= -1;
                }


                if (!(ball.Y >= ball.Radius && ball.Y <= board.Height - ball.Radius))
                {
                    if (ball.YDirection > 0)
                        ball.Y = board.Height - ball.Radius;
                    else
                        ball.Y = ball.Radius;

                    ball.YDirection *= -1;
                }
            }

            private BallApi? CheckDirection(BallApi ball)
            {
                foreach (BallApi secondBall in _dataLayer.GetBalls())
                {
                    if (secondBall == ball)
                        continue;

                    double environment = Math.Sqrt((ball.X - secondBall.X) * (ball.X - secondBall.X) +
                                                (ball.Y - secondBall.Y) * (ball.Y - secondBall.Y));

                    if (environment <= ball.Radius + secondBall.Radius)
                        return secondBall;
                }
                return null;
            }
        }
    }
}