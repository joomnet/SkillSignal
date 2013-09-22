using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SkillSignal
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _action;
        private readonly Func<bool> _canExecute;

        public AsyncRelayCommand(Func<Task> action, Func<bool> canExecute )
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            this.ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
           await  _action();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> action)
        {
            _action = action;
            _canExecute = o => true;
        }

        public RelayCommand(Action<object> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
