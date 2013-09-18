namespace SkillSignal.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using global::SkillSignal.ViewModels;

    public class DisplayableNavigationCommand : ICommand
    {
        private readonly Func<Task<IPageViewModel>> _action;
        private readonly Func<bool> _canExecute;
        public string DisplayText { get; set; }

        public DisplayableNavigationCommand(string displayText, Func<Task<IPageViewModel>> action, Func<bool> canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
            this.DisplayText = displayText;
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute();
        }

        public void Execute(object parameter)
        {
            this._action();
        }

        public async void ExecuteAsync(object parameter)
        {
            await this._action();
        }

        public event EventHandler CanExecuteChanged;
    }
}