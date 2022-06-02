using System.Numerics;
using Logic;

namespace Presentation.Model
{
   public class MainBoard
   {
      private readonly Vector2 BoardWH;
      private int countOfBalls;
      private LogicAPi LogicLayer;
      public event EventHandler<OnPositionChangeEvent>? BallPositionChange;

      public MainBoard()
      {
         countOfBalls = 0;
         BoardWH = new Vector2(1000, 600);
         PrepareBallsLogic();
      }

      public void StartSimulation()
      {
         LogicLayer.SummonBalls(countOfBalls);
         LogicLayer.StartSimulation();
      }

      public void StopSimulation()
      {
         LogicLayer.StopSimulation();
         PrepareBallsLogic();
      }

      private void PrepareBallsLogic()
      {
         LogicLayer = LogicAPi.CreateBallsLogic(BoardWH);
         LogicLayer.PositionChange += OnBallsLogicPositionChange;
      }

      private void OnBallsLogicPositionChange(object sender, Logic.OnPositionChangeEvent args)
      {
         BallPositionChange?.Invoke(this, new OnPositionChangeEvent(new CircleAdapter(args.Ball)));
      }

      public void SetBallsNumber(int count)
      {
         countOfBalls = count;
      }

      public int GetCountOfBalls()
      {
         return countOfBalls;
      }

      public void OnBallPositionChange(OnPositionChangeEvent BallArgs)
      {
         BallPositionChange?.Invoke(this, BallArgs);
      }
    }
}