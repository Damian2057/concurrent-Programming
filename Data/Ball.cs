namespace Data
{
    public class Ball
    {

        private readonly int _ballID;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public int Radius { get; set; }

        public string color { get; }

        
        public Ball(int ID, int xPos, int yPos,int radius, int xDir, int yDir)
        {
            _ballID = ID;
            XPos = xPos;
            YPos = yPos;
            XDirection = xDir;
            YDirection = yDir;
            Radius = radius;
            color = Color.PickColor();
        }


        public int GetID()
        {
            return _ballID;
        }
    }
}