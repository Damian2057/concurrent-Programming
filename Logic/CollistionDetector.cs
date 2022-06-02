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

        public static void BoundBalls(BallInterface ball, BallInterface bouncedBall)
        {
            Vector2 middleFirstBall = ball.Position + (Vector2.One * ball.Radius / 2);
            Vector2 middleSecondBall = bouncedBall.Position + (Vector2.One * bouncedBall.Radius / 2);

            Vector2 unitNormalVector = Vector2.Normalize(middleSecondBall - middleFirstBall);
            Vector2 unitTangentVector = new(-unitNormalVector.Y, unitNormalVector.X);

            float velocityOneNormal = Vector2.Dot(unitNormalVector, ball.Speed);
            float velocityOneTangent = Vector2.Dot(unitTangentVector, ball.Speed);
            float velocityTwoNormal = Vector2.Dot(unitNormalVector, bouncedBall.Speed);
            float velocityTwoTangent = Vector2.Dot(unitTangentVector, bouncedBall.Speed);

            float newNormalVelocityOne = (velocityOneNormal * (ball.Mass - bouncedBall.Mass)
                  + 2 * bouncedBall.Mass * velocityTwoNormal) / (ball.Mass + bouncedBall.Mass);
            float newNormalVelocityTwo = (velocityTwoNormal * (bouncedBall.Mass - ball.Mass)
                  + 2 * ball.Mass * velocityOneNormal) / (ball.Mass + bouncedBall.Mass);

            Vector2 newVelocityOne = Vector2.Multiply(unitNormalVector, newNormalVelocityOne)
                  + Vector2.Multiply(unitTangentVector, velocityOneTangent);
            Vector2 newVelocityTwo = Vector2.Multiply(unitNormalVector, newNormalVelocityTwo)
                  + Vector2.Multiply(unitTangentVector, velocityTwoTangent);

            ball.Speed = newVelocityOne;
            bouncedBall.Speed = newVelocityTwo;
        }
    }
}
