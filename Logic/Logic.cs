using Data;

namespace Logic
{
    internal class Logic : LogicAPi
    {
        private readonly BallApi DataLayer;
        private readonly Mutex simulationMutex = new(false);

        public Logic(BallApi? dataBalls)
        {
            DataLayer = dataBalls;
        }

        public override void SummonBalls(int howMany)
        {
            DataLayer.AddBalls(howMany);
        }

        public override void StartSimulation()
        {
            DataLayer.PositionChange += this.OnBallsOnPositionChange;
            DataLayer.StartAction();
        }

        private void OnBallsOnPositionChange(object _, Data.OnPositionChangeEvent args)
        {
            this.DetectBallsImpact(args.SenderBall, args.Balls);
            CollistionDetector.BoundFromBarrier(args.SenderBall, DataLayer.BoardHeightAndWidth);
            OnPositionChangeEvent newArgs = new(new BallAdapter(args.SenderBall));
            this.OnPositionChange(newArgs);
        }

        private void DetectBallsImpact(BallInterface ball, IList<BallInterface> ballList)
        {
            simulationMutex.WaitOne();
            try
            {
                BallInterface? collidedBall = CollistionDetector.CheckBallsDirection(ball, ballList);
                if (collidedBall != null)
                {
                    CollistionDetector.BoundBalls(ball, collidedBall);
                }
            }
            finally
            {
                simulationMutex.ReleaseMutex();
            }
        }

        public override void StopSimulation()
        {
            DataLayer.StopAction();
        }
    }
}
