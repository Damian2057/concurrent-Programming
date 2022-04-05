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
        private ArrayList _balls = new ArrayList();

        public void AddBall(Ball obj)
        {
            _balls.Add(obj);
        }

        public ArrayList GetAllBalls()
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
