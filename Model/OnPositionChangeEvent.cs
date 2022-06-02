
namespace Presentation.Model
{
    public class OnPositionChangeEvent : EventArgs
    {
        public readonly ICircleAdapter Ball;

        public OnPositionChangeEvent(ICircleAdapter ball)
        {
            Ball = ball;
        }
    }
}