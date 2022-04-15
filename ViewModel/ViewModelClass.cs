using Presentation.ViewModel.MVVMcore;
using Presentation.Model;
using Data;

namespace Presentation.ViewModel
{
    public class ViewModelClass : BaseViewModel
    {
        private string _numberOfBalls;
        public RelayCommand _start { get; }
        public RelayCommand _stop { get; }

        public bool _flag = true;

        public MainMap _mainMap{ get; }

        public int _width { get; }
        public int _height { get; }

        public ViewModelClass()
        {
            _width = 1000;
            _height = 706;
            _start = new RelayCommand(Start, CanButtonBeDisabled);
            _stop = new RelayCommand(Stop, CanButtonBeEnabled);
            _numberOfBalls = "";
            _mainMap = new MainMap(_width, _height);
        }

        public string NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                OnPropertyChanged();
            }
        }

        public bool Button
        {
            get => _flag;

            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }

        public Ball[]? GetBalls { get => _mainMap.GetBalls().ToArray(); }

        public void Start()
        {
            try
            {
                int numberOfBalls = int.Parse(_numberOfBalls);

                if(numberOfBalls < 0)
                {
                    throw new ArgumentException("Number of balls is less than 0");
                }

                _mainMap.CreateBalls(numberOfBalls);
                OnPropertyChanged("GetBalls");
                CheckButton();
            }
            catch(Exception)
            {
                _numberOfBalls = "";
                OnPropertyChanged();
            }
        }

        public void Stop()
        {
            _mainMap.ClearMap();
            OnPropertyChanged("GetBalls");
            CheckButton();
        }

        private void CheckButton()
        {
            Button = !Button;
            _start.OnCanExecuteChanged();
            _stop.OnCanExecuteChanged();
        }

        private bool CanButtonBeEnabled()
        {
            return !Button;
        }

        private bool CanButtonBeDisabled()
        {
            return Button;
        }
    }
}