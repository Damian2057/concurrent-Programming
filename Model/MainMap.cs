using LogicLayer;

namespace Model
{
    public class MainMap : MainMapApi
    {
        private readonly LogicLayerApi _logic;

        public MainMap(LogicLayerApi logicLayer)
        {
            _logic = logicLayer;
        }
    }
}