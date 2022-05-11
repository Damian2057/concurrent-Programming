using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public abstract class LogicLayerApi
    {
        public static LogicLayerApi CreateLogic(DataLayerApi data = default)
        {
            return new LogicLayer(data ?? DataLayerApi.CreateData());
        }

        public abstract void CreateMap(int width, int height, int numberOfBalls, int radius);
        public abstract void ResumeBalls();
        public abstract void StopBalls();
        public abstract List<BallTransform> GetBalls();

    }
}
