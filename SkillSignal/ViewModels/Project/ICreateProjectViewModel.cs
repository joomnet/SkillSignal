namespace SkillSignal.ViewModels.Project
{
    using System.Windows.Input;

    public interface ICreateProjectViewModel 
    {
        ICommand Create { get; set; }
        string ProjectName { get; set; }
    }
}