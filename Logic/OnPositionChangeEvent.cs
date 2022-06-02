
namespace Logic
{
    public class OnPositionChangeEvent : EventArgs
    {
        public IBallAdapter Ball;

        public OnPositionChangeEvent(IBallAdapter ball)
        {
            Ball = ball;
        }
    }
}