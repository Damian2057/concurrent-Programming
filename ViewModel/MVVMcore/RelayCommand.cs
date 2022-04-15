using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel.MVVMcore
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action _canBeExecuteAction;
        private readonly Func<bool>? _canBeFuctionExecute;


        internal void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            if (_canBeFuctionExecute == null)
            {
                return true;
            }
            else
            {
                return _canBeFuctionExecute();
            }
        }

        public void Execute(object? parameter)
        {
            _canBeExecuteAction();
        }

        public RelayCommand(Action action, Func<bool>? function = null)
        {
            _canBeExecuteAction = action ?? throw new ArgumentNullException(nameof(action));
            _canBeFuctionExecute = function;
        }
    }
}