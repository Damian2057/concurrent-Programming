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

        private ObjectStorage _ObjectStorage = new();

        public BallsManager(int mapWidth, int mapHeight)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
        }

        public void CreateBall()
        {

        }

        public void GenerateRandomBall()
        {

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