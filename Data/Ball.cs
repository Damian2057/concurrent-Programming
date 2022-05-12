using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball: BallApi, INotifyPropertyChanged
    {
        private double _xPos;
        private double _yPos;
        private double _radius;
        private double _mass;
        private double _xDirectory;
        private double _yDirectory;

        private bool _isMoving = false;
        private string _color;

        public Ball(double xPos, double yPos, double radius, double mass)
        {
            _xPos = xPos;
            _yPos = yPos;
            _radius = radius;
            _mass = mass;

            var color = ColorApi.CreateColor();
            _color = color.PickColor();

            Random r = new Random();

            do
            {
                if (r.Next(2) == 0)
                {
                    _xDirectory = r.NextDouble() * -1;
                }
                else
                {
                    _xDirectory = r.NextDouble() * 1;
                }

            }
            while (Math.Abs(_xDirectory) < 0.2);


            if (r.Next(2) == 0)
                _yDirectory = Math.Sqrt(1 - (_xDirectory * _xDirectory));
            else
                _yDirectory = Math.Sqrt(1 - (_xDirectory * _xDirectory)) * -1;
        }

        public override double xPos {
            get => _xPos;
            set
            {
                _xPos = value;
                RaisePropertyChanged("xPos");
            }
        }
        public override double yPos
        {
            get => _yPos;
            set
            {
                _yPos = value;
                RaisePropertyChanged("yPos");
            }
        }
        public override double radius {
            get => _radius;
            set
            {
                _radius = value;
            }
        }
        public override double mass {
            get => _mass;
            set
            {
                _mass = value;
            }
        }
        public override string color {
            get => _color;
        }

        public override double xDirectory
        {
            get => _xDirectory;
            set
            {
                _xDirectory = value;
            }
        }

        public override double yDirection
        {
            get => _yDirectory;
            set
            {
                _yDirectory = value;
            }
        }

        public override bool IsMoving
        {
            get => _isMoving;
            set
            {
                _isMoving = false;
            }
        }

        public override void MoveBall()
        {
            xPos += xDirectory;
            yPos += yDirection;
            IsMoving = true;
            RaisePropertyChanged("Coords");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
