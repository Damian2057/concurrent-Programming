using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
   sealed public class MainModel : INotifyPropertyChanged
   {
      private readonly MainBoard ModelLayer;

      public MainModel()
      {
         Circles = new ObservableCollection<BallAdapter>();

         ModelLayer = new MainBoard();
         BallsCount = 5;

         IncreaseButton = new RelayCommand(() =>
         {
            BallsCount += 1;
         });
         DecreaseButton = new RelayCommand(() =>
         {
            BallsCount -= 1;
         });

         StartSimulationButton = new RelayCommand(() =>
         {
            ModelLayer.SetBallsNumber(BallsCount);

            for (int i = 0; i < BallsCount; i++)
            {
               Circles.Add(new BallAdapter());
            }

            ModelLayer.BallPositionChange += (sender, args) =>
            {
                if (Circles.Count <= 0)
                {
                    return;
                }

               for (int i = 0; i < BallsCount; i++)
               {
                  Circles[args.Ball.ID].Position = args.Ball.Position;
                  Circles[args.Ball.ID].Radius = args.Ball.Radius;
                  Circles[args.Ball.ID].Color = args.Ball.Color;
                }
            };
            ModelLayer.StartSimulation();
            this.ToggleSimulationButtons();
         });

         StopSimulationButton = new RelayCommand(() =>
         {
            ModelLayer.StopSimulation();
            Circles.Clear();
            ModelLayer.SetBallsNumber(BallsCount);
            this.ToggleSimulationButtons();
         });
         StopSimulationButton.IsEnabled = false;
      }

      public ObservableCollection<BallAdapter> Circles { get; set; }

      public int BallsCount
      {
         get { return ModelLayer.GetCountOfBalls(); }
         private set
         {
            if (value >= 0)
            {
               ModelLayer.SetBallsNumber(value);
               this.OnPropertyChanged();
            }
         }
      }

      private void ToggleSimulationButtons()
      {
          IncreaseButton.IsEnabled = !IncreaseButton.IsEnabled;
          DecreaseButton.IsEnabled = !DecreaseButton.IsEnabled;
          StartSimulationButton!.IsEnabled = !StartSimulationButton!.IsEnabled;
          StopSimulationButton!.IsEnabled = !StopSimulationButton!.IsEnabled;
      }

      public ISimpleCommand IncreaseButton { get; }
      public ISimpleCommand DecreaseButton { get; }
      public ISimpleCommand StartSimulationButton { get; }
      public ISimpleCommand StopSimulationButton { get; }

      // Event for View update
      public event PropertyChangedEventHandler? PropertyChanged;

      private void OnPropertyChanged([CallerMemberName] string caller = "")
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
      }
   }

    public interface ISimpleCommand : ICommand
    {
        public bool IsEnabled { get; set; }
    }

    public class RelayCommand : ISimpleCommand
    {
        private readonly Action handler;
        private bool isEnabled;

        public RelayCommand(Action handler)
        {
            this.handler = handler;
            IsEnabled = true;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return isEnabled;
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object parameter)
        {
            handler();
        }
    }
}