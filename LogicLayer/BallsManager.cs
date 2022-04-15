using System.Collections;
using Data;
using LogicLayer.Exceptions;
using InvalidDataException = LogicLayer.Exceptions.InvalidDataException;

namespace LogicLayer
{
    public class BallsManager
    {
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly ObjectStorage<Ball> _objectStorage = new();
        private readonly int _ballMinRadius;
        private readonly int _ballMaxRadius;

        public BallsManager(int mapWidth, int mapHeight)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
            _ballMinRadius = Math.Min(mapHeight, mapWidth) / 60;
            _ballMaxRadius = Math.Max(mapWidth, mapHeight) / 30;

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


        public void CreateBall(int ID, int x, int y, int xDirectory, int yDirectory) 
        {
            if(CheckForDuplicateID(ID) 
               || (x < _ballMinRadius || x > _mapWidth - _ballMinRadius 
                         || y < _ballMinRadius || y > _mapHeight - _ballMinRadius 
                         || yDirectory > _mapHeight - _ballMinRadius || yDirectory < ((-1) * _mapHeight + _ballMinRadius) 
                         || xDirectory > _mapWidth - _ballMinRadius || xDirectory < ((-1) * _mapWidth + _ballMinRadius)))
            {
                throw new InvalidDataException("The ball parameters entered are invalid");
            }
            else
            {
                Random rnd = new Random();
                Ball newBall = new Ball(ID, x, y, rnd.Next(_ballMinRadius, _ballMaxRadius), xDirectory, yDirectory);
                _objectStorage.AddBall(newBall);
            }
        }

        public void GenerateRandomBall()
        {
            Random rand = new Random();
            CreateBall(AutoID()
                , rand.Next(_ballMaxRadius, _mapWidth - _ballMaxRadius)
                ,rand.Next(_ballMaxRadius, _mapHeight - _ballMaxRadius)
                , rand.Next((-1) * _mapWidth + _ballMaxRadius, _mapWidth - _ballMaxRadius)
                , rand.Next((-1) * _mapHeight + _ballMaxRadius, _mapHeight - _ballMaxRadius));
        }

        public void SummonBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateRandomBall();
            }
        }

        public int AutoID()
        {
            int max = 0;
            foreach (Ball ball in GetAllBalls())
            {
                if (max < ball.GetID())
                {
                    max = ball.GetID();
                }
            }

            return max + 1;
        }

        public void DoTick()
        {
            //TODO: add ball radius to condition
            foreach (Ball ball in GetAllBalls())
            {
                if (ball.XPos + ball.XDirection + ball.Radius < 0 || ball.XPos + ball.XDirection + ball.Radius > _mapWidth)
                {
                    ball.XDirection = ball.XDirection * (-1);
                }
                if (ball.YPos + ball.YDirection + ball.Radius < 0 || ball.YPos + ball.YDirection + ball.Radius > _mapHeight)
                {
                    ball.YDirection = ball.YDirection * (-1);
                }
                ball.XPos += ball.XDirection;
                ball.YPos += ball.YDirection;
            }
        }

        public bool CheckForDuplicateID(int ID)
        {
            foreach (Ball obj in _objectStorage.GetAllBalls())
            {
                if (ID == obj.GetID())
                {
                    return true;
                }
            }

            return false;
        }

        public Ball GetBallByID(int ID)
        {
            foreach (Ball obj in _objectStorage.GetAllBalls())
            {
                if (ID == obj.GetID())
                {
                    return _objectStorage.GetAllBalls().ElementAt(ID);
                }
            }

            throw new InvalidDataException("The ball with the given ID is not in the list");
        }

        public void RemoveBallByID(int ID)
        {
            foreach (Ball obj in _objectStorage.GetAllBalls())
            {
                if (ID == obj.GetID())
                { 
                    _objectStorage.RemoveBall(obj);
                    return;
                }
            }

            throw new InvalidDataException("The ball with the given ID is not in the list");
        }

        public List<Ball> GetAllBalls()
        {
            return _objectStorage.GetAllBalls();
        }

        public void ClearMap()
        {
            _objectStorage.ClearStorage();
        }
    }
}