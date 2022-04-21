using LogicLayer;
using Data;
using System.Collections.Generic;

namespace Presentation.Model
{

    public abstract class MainMapApi
    {
        public static MainMapApi createMap(int w, int h)
        {
            return new MainMap(w, h);
        }

        public abstract void Tick();
        public abstract List<BallApi> GetBalls();
        public abstract void CreateBalls(int amount);
        public abstract void ClearMap();


        private class MainMap : MainMapApi
        {
            private readonly BallServiceApi _ballService;
            private int _width;
            private int _height;

            public override void Tick()
            {
                _ballService.DoTick();
            }

            public MainMap(int w, int h)
            {
                _width = w;
                _height = h;
                _ballService = BallServiceApi.CreateLogic(_width, _height);
            }

            public override List<BallApi> GetBalls()
            {
                return _ballService.GetAllBalls();
            }

            public override void CreateBalls(int amount)
            {
                _ballService.SummonBalls(amount);
            }

            public override void ClearMap()
            {
                _ballService.ClearMap();
            }
        }
    }
}