using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AutoMoq;
using Machine.Specifications;
using Moq;
using SkillSignal.BusinessLayer;
using SkillSignal.Domain;
using SkillSignal.Repository;
using SkillSignal.ViewModels;
using It = Machine.Specifications.It;

namespace SkillSignal.Specifications
{
    using System.Threading;

    using Microsoft.Practices.Unity;

    using SkillSignal.DependencyResolution;
    using SkillSignal.IRepository;
    using SkillSignal.ServiceClients;
    using SkillSignal.ViewModels.Project;

    public class When_loading_a_project
    {
        Establish context = () =>
        {
            IUnityContainer unityContainer = new UnityContainer();
            var bootStrapper = new ApplicationBootStrapper(unityContainer);

            //_projectRepository = new Mock<IProjectRepository>();
            //_projectRepository.Setup(x => x.Create(Moq.It.IsAny<Project>())).Returns(new Project());

            //var dalContextMock = new Mock<IDALContext>();
            //dalContextMock.SetupGet(dal => dal.Projects).Returns(_projectRepository.Object);

            //var viewNavigationService = new PageNavigationService(new UserServiceClient(), new ViewModelFactory(new UnityContainer()));
            //ICreateProjectViewModel createProjectViewModel = new CreateProjectViewModel(new ProjectService(dalContextMock.Object),viewNavigationService );
            _applicationViewModel = bootStrapper.Resolve<IApplicationViewModel>();
                // new ApplicationViewModel(createProjectViewModel, viewNavigationService, new UserService(dalContextMock.Object));
        };

        Because of = () =>
            {
                ((AsyncRelayCommand)_applicationViewModel.CreateProjectViewModel.Create).ExecuteAsync(null);
                Thread.Sleep(2000);
            };

        It should_create_a_new_project = () => _applicationViewModel.CurrentProject.ShouldNotBeNull();  //_projectRepository.Verify(x => x.Create(Moq.It.IsAny<Project>()));

        It should_navigate_to_the_start_page =
            () => _applicationViewModel.PageNavigationService.CurrentPage.ShouldBeOfType<StartPageViewModel>();

        private static IApplicationViewModel _applicationViewModel;
        private static Mock<IProjectRepository> _projectRepository;
    }
}
