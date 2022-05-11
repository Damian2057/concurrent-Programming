using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace LogicLayer
{
    public class BallTransform : INotifyPropertyChanged
    {
        private readonly BallApi _ball;
        private double _xDirection;
        private double _yDirection;

        public BallTransform(BallApi ball, double xDirection, double yDirection)
        {
            _ball = ball;
            _xDirection = xDirection;
            _yDirection = yDirection;
        }

        public BallApi Ball
        {
            get => _ball;
        }

        public double GetX
        {
            get => _ball.xPos;
            set
            {
                _ball.xPos = value; 
                RaisePropertyChanged(nameof(GetX));
            }
        }

        public double GetY
        {
            get => _ball.yPos;
            set
            {
                _ball.yPos = value;
                RaisePropertyChanged(nameof(GetY));
            }
        }

        public double GetRadius
        {
            get => _ball.radius;
        }

        public double GetMass
        {
            get => _ball.mass;
        }

        public double GetXDir
        {
            get => _xDirection;
            set => _xDirection = value;
        }

        public double GetYDir
        {
            get => _yDirection;
            set => _yDirection = value;
        }

        public double Direction
        {
            get
            {
                return Math.Sqrt(_xDirection * _xDirection + _yDirection * _yDirection);
            }
        }

        public double GetForce
        {
            get
            {
                return GetMass * Direction * Direction / 2.0;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
