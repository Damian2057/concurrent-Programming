using System.Collections;
using Data;
using LogicLayer.Exceptions;
using InvalidDataException = LogicLayer.Exceptions.InvalidDataException;

namespace LogicLayer
{
    public class BallService
    {
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly BallRepositoryApi _ballRepository = BallRepositoryApi.CreateRepository();
        private readonly int _ballMinRadius;
        private readonly int _ballMaxRadius;

        public BallService(int mapWidth, int mapHeight)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
            _ballMinRadius = Math.Min(mapHeight, mapWidth) / 60;
            _ballMaxRadius = Math.Max(mapWidth, mapHeight) / 20;

        }

        public int GetMapWidth()
        {
            return _mapWidth;
        }

        public int GetMapHeight()
        {
            return _mapHeight;
        }

        public int GetBallsMinRadius()
        {
            return _ballMinRadius;
        }

        public int GetBallsMaxRadius()
        {
            return _ballMaxRadius;
        }

        public void CreateBall(int ID, int x, int y, int xDirection, int yDirection) 
        {
            if(CheckForExistingID(ID) 
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

        public void GenerateRandomBall()
        {
            Random rnd = new Random();
            int xrand = 0, yrand = 0; 
            while(xrand == 0 || yrand == 0)
            {
                xrand = rnd.Next(-5, 5);
                yrand = rnd.Next(-5, 5);
            }
            

            CreateBall(AutoId()
                , rnd.Next(_ballMaxRadius, _mapWidth - _ballMaxRadius)
                ,rnd.Next(_ballMaxRadius, _mapHeight - _ballMaxRadius)
                , xrand
                , yrand);
        }

        public void SummonBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateRandomBall();
            }
        }

        public int AutoId()
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

        public void DoTick()
        {
            
            foreach (var ball in GetAllBalls())
            {
                if (ball.XPos + ball.XDirection + ball.Radius < ball.Radius*2 || ball.XPos + ball.XDirection + ball.Radius > _mapWidth)
                {
                    ball.XDirection = ball.XDirection * (-1);
                }
                if (ball.YPos + ball.YDirection + ball.Radius < ball.Radius*2 || ball.YPos + ball.YDirection + ball.Radius > _mapHeight)
                {
                    ball.YDirection = ball.YDirection * (-1);
                }
                ball.XPos += ball.XDirection;
                ball.YPos += ball.YDirection;
            }
        }

        public bool CheckForExistingID(int ID)
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

        public BallApi GetBallByID(int ID)
        {
            foreach (var obj in _ballRepository.GetAllBalls())
            {
                if (ID == obj.BallID)
                {
                    return _ballRepository.GetAllBalls().ElementAt(ID);
                }
            }

            throw new InvalidDataException("The ball with the given ID is not in the list");
        }

        public void RemoveBallByID(int ID)
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

        public List<BallApi> GetAllBalls()
        {
            return _ballRepository.GetAllBalls();
        }

        public void ClearMap()
        {
            _ballRepository.ClearStorage();
        }
    }
}