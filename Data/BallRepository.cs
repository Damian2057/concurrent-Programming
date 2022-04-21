using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallRepositoryApi
    {
        public static BallRepositoryApi CreateRepository()
        {
            return new BallRepository();
        }

        public abstract void AddBall(BallApi obj);
        public abstract List<BallApi> GetAllBalls();
        public abstract void RemoveBall(BallApi obj);
        public abstract void ClearStorage();


        private class BallRepository : BallRepositoryApi
        {
            private List<BallApi> _balls = new();

            public override void AddBall(BallApi obj)
            {
                _balls.Add(obj);
            }

            public override void ClearStorage()
            {
                _balls.Clear();
            }

            public override List<BallApi> GetAllBalls()
            {
                return _balls;
            }

            public override void RemoveBall(BallApi obj)
            {
                _balls.Remove(obj);
            }
        }
    }
}
