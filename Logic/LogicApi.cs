using System.Numerics;
using Data;

namespace Logic
{
    public abstract class LogicAPi
    {
        public event EventHandler<OnPositionChangeEvent>? PositionChange;
        public abstract void SummonBalls(int howMany);
        public abstract void StartSimulation();
        public abstract void StopSimulation();

        protected void OnPositionChange(OnPositionChangeEvent args)
        {
            PositionChange?.Invoke(this, args);
        }

        public static LogicAPi CreateBallsLogic(Vector2 boardSize, BallApi? dataApi = default(BallApi))
        {
            dataApi ??= BallApi.CreateBallsList(boardSize);
            return new Logic(dataApi);
        }
    }
}