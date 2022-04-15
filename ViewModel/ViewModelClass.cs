using Presentation.ViewModel.MVVMcore;
using Presentation.Model;
using Data;

namespace Presentation.ViewModel
{
    public class ViewModelClass : BaseViewModel
    {
        private string _numberOfBalls;
        public RelayCommand _summon { get; }
        public RelayCommand _clear { get; }
        public RelayCommand _pause { get; }
        public RelayCommand _resume { get; }

        public bool _summonFlag = true;
        public bool _clearFlag = false;
        public bool _resumeFlag = false;
        public bool _pauseFlag = false;

        public MainMap _mainMap{ get; }

        public int _width { get; }
        public int _height { get; }

        public ViewModelClass()
        {
            _width = 1000;
            _height = 706;
            _summon = new RelayCommand(Summon, SummonProperties);
            _clear = new RelayCommand(Clear, ClearProperties);
            _resume = new RelayCommand(Resume, ResumeProperties);
            _pause = new RelayCommand(Pause, PauseProperties);
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

        public bool SummonFlag
        {
            get => _summonFlag;

            set
            {
                _summonFlag = value;
                OnPropertyChanged();
            }
        }

        public bool ClearFlag
        {
            get => _clearFlag;

            set
            {
                _clearFlag = value;
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

        public void Summon()
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
                _summonFlag = false;
                _clearFlag = true;
                _resumeFlag = true;
                _resume.OnCanExecuteChanged();
                _summon.OnCanExecuteChanged();
                _clear.OnCanExecuteChanged();
            }
            catch(Exception)
            {
                _numberOfBalls = "";
                OnPropertyChanged();
            }
        }

        public void Clear()
        {
            _mainMap.ClearMap();
            OnPropertyChanged("GetBalls");
            _summonFlag = true;
            _clearFlag = false;
            _summon.OnCanExecuteChanged();
            _clear.OnCanExecuteChanged();

            _resumeFlag = false;
            _pauseFlag = false;
            _resume.OnCanExecuteChanged();
            _pause.OnCanExecuteChanged();
        }

        public void Resume()
        {
            _mainMap.Tick();
            OnPropertyChanged("GetBalls");
            _pauseFlag = true;
            _resumeFlag = false;
            _resume.OnCanExecuteChanged();
            _pause.OnCanExecuteChanged();
        }

        public void Pause()
        {
            _resumeFlag = true;
            _pauseFlag = false;
            _pause.OnCanExecuteChanged();
            _resume.OnCanExecuteChanged();
        }


        private bool SummonProperties()
        {
            return _summonFlag;
        }

        private bool ClearProperties()
        {
            return _clearFlag;
        }

        private bool ResumeProperties()
        {
            return _resumeFlag;
        }

        private bool PauseProperties()
        {
            return _pauseFlag;
        }
    }
}