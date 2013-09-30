using System.Threading.Tasks;
using System.Windows.Input;
using SkillSignal.DependencyResolution;
using SkillSignal.IBusinessLayer;

namespace SkillSignal.ViewModels.Project
{
    using Microsoft.Practices.Unity;

    using SkillSignal.Domain;

    public class DesignTimeCreateProjectViewModel : CreateProjectViewModel
    {
        static PageNavigationService PageNavigationService = new PageNavigationService(new ViewModelFactory(new UnityContainer()));

        static IProjectService projectServiceClient = new FakeProjectServiceClient();
        public DesignTimeCreateProjectViewModel()
            : base(projectServiceClient, PageNavigationService)
        {
        }

        
    }

    internal class FakeProjectServiceClient : IProjectService
    {
        public Project Create(string projectName)
        {
            throw new System.NotImplementedException();
        }
    }

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
                },() => true);
        }

        

        async Task _CreateProject()
        {
            await TaskEx.Run(
                () =>
                {
                    var newProject = this.projectServiceClient.Create(this.ProjectName);
                    this.ViewNavigationService.CurrentPage = new ProjectDashBoardViewModel(ViewNavigationService, newProject);
                });
        }

        public ICommand Create { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }
    }
}