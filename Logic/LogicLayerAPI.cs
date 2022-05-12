using Data;
using System.ComponentModel;

namespace Logic
{
    public abstract class LogicLayerAbstractAPI
    {
        public static LogicLayerAbstractAPI CreateLogic(DataLayerAbstractAPI? data = default)
        {
            return new LogicLayer(data ?? DataLayerAbstractAPI.CreateAPI());
        }

        public abstract void CreateData(int height, int width, int numberOfBalls, int radiusOfBalls);
        public abstract void StartAnimation();
        public abstract void StopAnimation();

        public abstract List<BallTransformationApi> GetBalls();
        private List<BallTransformationApi> ballOperators;

        internal class LogicLayer : LogicLayerAbstractAPI
        {

            private readonly DataLayerAbstractAPI _dataLayer;

            internal LogicLayer(DataLayerAbstractAPI dataLayerAbstractAPI)
            {
                _dataLayer = dataLayerAbstractAPI;
            }

            public override void CreateData(int height, int width, int numberOfBalls, int radiusOfBalls)
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
                BallApi colisionBall = CheckDirectory(ball);
                if (colisionBall != null)
                {
                    double newXFirst;
                    double newXSecond;
                    double newYFirst;
                    double newYSecond;

                    newXFirst = (ball.XDirectory * (ball.Mass - colisionBall.Mass) 
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * colisionBall.Mass * colisionBall.XDirectory) 
                        / (ball.Mass + colisionBall.Mass));
                    newYFirst = (ball.YDirectory * (ball.Mass - colisionBall.Mass) 
                        / (ball.Mass + colisionBall.Mass) + (2 * colisionBall.Mass 
                        * colisionBall.YDirectory) / (ball.Mass + colisionBall.Mass));

                    newXSecond = (colisionBall.XDirectory 
                        * (colisionBall.Mass - ball.Mass) 
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * ball.Mass * ball.XDirectory) 
                        / (ball.Mass + colisionBall.Mass));
                    newYSecond = (colisionBall.YDirectory * (colisionBall.Mass - ball.Mass) 
                        / (ball.Mass + colisionBall.Mass) 
                        + (2 * ball.Mass * ball.YDirectory) 
                        / (ball.Mass + colisionBall.Mass));

                    ball.XDirectory = newXFirst;
                    ball.YDirectory = newYFirst;

                    colisionBall.XDirectory = newXSecond;
                    colisionBall.YDirectory = newYSecond;
                }
            }

            private void WindowbarrierChecker(BallApi ball, BoardApi board)
            {
                if (!(ball.X >= ball.Radius && ball.X <= board.Width - ball.Radius))
                {
                    if (ball.XDirectory > 0)
                        ball.X = board.Width - ball.Radius;
                    else
                        ball.X = ball.Radius;

                    ball.XDirectory *= -1;
                }


                if (!(ball.Y >= ball.Radius && ball.Y <= board.Height - ball.Radius))
                {
                    if (ball.YDirectory > 0)
                        ball.Y = board.Height - ball.Radius;
                    else
                        ball.Y = ball.Radius;

                    ball.YDirectory *= -1;
                }
            }

            private BallApi? CheckDirectory(BallApi ball)
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