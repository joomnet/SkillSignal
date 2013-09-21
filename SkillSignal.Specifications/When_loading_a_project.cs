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

    using SkillSignal.BootStrapper;
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
            _applicationViewModel = bootStrapper.Resolve<IApplicationViewModel>();
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
