using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Presentation.ViewModel
{
    public class BallAdapter : INotifyPropertyChanged
    {
        private Vector2 position;
        private float radius;
        private string color;

        public BallAdapter(Vector2 position, float radius, string color)
        {
            this.radius = radius;
            Position = position;
            this.color = color;
        }

        public BallAdapter()
        {
            radius = 0;
            Position = Vector2.Zero;
        }


        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                X = value.X;
                Y = value.Y;
                this.OnPropertyChanged();
            }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
                this.OnPropertyChanged();
            }
        }

        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
                this.OnPropertyChanged();
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChangedEventArgs args = new(caller);
            PropertyChanged?.Invoke(this, args);
        }
    }
}