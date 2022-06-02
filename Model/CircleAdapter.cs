using System.Numerics;
using Logic;

namespace Presentation.Model
{
    public interface ICircleAdapter
    {
        Vector2 Position { get; }
        float Radius { get; }
        int ID { get; }
        string Color { get; }
    }

    internal class CircleAdapter : ICircleAdapter
    {
        private readonly IBallAdapter ball;

        public CircleAdapter(IBallAdapter ball)
        {
            this.ball = ball;
        }

        public Vector2 Position
        {
            get
            {
                return ball.Position;
            }
        }
        public float Radius
        {
            get
            {
                return ball.Radius;
            }
        }
        public int ID
        {
            get
            {
                return ball.ID;
            }
        }

        public string Color
        {
            get => ball.Color;
        }
    }
}