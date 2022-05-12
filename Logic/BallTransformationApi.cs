using Data;
using System.ComponentModel;

namespace Logic
{
    public abstract class BallTransformationApi
    {
        public static BallTransformationApi CreateBallTransformation(BallApi ball)
        {
            return new BallTransform(ball);
        }

        public abstract BallApi Ball { get; }
        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double Radius { get; set; }
        public abstract double XDirectory { get; set; }
        public abstract double YDirectory { get; set; }
        public abstract double Mass { get; }

        public abstract event PropertyChangedEventHandler PropertyChanged;


        public class BallTransform : BallTransformationApi ,INotifyPropertyChanged
        {

            private readonly BallApi _ball;

            public BallTransform(BallApi ball)
            {
                _ball = ball;
                ball.PropertyChanged += DataBallChanged;
            }

            public override BallApi Ball { get => _ball; }

            public void DataBallChanged(object sender, PropertyChangedEventArgs e)
            {
                RaisePropertyChanged("Coordinates");
            }

            public override double X
            {
                get => _ball.X;
                set
                {
                    _ball.X = value;
                    RaisePropertyChanged(nameof(X));
                }
            }

            public override double Y
            {
                get => _ball.Y;
                set
                {
                    _ball.Y = value;
                    RaisePropertyChanged(nameof(Y));
                }

            }

            public override double Radius
            {
                get => _ball.Radius;
                set
                {
                    _ball.Radius = value;
                }
            }

            public override double XDirectory
            {
                get => _ball.XDirectory;
                set
                {
                    _ball.XDirectory = value;
                }
            }

            public override double YDirectory
            {
                get => _ball.YDirectory;
                set
                {
                    _ball.YDirectory = value;
                }
            }

            public override double Mass
            {
                get
                {
                    return _ball.Mass;
                }
            }

            public override event PropertyChangedEventHandler PropertyChanged;

            public void RaisePropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}