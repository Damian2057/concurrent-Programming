using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallApi
    {
        public static BallApi CreateBall(double xPos, double yPos, double radius, double mass)
        {
            return new Ball(xPos, yPos, radius, mass);
        }

        public abstract string color { get; set; } 
        public abstract double xPos { get; set; }
        public abstract double yPos { get; set; }
        public abstract double radius { get; set; }
        public abstract double mass { get; set; }


    }
}
