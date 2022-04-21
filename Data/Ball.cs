namespace Data
{
    public abstract class BallApi
    {
        public static BallApi CreateBall(int ID, int xPos, int yPos, int radius, int xDir, int yDir)
        {
            return new Ball(ID, xPos, yPos, radius, xDir, yDir);
        }

        public int BallID { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public int Radius { get; set; }
        public string? color { get; set; }

        private class Ball : BallApi
        {
            public Ball(int ID, int xPos, int yPos, int radius, int xDir, int yDir)
            {
                BallID = ID;
                XPos = xPos;
                YPos = yPos;
                XDirection = xDir;
                YDirection = yDir;
                Radius = radius;
                color = ID <= 200 ? ColorApi.CreateColor().PickColor() : ColorApi.CreateColor().PickRandomColor();
            }
        }
    }
}