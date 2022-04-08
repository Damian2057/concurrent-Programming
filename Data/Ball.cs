namespace Data
{
    public class Ball
    {

        private readonly int _ballID;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int XDirectory { get; set; }
        public int YDirectory { get; set; }

        public Ball(int ID, int xPos, int yPos, int xDirectory, int yDirectory)
        {
            _ballID = ID;
            XPos = xPos;
            YPos = yPos;
            XDirectory = xDirectory;
            YDirectory = yDirectory;
        }

        public int Getid()
        {
            return _ballID;
        }
    }
}