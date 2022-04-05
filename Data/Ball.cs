namespace Data
{
    public class Ball
    {

        private readonly int _ballID;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }

        public Ball(int ID, int xPos, int yPos, int xSpeed, int ySpeed)
        {
            _ballID = ID;
            XPos = xPos;
            YPos = yPos;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }

        public int Getid()
        {
            return _ballID;
        }
    }
}