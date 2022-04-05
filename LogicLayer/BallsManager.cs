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

        private ObjectStorage<Ball> _ObjectStorage = new();

        public BallsManager(int mapWidth, int mapHeight)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
        }

        public int GetMapWidth()
        {
            return _mapWidth;
        }

        public int GetMapHeight()
        {
            return _mapHeight;
        }

        public void CreateBall(int ID, int x, int y, int xspeed, int yspeed) 
        {
            if(IsBallWithID(ID) 
               && (x < 0 || x > _mapWidth 
                         || y < 0 || y > _mapHeight 
                         || xspeed < -50 || xspeed > 50 
                         || yspeed < -50 || yspeed > 50))
            {
                throw new InvalidDataException("The ball parameters entered are invalid");
            }
            else
            {
                Ball newBall = new Ball(ID, x, y, xspeed, yspeed);
                _ObjectStorage.AddBall(newBall);
            }
        }

        public void GenerateRandomBall()
        {
            Random rand = new Random();
            CreateBall(AutoID()
                , rand.Next(0,_mapWidth),rand.Next(0,_mapHeight)
                , rand.Next(-50,50), rand.Next(-50, 50));
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
            //TODO: Calls to this function will cause the balls to move (change their position),
            //a condition to check if the ball has not reached the border (with some radius?)
        }

        public bool IsBallWithID(int ID)
        {
            foreach (Ball obj in _ObjectStorage.GetAllBalls())
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
            foreach (Ball obj in _ObjectStorage.GetAllBalls())
            {
                if (ID == obj.Getid())
                {
                    return _ObjectStorage.GetAllBalls().ElementAt(ID);
                }
            }

            throw new InvalidDataException("The ball with the given ID is not in the list");
        }

        public void RemoveBallByID(int ID)
        {
            foreach (Ball obj in _ObjectStorage.GetAllBalls())
            {
                if (ID == obj.Getid())
                { 
                    _ObjectStorage.RemoveBall(obj);
                    return;
                }
            }

            throw new InvalidDataException("The ball with the given ID is not in the list");
        }

        public List<Ball> GetAllBalls()
        {
            return _ObjectStorage.GetAllBalls();
        }

        public void ClearMap()
        {
            _ObjectStorage.ClearStorage();
        }
    }
}