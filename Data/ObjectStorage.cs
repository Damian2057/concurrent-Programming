using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ObjectStorage<T>
    {
        private List<T> _balls = new();

        public void AddBall(T obj)
        {
            _balls.Add(obj);
        }

        public List<T> GetAllBalls()
        {
            return _balls;
        }

        public void RemoveBall(T obj)
        {
            _balls.Remove(obj);
        }

        public void ClearStorage()
        {
            _balls.Clear();
        }
    }
}
