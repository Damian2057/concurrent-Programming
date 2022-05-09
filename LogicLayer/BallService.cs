using System.Collections;
using Data;
using LogicLayer.Exceptions;
using InvalidDataException = LogicLayer.Exceptions.InvalidDataException;

namespace LogicLayer
{
    public abstract class BallServiceApi
    {
        public static BallServiceApi CreateLogic(int mapWidth, int mapHeight, BallRepositoryApi? repo = default)
        {
            return new BallService(mapWidth, mapHeight, repo ?? BallRepositoryApi.CreateRepository());
        }

        public abstract void ClearMap();
        public abstract List<BallApi> GetAllBalls();
        public abstract void RemoveBallByID(int ID);
        public abstract BallApi GetBallByID(int ID);
        public abstract void DoTick();
        public abstract void SummonBalls(int amount);
        public abstract int GetMapWidth();
        public abstract int GetMapHeight();

        private class BallService : BallServiceApi
        {

            private readonly int _mapWidth;
            private readonly int _mapHeight;
            private readonly BallRepositoryApi _ballRepository;
            private readonly int _ballMinRadius;
            private readonly int _ballMaxRadius;

            public BallService(int mapWidth, int mapHeight,BallRepositoryApi repoLayer)
            {
                _ballRepository = repoLayer;
                _mapHeight = mapHeight;
                _mapWidth = mapWidth;
                _ballMinRadius = Math.Min(mapHeight, mapWidth) / 60;
                _ballMaxRadius = Math.Max(mapWidth, mapHeight) / 20;

            }

            private void CreateBall(int ID, int x, int y, int xDirection, int yDirection)
            {
                if (CheckForExistingID(ID)
                   || (x < _ballMinRadius || x > _mapWidth - _ballMinRadius
                             || y < _ballMinRadius || y > _mapHeight - _ballMinRadius
                             || yDirection > _mapHeight - _ballMinRadius || yDirection < ((-1) * _mapHeight + _ballMinRadius)
                             || xDirection > _mapWidth - _ballMinRadius || xDirection < ((-1) * _mapWidth + _ballMinRadius)))
                {
                    throw new InvalidDataException("The ball parameters entered are invalid");
                }
                else
                {
                    Random rnd = new Random();
                    var newBall = BallApi.CreateBall(ID, x, y, rnd.Next(_ballMinRadius, _ballMaxRadius), xDirection, yDirection);
                    _ballRepository.AddBall(newBall);
                }
            }

            private void GenerateRandomBall()
            {
                Random rnd = new Random();
                int xrand = 0, yrand = 0;
                while (xrand == 0 || yrand == 0)
                {
                    xrand = rnd.Next(-5, 5);
                    yrand = rnd.Next(-5, 5);
                }


                CreateBall(AutoId()
                    , rnd.Next(_ballMaxRadius, _mapWidth - _ballMaxRadius)
                    , rnd.Next(_ballMaxRadius, _mapHeight - _ballMaxRadius)
                    , xrand
                    , yrand);
            }

            public override void SummonBalls(int amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    GenerateRandomBall();
                }
            }

            private int AutoId()
            {
                int max = 0;
                foreach (var ball in GetAllBalls())
                {
                    if (max < ball.BallID)
                    {
                        max = ball.BallID;
                    }
                }

                return max + 1;
            }

            public override void DoTick()
            {

                foreach (var ball in GetAllBalls())
                {
                    if (ball.XPos + ball.XDirection + ball.Radius < ball.Radius * 2 || ball.XPos + ball.XDirection + ball.Radius > _mapWidth)
                    {
                        ball.XDirection = ball.XDirection * (-1);
                    }
                    if (ball.YPos + ball.YDirection + ball.Radius < ball.Radius * 2 || ball.YPos + ball.YDirection + ball.Radius > _mapHeight)
                    {
                        ball.YDirection = ball.YDirection * (-1);
                    }
                    ball.XPos += ball.XDirection;
                    ball.YPos += ball.YDirection;
                }
            }

            private bool CheckForExistingID(int ID)
            {
                foreach (var obj in _ballRepository.GetAllBalls())
                {
                    if (ID == obj.BallID)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override BallApi GetBallByID(int ID)
            {
                foreach (var obj in _ballRepository.GetAllBalls())
                {
                    if (ID == obj.BallID)
                    {
                        return obj;
                    }
                }

                throw new InvalidDataException("The ball with the given ID is not in the list");
            }

            public override void RemoveBallByID(int ID)
            {
                foreach (var obj in _ballRepository.GetAllBalls())
                {

                    if (ID == obj.BallID)
                    {
                        _ballRepository.RemoveBall(obj);
                        return;
                    }
                }

                throw new InvalidDataException("The ball with the given ID is not in the list");
            }

            public override List<BallApi> GetAllBalls()
            {
                return _ballRepository.GetAllBalls();
            }

            public override void ClearMap()
            {
                _ballRepository.ClearStorage();
            }

            public override int GetMapWidth()
            {
                return _mapWidth;
            }

            public override int GetMapHeight()
            {
                return _mapHeight;
            }
        }

    }
}