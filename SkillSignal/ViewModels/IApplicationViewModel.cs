namespace SkillSignal.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using global::SkillSignal.Commands;
    using global::SkillSignal.ViewModels.Project;

    public interface IApplicationViewModel
    {
        ICommand Back { get; set; }
        ICommand Forward { get; set; }
        ICommand Close { get; set; }
        ICommand Resize { get; set; }
        object ApplicationDialog { get; set; }
        INavigationService ViewNavigationService { get; set; }
        List<DisplayableNavigationCommand> LeftNavigationButtonCollection { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
        Task DisplayPage(IPageViewModel pageViewModel);
    }
}