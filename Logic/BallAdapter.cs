using System.Numerics;
using Data;

namespace Logic
{
    public interface IBallAdapter
    {
        Vector2 Position { get; }
        float Radius { get; }
        int ID { get; }
        string Color { get; }
    }

    internal class BallAdapter : IBallAdapter
    {
        private readonly BallInterface handedBall;

        public BallAdapter(BallInterface ball)
        {
            handedBall = ball;
        }

        public Vector2 Position
        {
            get
            {
                return handedBall.Position;
            }
        }

        public float Radius
        {
            get
            {
                return handedBall.Radius;
            }
        }

        public int ID
        {
            get
            {
                return handedBall.BallID;
            }
        }

        public string Color
        {
            get
            {
                return handedBall.Color;
            }
        }
    }
}