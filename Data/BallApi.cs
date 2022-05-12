using System;
using System.ComponentModel;

namespace Data
{
    public abstract class BallApi
    {
        public static BallApi CreateBall(double xPos, double yPos, double radius, double mass)
        {
            return new Ball(xPos, yPos, radius, mass);
        }

        public abstract void MoveBall();
        public abstract bool IsMoving { get; set; }
        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double Radius { get; set; } 
        public abstract double Mass { get; set; }
        public abstract double XDirectory { get; set; }
        public abstract double YDirectory { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract string Color { get; }


        internal class Ball : BallApi, INotifyPropertyChanged
        {

            private double _x;
            private double _y;
            private double _radius;
            private double _mass;
            private double _xDirectory;
            private double _yDirectory;
            private bool _isMoving = false;
            private string _colorHex;

            public Ball(double x, double y, double radius, double mass)
            {
                _x = x;
                _y = y;
                _radius = radius;
                _mass = mass;
                var color = ColorApi.CreateColor();
                _colorHex = color.PickColor();

                Random rnd = new Random();
                double xrand = 0, yrand = 0;
                int minXFlag = 0, minYFlag = 0;
                while (xrand == 0 || yrand == 0)
                {
                    xrand = rnd.Next(1, 4);
                    yrand = rnd.Next(1, 4);
                    minXFlag = rnd.Next(0, 2);
                    minYFlag = rnd.Next(0, 2);
                }

                xrand = Math.Sqrt(xrand);
                yrand = Math.Sqrt(yrand);

                if (minXFlag == 0)
                {
                    xrand *= -1;
                }
                if (minYFlag == 0)
                {
                    yrand *= -1;
                }

                _xDirectory = xrand;
                _yDirectory = yrand;
            }

            public override void MoveBall()
            {
                X += XDirectory;
                Y += YDirectory;
                IsMoving = true;
                RaisePropertyChanged("Position");
            }


            public override bool IsMoving
            {
                get => _isMoving;
                set { _isMoving = value; }
            }

            public override double X
            {
                get => _x;
                set
                {
                    _x = value;
                    RaisePropertyChanged("X");
                }
            }

            public override double Y
            {
                get => _y;
                set
                {
                    _y = value;
                    RaisePropertyChanged("Y");
                }
            }

            public override double Radius
            {
                get => _radius;
                set
                {
                    _radius = value;
                }
            }

            public override double Mass
            {
                get => _mass;
                set
                {
                    _mass = value;
                }
            }

            public override double XDirectory
            {
                get => _xDirectory;
                set
                {
                    _xDirectory = value;
                }
            }

            public override double YDirectory
            {
                get => _yDirectory;
                set
                {
                    _yDirectory = value;
                }
            }

            public override event PropertyChangedEventHandler PropertyChanged;

            public void RaisePropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public override string Color
            {
                get
                {
                    return _colorHex;
                }
            }
        }
    }
}