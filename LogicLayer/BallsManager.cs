using System.Collections;
using Data;

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

        public ArrayList GetAllBalls()
        {
            return _ObjectStorage.GetAllBalls();
        }

        public void ClearMap()
        {
            _ObjectStorage.ClearStorage();
        }

    }
}