namespace Data
{
    public class Ball
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }

        public Ball(int xPos, int yPos, int xSpeed, int ySpeed)
        {
            XPos = xPos;
            YPos = yPos;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }
    }
}