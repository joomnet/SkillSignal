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
        ProjectViewModel CurrentProject { get; set; }
        ICommand Back { get; set; }
        ICommand Forward { get; set; }
        ICommand Close { get; set; }
        ICommand Resize { get; set; }
        object ApplicationDialog { get; set; }
        ICreateProjectViewModel CreateProjectViewModel { get; set; }
        INavigationService PageNavigationService { get; set; }
        List<DisplayableNavigationCommand> LeftNavigationButtonCollection { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        Task DisplayPage(IPageViewModel pageViewModel);
    }
}