using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;

namespace Data
{
    public interface BallInterface : ISerializable
    {
        int BallID { get; }
        Vector2 Position { get; }
        Vector2 Speed { get; set; }
        float Radius { get; }
        float Mass { get; }
        string Color { get; }
        public void Move();

        public event EventHandler<OnBallPositionChangeEvent>? PositionChange;
    }

    public class OnBallPositionChangeEvent
    {
        public BallInterface Ball;

        public OnBallPositionChangeEvent(BallInterface ball)
        {
            Ball = ball;
        }
    }

    internal class Ball : BallInterface
    {
        private readonly BallApi owner;

        public Ball(int ID, Vector2 position, float radius, float weight, Vector2 velocity, BallApi owner, string color)
        {
            Position = position;
            Speed = velocity;
            this.owner = owner;
            Mass = weight;
            Radius = radius;
            BallID = ID;
            Color = color;
        }

        public Vector2 Position { get; private set; }
        public float Radius { get; }
        public float Mass { get; }
        public string Color { get; }
        public Vector2 Speed { get; set; }
        public int BallID { get; }

        public event EventHandler<OnBallPositionChangeEvent>? PositionChange;

        public async void Move()
        {
            Stopwatch stopwatch = new();
            float timeDifference = 0f;
            while (!owner.CancelSimmulation.Token.IsCancellationRequested)
            {
                stopwatch.Start();
                OnBallPositionChangeEvent newArgs = new(this);
                PositionChange?.Invoke(this, newArgs);

                Vector2 nextPosition = Position + Vector2.Multiply(Speed, timeDifference);
                Position = Bounce(nextPosition);

                await Task.Delay(4, owner.CancelSimmulation.Token).ContinueWith(_ => { });
               
                stopwatch.Stop();
                timeDifference = stopwatch.ElapsedMilliseconds / 1000f; // Delta time calculation
                stopwatch.Reset();
            }
        }

        private Vector2 Bounce(Vector2 nextPos)
        {
            if (nextPos.X < 0)
            {
                nextPos.X = -1;
            }
            if (Radius + nextPos.X > owner.BoardHeightAndWidth.X)
            {
                nextPos.X = owner.BoardHeightAndWidth.X - Radius + 1;
            }
            if (nextPos.Y < 0)
            {
                nextPos.Y = -1;

            }
            if (Radius + nextPos.Y > owner.BoardHeightAndWidth.Y)
            {
                nextPos.Y = owner.BoardHeightAndWidth.Y - Radius + 1;

            }

            return nextPos;
        }

        public void GetObjectData(SerializationInfo ballInfo, StreamingContext context)
        {
            ballInfo.AddValue("BallID", BallID);
            ballInfo.AddValue("PositionXY", Position);
            ballInfo.AddValue("SpeedDirection", Speed);
            ballInfo.AddValue("BallRadius", Radius);
            ballInfo.AddValue("BallMass", Mass);        
        }
    }
}