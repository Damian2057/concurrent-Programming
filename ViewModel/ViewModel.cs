using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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
            _resume = new RelayCommand(Resume, ResumeProperties);
            SummonFlag = true;
            ClearFlag = false;
            ResumeFlag = false;
        }

        public RelayCommand _summon { get; }
        public RelayCommand _clear { get; }
        public RelayCommand _resume { get; }

        public bool _summonFlag = true;
        public bool _clearFlag = false;
        public bool _resumeFlag = false;
        public bool _pauseFlag = false;

        public ModelApi _mainMap { get; set; }

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

        public bool ResumeFlag
        {
            get => _resumeFlag;

            set
            {
                _resumeFlag = value;
                _resume.OnCanExecuteChanged();
            }
        }

        public ObservableCollection<CircleApi> GetBalls
        {
            get => _mainMap.Circles;
            set => _mainMap.Circles = value;
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

                _mainMap.CreateCircle(706, 1000, numberOfBalls, 25);
                SummonFlag = false;
                ClearFlag = true;
                ResumeFlag = true;
            }
            catch (Exception)
            {
                NumberOfBalls = "";
            }
        }

        public void Clear()
        {
            NumberOfBalls = "";
            SummonFlag = true;
            ClearFlag = false;
            ResumeFlag = false;
            _mainMap.StopAnimation();
        }

        public void Resume()
        {

            ResumeFlag = false;
            _mainMap.StartAnimation();
        }

        private bool SummonProperties()
        {
            return SummonFlag;
        }

        private bool ClearProperties()
        {
            return ClearFlag;
        }

        private bool ResumeProperties()
        {
            return ResumeFlag;
        }
    }
}