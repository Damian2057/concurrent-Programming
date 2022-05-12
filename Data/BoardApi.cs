using System;
using System.Collections.Generic;

namespace Data
{
    public abstract class BoardApi
    {
        public static BoardApi CreateBoard(int height, int width)
        {
            return new Board(height,width);
        }

        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract string Color { get; }
        public abstract void AddBall(BallApi ball);
        public abstract List<BallApi> GetBalls();

        internal class Board : BoardApi
        {
            private readonly int _width;
            private readonly int _height;
            private readonly List<BallApi> _balls;

            public Board(int height, int width)
            {
                _balls = new();
                _width = width;
                _height = height;
            }

            public override int Width { get => _width; }

            public override int Height { get => _height; }

            public override string Color
            {
                get => Color;
            }

            public override void AddBall(BallApi ball)
            {
                _balls.Add(ball);
            }

            public override List<BallApi> GetBalls()
            {
                return _balls;
            }

        }
    }
}