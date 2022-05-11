using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board
    {
        private readonly int _width;
        private readonly int _height;
        private readonly List<BallApi> _balls = new();

        public Board(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public void AddBall(BallApi ball)
        {
            _balls.Add(ball);
        }

        public List<BallApi> GetBalls()
        {
            return _balls;
        }

        public void ClearBalls()
        {
            _balls.Clear();
        }
    }
}
