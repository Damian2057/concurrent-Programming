
namespace Data
{
    public class OnPositionChangeEvent : EventArgs
    {
        public readonly IList<BallInterface> Balls;
        public readonly BallInterface SenderBall;

        public OnPositionChangeEvent(BallInterface senderBall, IList<BallInterface> balls)
        {
            Balls = balls;
            SenderBall = senderBall;
        }
    }
}
