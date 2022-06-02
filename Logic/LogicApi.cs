using System.Numerics;
using Data;

namespace Logic
{
    public abstract class LogicAPi
    {
        public abstract void SummonBalls(int countOfBalls);
        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public event EventHandler<OnPositionChangeEvent>? PositionChange;

        public static LogicAPi CreateBallsLogic(Vector2 boardSize, BallApi? dataApi = default(BallApi))
        {
            dataApi ??= BallApi.CreateBallsList(boardSize);
            return new Logic(dataApi);
        }

        protected void OnPositionChange(OnPositionChangeEvent BallData)
        {
            PositionChange?.Invoke(this, BallData);
        }
    }
}