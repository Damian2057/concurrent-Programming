using Logic;
using System.Collections.ObjectModel;


namespace Presentation.Model
{
    public abstract class ModelApi
    {
        private ObservableCollection<CircleApi> _circle = new ObservableCollection<CircleApi>();

        public static ModelApi CreateModel(LogicLayerAbstractAPI logicLayer = default)
        {
            return new ModelLayer(logicLayer ?? LogicLayerAbstractAPI.CreateLogic());
        }

        public abstract void CreateCircle(int height, int width, int numberOfBalls, int radiusOfBalls);
        public abstract void StartAnimation();
        public abstract void StopAnimation();

        public ObservableCollection<CircleApi> Circles
        {
            get => _circle;
            set => _circle = value;
        }

        internal class ModelLayer : ModelApi
        {

            private readonly LogicLayerAbstractAPI _logicLayer;

            public ModelLayer(LogicLayerAbstractAPI logicLayer)
            {
                _logicLayer = logicLayer;
            }

            public override void CreateCircle(int height, int width, int count, int radius)
            {
                _logicLayer.StopAnimation();
                _logicLayer.CreateData(height, width, count, radius);

                _circle.Clear();

                foreach (BallTransformationApi ballTransform in _logicLayer.GetBalls())
                {
                    _circle.Add(CircleApi.CreateCircle(ballTransform));
                }
            }
            public override void StartAnimation()
            {
                _logicLayer.StartAnimation();
            }

            public override void StopAnimation()
            {
                _logicLayer.StopAnimation();
            }
        }
    }
}