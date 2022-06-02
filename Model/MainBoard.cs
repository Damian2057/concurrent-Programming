using System.Numerics;
using Logic;

namespace Presentation.Model
{
   public class MainBoard
   {
      private readonly Vector2 BoardWH;
      private int countOfBalls;
      private LogicAPi LogicLayer;

      public MainBoard()
      {
         BoardWH = new Vector2(650, 400);
         countOfBalls = 0;
         this.PrepareBallsLogic();
      }

      public void StartSimulation()
      {
         LogicLayer.SummonBalls(countOfBalls);
         LogicLayer.StartSimulation();
      }

      public void StopSimulation()
      {
         LogicLayer.StopSimulation();
         this.PrepareBallsLogic();
      }

      private void PrepareBallsLogic()
      {
         LogicLayer = LogicAPi.CreateBallsLogic(BoardWH);
         LogicLayer.PositionChange += this.OnBallsLogicPositionChange;
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

      public void OnBallPositionChange(OnPositionChangeEvent args)
      {
         BallPositionChange?.Invoke(this, args);
      }

      public event EventHandler<OnPositionChangeEvent>? BallPositionChange;
    }
}