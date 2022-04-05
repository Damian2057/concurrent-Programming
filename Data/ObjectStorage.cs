using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ObjectStorage
    {
        private List<Ball> _balls = new();

        public void AddBall(Ball obj)
        {
            _balls.Add(obj);
        }

        public List<Ball> GetAllBalls()
        {
            return _balls;
        }

        public void RemoveBall(Ball obj)
        {
            _balls.Remove(obj);
        }

        public void ClearStorage()
        {
            _balls.Clear();
        }

    }
}
