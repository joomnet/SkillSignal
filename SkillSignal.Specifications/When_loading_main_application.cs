using System.Linq;
using Machine.Specifications;

using SkillSignal.Domain;
using SkillSignal.Repository;
using SkillSignal.ViewModels;
using SkillSignal.ViewModels.Project;
using SkillSignal.ViewModels.Users;

namespace SkillSignal.Specifications
{
    using System.Threading;

    using Microsoft.Practices.Unity;

    using SkillSignal.BusinessLayer;
    using SkillSignal.DependencyResolution;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;
    using SkillSignal.ServiceClients;

    public class When_loading_main_application
    {
        Establish context = () =>
            {
                IUnityContainer unityContainer = new UnityContainer();
                var bootStrapper = new ApplicationBootStrapper(unityContainer);
                _viewNavigationService = bootStrapper.Resolve<INavigationService>();
                _applicationViewModel = bootStrapper.Resolve<IApplicationViewModel>();
            };

        protected static IApplicationViewModel _applicationViewModel;

        protected static INavigationService _viewNavigationService;
    }

    public class ApplicationBootStrapper
    {
        readonly IUnityContainer unityContainer;

        public ApplicationBootStrapper(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            unityContainer.RegisterType<UserManagementViewModel, UserManagementViewModel>()
                              .RegisterType<IUserService, UserServiceClient>()
                              .RegisterType<INavigationService, PageNavigationService>()
                              .RegisterType<IViewModelFactory, ViewModelFactory>()
                              .RegisterType<IProjectService, ProjectServiceClient>()
                              .RegisterType<IDALContext, DALContext>()
                              .RegisterType<IUserAccountRepository, UserAccountRepository>()
                              .RegisterType<IProjectRepository, ProjectRepository>()
                              .RegisterType<IApplicationViewModel, ApplicationViewModel>()
                              .RegisterType<ICreateProjectViewModel, CreateProjectViewModel>()
                              .RegisterType<VATViewModel, VATViewModel>()
                              .RegisterType<DepositViewModel, DepositViewModel>()
                              .RegisterType<PurchaseLedgerViewModel, PurchaseLedgerViewModel>()
                              .RegisterInstance(unityContainer);
        }

        public T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }

    public class and_the_user_management_button_is_clicked : When_loading_main_application
    {
        Because of = () => _applicationViewModel.LeftNavigationButtonCollection.Single(x => x.DisplayText == "User Mgt")
                                                .ExecuteAsync(null);

        It should_navigate_to_the_user_management_page = () =>
            _applicationViewModel.PageNavigationService.CurrentPage.ShouldBeOfType<UserManagementViewModel>();
    }

    public class and_no_project_is_selected : When_loading_main_application
    {
        It should_only_show_the_default_buttons =
            () =>
                {
                    _applicationViewModel.LeftNavigationButtonCollection.Count().ShouldEqual(2);
                    _applicationViewModel.LeftNavigationButtonCollection.Select(btn => btn.DisplayText).ShouldContainOnly("New", "Open");
                };
    }
}
