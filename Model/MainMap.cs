using Data;
using LogicLayer;

namespace Presentation.Model
{
    public class MainMap
    {
        private readonly BallsManager _ballsManager = new BallsManager(600, 600);


        public List<Ball> GetBalls()
        {
            return _ballsManager.GetAllBalls();
        }

        public void CreateBalls(int amount) 
        {
            _ballsManager.SummonBalls(amount);
        }

        public void ClearMap()
        {
            _ballsManager.ClearMap();
        }
    }
}