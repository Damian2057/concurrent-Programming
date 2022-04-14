using Presentation.ViewModel.MVVMcore;
using Presentation.Model;

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


    }
}