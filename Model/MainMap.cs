using Data;
using LogicLayer;

namespace Presentation.Model
{
    public class MainMap
    {
        private readonly BallService _ballService;
        private int _width;
        private int _height;

        public void Tick()
        {
           _ballService.DoTick();
        }

        public MainMap(int w, int h)
        {
            _width = w;
            _height = h;
            _ballService = new BallService(_width, _height);
        }

        public List<Ball> GetBalls()
        {
            return _ballService.GetAllBalls();
        }

        public void CreateBalls(int amount) 
        {
            _ballService.SummonBalls(amount);
        }

        public void ClearMap()
        {
            _ballService.ClearMap();
        }
    }
}