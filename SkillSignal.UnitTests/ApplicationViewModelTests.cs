using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMoq;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using SkillSignal.DependencyResolution;
using SkillSignal.Domain;
using SkillSignal.IBusinessLayer;
using SkillSignal.ViewModels;
using SkillSignal.ViewModels.Project;

namespace SkillSignal.UnitTests
{
    [TestFixture]
    public class ApplicationViewModelTests
    {
        IUnityContainer testContainer;
        AutoMoqer autoMocker;
        Mock<ICreateProjectViewModel> createProjectViewModelMock;
        Mock<INavigationService> navigationServiceMock;
        Mock<IViewModelFactory> viewModelFactoryMock;
        Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
            testContainer = new UnityContainer();
            autoMocker = new AutoMoqer(this.testContainer);
            createProjectViewModelMock = new Mock<ICreateProjectViewModel>();
            navigationServiceMock = new Mock<INavigationService>();
            navigationServiceMock.SetupAllProperties();
            userServiceMock  = new Mock<IUserService>();
            viewModelFactoryMock = new Mock<IViewModelFactory>();

            autoMocker.SetInstance(createProjectViewModelMock.Object);
            autoMocker.SetInstance(navigationServiceMock.Object);
            autoMocker.SetInstance(userServiceMock.Object);
            autoMocker.SetInstance(viewModelFactoryMock.Object);
        }

        [Test]
        public void When_user_service_is_null_should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    new ApplicationViewModel(this.navigationServiceMock.Object, null, this.viewModelFactoryMock.Object);
                });
        }

        [Test]
        public void When_navigation_service_is_null_should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new ApplicationViewModel(null, this.userServiceMock.Object, null));
        }

        [Test]
        public void When_a_navigation_button_is_clicked_the_associated_page_should_be_displayed()
        {
            this.testContainer = new UnityContainer();
            this.autoMocker = new AutoMoq.AutoMoqer(this.testContainer);

            var mockPageViewModel = new Mock<IPageViewModel>();
            mockPageViewModel.SetupGet(page => page.Title).Returns("Page1");
            IPageViewModel pageViewModel = mockPageViewModel.Object;

            var viewModelFactoryMock = new Mock<IViewModelFactory>();
            viewModelFactoryMock.Setup(f => f.Get<StartPageViewModel>()).Returns(() => new StartPageViewModel(new Mock<INavigationService>().Object));
            this.autoMocker.SetInstance(viewModelFactoryMock.Object);

            var navigationServiceMock = this.navigationServiceMock;
            navigationServiceMock.Setup(vm => vm.GetDefaultPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "page1", () => pageViewModel },
                                             { "page2", () => null },
                                             { "page3", () => null },
                                             { "page4", () => null },
                                         });

            

            INavigationService navigationService = navigationServiceMock.Object;
            this.autoMocker.SetInstance(navigationService);
           
            var applicationViewModel = this.autoMocker.Resolve<ApplicationViewModel>();

            applicationViewModel.LeftNavigationButtonCollection.Single(btn => btn.DisplayText == "page1").ExecuteAsync(null);

            navigationServiceMock.VerifySet(ns => ns.CurrentPage = pageViewModel);
        }

        [Test]
        public void when_loading_and_no_project_is_loaded_only_default_buttons_should_be_enabled()
        {
           

            var navigationServiceMock = this.navigationServiceMock;
            navigationServiceMock.Setup(vm => vm.GetDefaultPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "Create Project", () => null },
                                         });

            navigationServiceMock.Setup(vm => vm.GetPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "page1", () => null },
                                             { "page2", () => null },
                                             { "page3", () => null },
                                             { "page4", () => null },
                                         });


            INavigationService navigationService = navigationServiceMock.Object;
            this.autoMocker.SetInstance(navigationService);

            var applicationViewModel = this.autoMocker.Resolve<ApplicationViewModel>();

            Assert.AreEqual("Create Project", applicationViewModel.LeftNavigationButtonCollection.Single().DisplayText); 
        }

        [Test]
        public void when_project_is_loaded_non_default_buttons_should_be_loaded()
        {
            var projectServiceClientMock = new Mock<IProjectService>();
            const string expectedProjectName = "Test Project";
            projectServiceClientMock.Setup(vm => vm.Create(expectedProjectName))
                .Returns(() => new Project { Name = expectedProjectName });
            autoMocker.SetInstance(projectServiceClientMock.Object);

            navigationServiceMock.Setup(vm => vm.GetDefaultPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "Create Project", () => autoMocker.Resolve<CreateProjectViewModel>()},
                                         });

            navigationServiceMock.Setup(vm => vm.GetPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "page1", () => null },
                                             { "page2", () => null },
                                             { "page3", () => null },
                                             { "page4", () => null },
                                         });


            var applicationViewModel = autoMocker.Resolve<ApplicationViewModel>();
            applicationViewModel.LeftNavigationButtonCollection.Single(btn => btn.DisplayText == "Create Project").Execute(null);
            Thread.Sleep(3000);
            Assert.IsInstanceOf<CreateProjectViewModel>(applicationViewModel.ViewNavigationService.CurrentPage, "The current page should be of type {0} but it is not.", typeof(CreateProjectViewModel));
            
            var createProjectViewModel = (applicationViewModel.ViewNavigationService.CurrentPage as CreateProjectViewModel);
            
            createProjectViewModel.ProjectName =
                expectedProjectName;
            createProjectViewModel.Create.Execute(null);
            Thread.Sleep(3000);
          
            Assert.IsInstanceOf<ProjectDashBoardViewModel>(applicationViewModel.ViewNavigationService.CurrentPage);
            
            string actualProjectName = (applicationViewModel.ViewNavigationService.CurrentPage as ProjectDashBoardViewModel).Title;
            Thread.Sleep(3000);
            Assert.AreEqual(
                expectedProjectName,
                actualProjectName, "The new project title should be {0} but it is {1}", expectedProjectName,actualProjectName );

            /*manually push the page changed event*/
            navigationServiceMock.Raise(ns => ns.PageChanged += null, new EventArgs());

            navigationServiceMock.Verify(vm => vm.GetPagesByNames());
        }
    }
}
