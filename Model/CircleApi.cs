using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.Model
{
    public abstract class CircleApi
    {
        public static CircleApi CreateCircle(BallTransformationApi ball)
        {
            return new Circle(ball);
        }

        public abstract double XCircle { get; set; }
        public abstract double YCircle { get; set; }
        public abstract double Radius { get; set; }
        public abstract double RadiusTransformer { get; }
        public abstract double BallDiameter { get;}
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public class Circle : CircleApi, INotifyPropertyChanged
        {
            private double _xCircle;
            private double _yCircle;
            private double _radius;

            public Circle(BallTransformationApi ball)
            {
                ball.PropertyChanged += BallPropertyChanged;
                XCircle = ball.X;
                YCircle = ball.Y;
                Radius = ball.Radius;
            }

            public override double XCircle
            {
                get => _xCircle;
                set
                {
                    _xCircle = value;
                    RaisePropertyChanged("XCircle");
                }
            }
            public override double YCircle
            {
                get => _yCircle;
                set
                {
                    _yCircle = value;
                    RaisePropertyChanged("YCircle");
                }
            }

            public override double Radius
            {
                get => _radius;
                set
                {
                    _radius = value;
                    RaisePropertyChanged("Radius");
                }

            }

            public override double RadiusTransformer { get => -1 * Radius; }
            public override double BallDiameter { get => 2 * Radius; }

            public override event PropertyChangedEventHandler PropertyChanged;

            public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                BallTransformationApi b = (BallTransformationApi)sender;

                XCircle = b.X;
                YCircle = b.Y;
            }
        }
    }
}