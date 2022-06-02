using System.Numerics;
using Data;

namespace Logic
{
    internal static class CollistionDetector
    {
        public static BallInterface? CheckBallsDirection(BallInterface ball, IEnumerable<BallInterface> ballsList)
        {
            foreach (BallInterface? checkedBall in ballsList)
            {
                if (ball.BallID == checkedBall.BallID)
                {
                    continue; //Skip the same ball
                }

                if (CheckSurroundingOfBalls(ball, checkedBall))
                {
                    return checkedBall;
                }
            }

            return null;
        }

        private static bool CheckSurroundingOfBalls(BallInterface ball, BallInterface checkedBall)
        {


            Vector2 xPosCenter = ball.Position + (Vector2.One * ball.Radius / 2) + ball.Speed * (16 / 1000f);
            Vector2 yPosCenter = checkedBall.Position + (Vector2.One * checkedBall.Radius / 2) + checkedBall.Speed * (16 / 1000f);





            float div = Vector2.Distance(xPosCenter, yPosCenter);
            float sumOfBothRadius = (ball.Radius + checkedBall.Radius) / 2f;

            return div <= sumOfBothRadius;
        }

        public static void BoundBalls(BallInterface ball, BallInterface bouncedBall)
        {
            Vector2 middleFirstBall = ball.Position + (Vector2.One * ball.Radius / 2);
            Vector2 middleSecondBall = bouncedBall.Position + (Vector2.One * bouncedBall.Radius / 2);




            Vector2 normalVector = Vector2.Normalize(middleSecondBall - middleFirstBall);
            Vector2 targetVector = new(-normalVector.Y, normalVector.X);

            float firstBallSpeed = Vector2.Dot(normalVector, ball.Speed);
            float secondBallSpeed = Vector2.Dot(targetVector, ball.Speed);

            float firstBouncedSpeed = Vector2.Dot(normalVector, bouncedBall.Speed);
            float secondBouncedSpeed = Vector2.Dot(targetVector, bouncedBall.Speed);




            float newBallSpeedDirectory = (firstBallSpeed * (ball.Mass - bouncedBall.Mass)
                  + 2 * bouncedBall.Mass * firstBouncedSpeed) / (ball.Mass + bouncedBall.Mass);

            float newBouncedSpeedDirectory = (firstBouncedSpeed * (bouncedBall.Mass - ball.Mass)
                  + 2 * ball.Mass * firstBallSpeed) / (ball.Mass + bouncedBall.Mass);



            Vector2 newDirectoryFirst = Vector2.Multiply(normalVector, newBallSpeedDirectory)
                  + Vector2.Multiply(targetVector, secondBallSpeed);
            Vector2 newDirectorySecond = Vector2.Multiply(normalVector, newBouncedSpeedDirectory)
                  + Vector2.Multiply(targetVector, secondBouncedSpeed);

            ball.Speed = newDirectoryFirst;
            bouncedBall.Speed = newDirectorySecond;
        }

        public static void BoundFromBarrier(BallInterface ball, Vector2 boardWH)
        {
            Vector2 position = ball.Position + ball.Speed * (16 / 1000f);
            if (position.X <= 0 || position.X + ball.Radius >= boardWH.X)
            {
                ball.Speed = new Vector2(-ball.Speed.X, ball.Speed.Y);
            }






            if (position.Y <= 0 || position.Y + ball.Radius >= boardWH.Y)
            {
                ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
            }
        }

    }
}
