using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class MainMapApi
    {
        public static MainMapApi CreateAPI(LogicLayerApi logicLayer = default)
        {
            return new MainMap(logicLayer ?? LogicLayerApi.CreateLogic());
        }
    }
}
