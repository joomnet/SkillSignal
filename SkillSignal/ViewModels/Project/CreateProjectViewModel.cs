namespace SkillSignal.ViewModels.Project
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using SkillSignal.IBusinessLayer;

    public class CreateProjectViewModel : PageViewModel, ICreateProjectViewModel
    {
        private readonly IProjectService projectServiceClient;

        public CreateProjectViewModel(IProjectService projectServiceClient, INavigationService viewNavigationService)
            : base(viewNavigationService)
        {
            this.projectServiceClient = projectServiceClient;
            this.Create = new AsyncRelayCommand(async () =>
                {
                    await this._CreateProject();
                    await this.ViewNavigationService.DisplayPage(new StartPageViewModel(viewNavigationService));
                },() => true);
        }

        

        async Task _CreateProject()
        {
            await Task.Factory.StartNew(() => this.projectServiceClient.Create(this.ProjectName));
        }

        public ICommand Create { get; set; }

        public string ProjectName { get; set; }
    }
}