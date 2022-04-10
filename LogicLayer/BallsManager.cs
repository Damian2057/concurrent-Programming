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
        private readonly int _ballRadius;

        public BallsManager(int mapWidth, int mapHeight)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
            _ballRadius = Math.Min(mapHeight, mapWidth) / 60;
        }

        public int GetMapWidth()
        {
            return _mapWidth;
        }

        public int GetMapHeight()
        {
            return _mapHeight;
        }

        public int GetBallsRadius()
        {
            return _ballRadius;
        }

        public void CreateBall(int ID, int x, int y, int xDirectory, int yDirectory) 
        {
            if(IsBallWithID(ID) 
               || (x < _ballRadius || x > _mapWidth - _ballRadius 
                         || y < _ballRadius || y > _mapHeight - _ballRadius 
                         || yDirectory > _mapHeight - _ballRadius || yDirectory < ((-1) * _mapHeight + _ballRadius) 
                         || xDirectory > _mapWidth - _ballRadius || xDirectory < ((-1) * _mapWidth + _ballRadius)))
            {
                throw new InvalidDataException("The ball parameters entered are invalid");
            }
            else
            {
                Ball newBall = new Ball(ID, x, y, _ballRadius, xDirectory, yDirectory);
                _objectStorage.AddBall(newBall);
            }
        }

        public void GenerateRandomBall()
        {
            Random rand = new Random();
            CreateBall(AutoID()
                , rand.Next(_ballRadius, _mapWidth - _ballRadius)
                ,rand.Next(_ballRadius, _mapHeight - _ballRadius)
                , rand.Next((-1) * _mapWidth + _ballRadius, _mapWidth - _ballRadius)
                , rand.Next((-1) * _mapHeight + _ballRadius, _mapHeight - _ballRadius));
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
                if (max < ball.Getid())
                {
                    max = ball.Getid();
                }
            }

            return max + 1;
        }

        public void DoTick()
        {
            //TODO: add ball radius to condition
            foreach (Ball ball in GetAllBalls())
            {
                if (ball.XPos + ball.XDirectory + ball.Radius < 0 || ball.XPos + ball.XDirectory + ball.Radius > _mapWidth)
                {
                    ball.XDirectory = ball.XDirectory * (-1);
                }
                if (ball.YPos + ball.YDirectory + ball.Radius < 0 || ball.YPos + ball.YDirectory + ball.Radius > _mapHeight)
                {
                    ball.YDirectory = ball.YDirectory * (-1);
                }
                ball.XPos += ball.XDirectory;
                ball.YPos += ball.YDirectory;
            }
        }

        public bool IsBallWithID(int ID)
        {
            foreach (Ball obj in _objectStorage.GetAllBalls())
            {
                if (ID == obj.Getid())
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
                if (ID == obj.Getid())
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
                if (ID == obj.Getid())
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