using System.ComponentModel;
using Presentation.Model;
using ViewModel.MVVMBase;

namespace Presentation.ViewModel
{
    public class MainModel : BaseViewModel, INotifyPropertyChanged
    {
        private string _numberOfBalls;
        private readonly MainBoard mainboard;
        public ObservableCollection<BallAdapter> Circles { get; set; }
        public MainModel()
        {
            mainboard = new MainBoard();
            Circles = new ObservableCollection<BallAdapter>();

            _numberOfBalls = "";
            _summon = new RelayCommand(Summon, SummonProperties);
            _clear = new RelayCommand(Clear, ClearProperties);
            SummonFlag = true;
            ClearFlag = false;
        }

        public RelayCommand _summon { get; }
        public RelayCommand _clear { get; }

        public bool _summonFlag = true;
        public bool _clearFlag = false;
        public bool _pauseFlag = false;

        public int _width { get; }
        public int _height { get; }

        public string NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                OnPropertyChanged();
            }
        }

        public bool SummonFlag
        {
            get => _summonFlag;

            set
            {
                _summonFlag = value;
                _summon.OnCanExecuteChanged();
            }
        }

        public bool ClearFlag
        {
            get => _clearFlag;

            set
            {
                _clearFlag = value;
                _clear.OnCanExecuteChanged();
            }
        }

        public void Summon()
        {
            try
            {
                int numberOfBalls = int.Parse(_numberOfBalls);

                if (numberOfBalls < 1)
                {
                    throw new ArgumentException("Number of balls is less than 1");
                }

                mainboard.SetBallsNumber(numberOfBalls);
                for (int i = 0; i < numberOfBalls; i++)
                {
                    Circles.Add(new BallAdapter());
                }

                mainboard.BallPositionChange += (sender, args) =>
                {
                    if (Circles.Count <= 0) return;

                    for (int i = 0; i < numberOfBalls; i++)
                    {
                        Circles[args.Ball.ID].Position = args.Ball.Position;
                        Circles[args.Ball.ID].Radius = args.Ball.Radius;
                    }
                };
                mainboard.StartSimulation();

                SummonFlag = false;
                ClearFlag = true;
            }
            catch (Exception)
            {
                NumberOfBalls = "";
            }
        }

        public void Clear()
        {
            mainboard.StopSimulation();
            Circles.Clear();
            mainboard.SetBallsNumber(0);
            SummonFlag = true;
            ClearFlag = false;
        }

        private bool SummonProperties()
        {
            return SummonFlag;
        }

        private bool ClearProperties()
        {
            return ClearFlag;
        }
    }
}