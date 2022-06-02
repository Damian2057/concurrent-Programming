using System.Numerics;

namespace Data
{
    internal class BallBoard : BallApi
    {
        private const int MaxSpeed = 300;
        private const int MinSpeed = 20;
        private const int MinRadius = 30;
        private const int MaxRadius = 60;
        private readonly List<BallInterface> ballsList;
        private readonly BallApiListLogger BallLoger = new BallListLogger();

        public BallBoard(Vector2 boardSize) : base(boardSize)
        {
            ballsList = new List<BallInterface>();
        }

        public override void AddBalls(int countOfBalls)
        {
            Random rand = new();
            for (int i = 0; i < countOfBalls; i++)
            {
                int BallRadius = rand.Next(MinRadius, MaxRadius);
                int BallWeight = BallRadius;

                Vector2 BallCoordinates = this.CreateBallCoordinates(BallRadius);
                Vector2 DirectionSpeed = this.CreateDirectionSpeed();
                var color = ColorApi.CreateColor();
                BallInterface ball = new Ball(ballsList.Count, BallCoordinates, BallRadius, BallWeight, DirectionSpeed, this,color.PickColor());

                ballsList.Add(ball);
            }
        }

        private Vector2 CreateBallCoordinates(int ballRadius)
        {
            Random random = new();
            float xPos = 0;
            float yPos = 0;
            int attempt = 0;
            bool PossitionStatus = false;
            while (!PossitionStatus)
            {
                xPos = random.Next(ballRadius, (int)(BoardHeightAndWidth.X - ballRadius));
                yPos = random.Next(ballRadius, (int)(BoardHeightAndWidth.Y - ballRadius));
                PossitionStatus = this.CheckSpace(new Vector2(xPos, yPos), ballRadius);

                if (attempt >= 100)
                {
                    throw new NoAvailableSpaceForNewBallException();
                }
                attempt++;
            }
            return new Vector2(xPos, yPos);
        }

        private bool CheckSpace(Vector2 position, int ballRadius)
        {
            foreach (BallInterface? ball in ballsList)
            {
                if (this.CheckEnvironment(ball.Position, ball.Radius, position, ballRadius))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckEnvironment(Vector2 position1, float radius1, Vector2 position2, float radius2)
        {
            float distSq = (position1.X - position2.X) * (position1.X - position2.X) + (position1.Y - position2.Y) * (position1.Y - position2.Y);
            float radSumSq = (radius1 + radius2) * (radius1 + radius2);
            return distSq <= radSumSq;
        }

        private Vector2 CreateDirectionSpeed()
        {
            Random rng = new();
            int x = rng.Next(-MaxSpeed, MaxSpeed);
            int y = rng.Next(-MaxSpeed, MaxSpeed);
            if (Math.Abs(x) < MinSpeed)
            {
                x = MinSpeed;
            }

            if (Math.Abs(y) < MinSpeed)
            {
                y = MinSpeed;
            }

            return new Vector2(x, y);
        }

        public override void StartAction()
        {
            if (CancelSimmulation.IsCancellationRequested)
            {
                return;
            }

            foreach (BallInterface? ball in ballsList)
            {
                ball.PositionChange += this.OnBallOnPositionChange;

                Task.Factory.StartNew(ball.Move, CancelSimmulation.Token);
            }
        }

        private void OnBallOnPositionChange(object _, OnBallPositionChangeEvent args)
        {
            BallLoger.AddLogToSave(args.Ball);
            OnPositionChangeEvent newArgs = new(args.Ball, new List<BallInterface>(ballsList));
            this.OnPositionChange(newArgs);
        }

        public override void StopAction()
        {
            CancelSimmulation.Cancel();
        }
    }
}
