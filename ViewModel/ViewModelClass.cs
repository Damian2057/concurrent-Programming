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

        public ViewModelClass()
        {
            _start = new RelayCommand(); //to do
            _stop = new RelayCommand(); //to do
            _numberOfBalls = "";
            _mainMap = new MainMap();
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

        public List<Ball> GetBalls { get => _mainMap.GetBalls(); }

        public void Start()
        {
            try
            {
                int numberOfBalls = int.Parse(_numberOfBalls);

                if(numberOfBalls < 0)
                {
                    throw new ArgumentException("to do error");
                }

                _mainMap.CreateBalls(numberOfBalls);
                OnPropertyChanged("Balls");
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
            OnPropertyChanged("Balls");
            CheckButton();
        }

        private void CheckButton()
        {
            Button = !Button;
            //_start
            //_stop
        }
    }
}