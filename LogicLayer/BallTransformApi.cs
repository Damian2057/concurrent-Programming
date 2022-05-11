using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace LogicLayer
{
    public abstract class BallTransformApi 
    {
        public static BallTransformApi CreateBallTransform(BallApi ball, double xDirection, double yDirection)
        {
            return new BallTransform(ball, xDirection, yDirection);
        }

        public abstract BallApi Ball { get; }
        public abstract double GetX { get; set; }
        public abstract double GetY { get; set; }
        public abstract double GetRadius { get; }
        public abstract double GetMass { get; }
        public abstract double GetXDir { get; set; }
        public abstract double GetYDir { get; set; }
        public abstract double Direction { get; }
        public abstract double GetForce { get; }
    }
}
