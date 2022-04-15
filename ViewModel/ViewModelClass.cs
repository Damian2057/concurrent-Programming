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
        public RelayCommand _pause { get; }
        public RelayCommand _resume { get; }

        public bool _summonClearFlag = true;
        public bool _resumeFlag = false;
        public bool _pauseFlag = false;

        public MainMap _mainMap{ get; }

        public int _width { get; }
        public int _height { get; }

        public ViewModelClass()
        {
            _width = 1000;
            _height = 706;
            _start = new RelayCommand(Start, CanSummonClearBeDisabled);
            _stop = new RelayCommand(Stop, CanSummonClearBeEnabled);
            _resume = new RelayCommand(Resume, CanResumeBeDisabled);
            _pause = new RelayCommand(Pause, CanPauseBeDisabled);
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

        public bool SummonClearFlag
        {
            get => _summonClearFlag;

            set
            {
                _summonClearFlag = value;
                OnPropertyChanged();
            }
        }

        public bool ResumeFlag
        {
            get => _resumeFlag;

            set
            {
                _resumeFlag = value;
                OnPropertyChanged();
            }
        }

        public bool PauseFlag
        {
            get => _pauseFlag;

            set
            {
                _pauseFlag = value;
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
                CheckSummonClear();
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
            CheckSummonClear();
            _resumeFlag = false;
            _pauseFlag = false;
            _resume.OnCanExecuteChanged();
            _pause.OnCanExecuteChanged();
        }

        public void Resume()
        {
            _pauseFlag = !_pauseFlag;
            _resumeFlag = !_resumeFlag;
            _resume.OnCanExecuteChanged();
            _pause.OnCanExecuteChanged();
        }

        public void Pause()
        {
            _resumeFlag = !_resumeFlag;
            _pauseFlag = !_pauseFlag;
            _pause.OnCanExecuteChanged();
            _resume.OnCanExecuteChanged();
        }

        private void CheckSummonClear()
        {
            _summonClearFlag = !_summonClearFlag;
            _start.OnCanExecuteChanged();
            _stop.OnCanExecuteChanged();
            _resumeFlag = !_resumeFlag;
            _resume.OnCanExecuteChanged();
        }

        private bool CanSummonClearBeEnabled()
        {
            return !_summonClearFlag;
        }

        private bool CanResumeBeEnabled()
        {
            return !_resumeFlag;
        }

        private bool CanPauseBeEnabled()
        {
            return !_pauseFlag;
        }

        private bool CanSummonClearBeDisabled()
        {
            return _summonClearFlag;
        }

        private bool CanResumeBeDisabled()
        {
            return _resumeFlag;
        }

        private bool CanPauseBeDisabled()
        {
            return _pauseFlag;
        }
    }
}