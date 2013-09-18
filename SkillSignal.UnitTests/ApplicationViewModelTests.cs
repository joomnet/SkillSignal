using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.UnitTests
{
    using System.Threading;

    using AutoMoq;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using SkillSignal.IBusinessLayer;
    using SkillSignal.ViewModels;
    using SkillSignal.ViewModels.Project;

    public class ApplicationViewModelTests
    {
        IUnityContainer _testContainer;

        AutoMoqer _autoMocker;

        [Test]
        public void When_user_service_is_null_should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new ApplicationViewModel(new Mock<ICreateProjectViewModel>().Object, new Mock<INavigationService>().Object, null));
        }

        [Test]
        public void When_navigation_service_is_null_should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new ApplicationViewModel(new Mock<ICreateProjectViewModel>().Object, null, new Mock<IUserService>().Object));
        }

        [Test]
        public void When_create_project_view_model_is_null_should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new ApplicationViewModel(null, new Mock<INavigationService>().Object, new Mock<IUserService>().Object));
        }

        [Test]
        public void When_a_navigation_button_is_clicked_the_associated_page_should_be_displayed()
        {
            _testContainer = new UnityContainer();
            _autoMocker = new AutoMoq.AutoMoqer(_testContainer);

            var mockPageViewModel = new Mock<IPageViewModel>();
            mockPageViewModel.SetupGet(page => page.Title).Returns("Page1");
            IPageViewModel pageViewModel = mockPageViewModel.Object;

            var navigationServiceMock = new Mock<INavigationService>();
            navigationServiceMock.Setup(vm => vm.GetPagesByNames())
                                 .Returns(
                                     new Dictionary<string, Func<IPageViewModel>>
                                         {
                                             { "page1", () => pageViewModel },
                                             { "page2", () => null },
                                             { "page3", () => null },
                                             { "page4", () => null },
                                         });

            

            INavigationService navigationService = navigationServiceMock.Object;
            _autoMocker.SetInstance(navigationService);
           
            var applicationViewModel = _autoMocker.Resolve<ApplicationViewModel>();

            applicationViewModel.LeftNavigationButtonCollection.Single(btn => btn.DisplayText == "page1").ExecuteAsync(null);

            navigationServiceMock.VerifySet(ns => ns.CurrentPage = pageViewModel);
        }

        [Test]
        public void when_loading_and_no_project_is_loaded_only_default_buttons_should_be_enabled()
        {
            _testContainer = new UnityContainer();
            _autoMocker = new AutoMoqer(_testContainer);

            var navigationServiceMock = new Mock<INavigationService>();
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
            _autoMocker.SetInstance(navigationService);

            var applicationViewModel = _autoMocker.Resolve<ApplicationViewModel>();

            Assert.AreEqual("Create Project", applicationViewModel.LeftNavigationButtonCollection.Single().DisplayText); 
        }
    }
}
