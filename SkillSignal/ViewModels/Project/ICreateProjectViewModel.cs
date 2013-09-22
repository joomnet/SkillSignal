namespace SkillSignal.ViewModels.Project
{
    using System;
    using System.Windows.Input;

    public interface ICreateProjectViewModel : IPageViewModel
    {
        ICommand Create { get; set; }
        string ProjectName { get; set; }
    }
}