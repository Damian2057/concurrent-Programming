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

        public void CreateBall(int ID, int x, int y, int xdirectory, int ydirectory) 
        {
            if(IsBallWithID(ID) 
               && (x < 0 || x > _mapWidth 
                         || y < 0 || y > _mapHeight 
                         || xdirectory < -50 || xdirectory > 50 
                         || ydirectory < -50 || ydirectory > 50))
            {
                throw new InvalidDataException("The ball parameters entered are invalid");
            }
            else
            {
                Ball newBall = new Ball(ID, x, y, xdirectory, ydirectory);
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

        public void SummonBalls(int amount)
        {
            //generate balls
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
                if (ball.XPos + ball.XDirectory < 0 || ball.XPos + ball.XDirectory > _mapWidth)
                {
                    ball.XDirectory = ball.XDirectory * (-1);
                }
                if (ball.YPos + ball.YDirectory < 0 || ball.YPos + ball.YDirectory > _mapHeight)
                {
                    ball.YDirectory = ball.YDirectory * (-1);
                }
                ball.XPos += ball.XDirectory;
                ball.YPos += ball.YDirectory;
            }
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