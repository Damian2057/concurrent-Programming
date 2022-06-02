using System.Numerics;

namespace Data
{
    public abstract class BallApi
    {
        public CancellationTokenSource CancelSimmulation { get; }

        protected BallApi(Vector2 boardHW)
        {
            BoardHeightAndWidth = boardHW;
            CancelSimmulation = new CancellationTokenSource();
        }
        public static BallApi? CreateBallsList(Vector2 boardHW)
        {
            return new BallBoard(boardHW);
        }

        public Vector2 BoardHeightAndWidth { get; protected set; }
        public abstract void AddBalls(int countOfBalls);

        public event EventHandler<OnPositionChangeEvent>? PositionChange;

        protected void OnPositionChange(OnPositionChangeEvent argv)
        {
            PositionChange?.Invoke(this, argv);
        }

        public abstract void StartAction();
        public abstract void StopAction();
    }
}
