using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball: BallApi
    {
        private double _xPos;
        private double _yPos;
        private double _radius;
        private double _mass;
        private string _color;

        public Ball(double xPos, double yPos, double radius, double mass)
        {
            _xPos = xPos;
            _yPos = yPos;
            _radius = radius;
            _mass = mass;

            var color = ColorApi.CreateColor();
            _color = color.PickColor();
        }

        public override double xPos { get; set; }
        public override double yPos { get; set; }
        public override double radius { get; set; }
        public override double mass { get; set; }
        public override string color { get; set; }
    }
}
