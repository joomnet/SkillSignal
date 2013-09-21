namespace SkillSignal.BootStrapper
{
    using Microsoft.Practices.Unity;

    using SkillSignal.DependencyResolution;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;
    using SkillSignal.Repository;
    using SkillSignal.ServiceClients;
    using SkillSignal.ViewModels;
    using SkillSignal.ViewModels.Project;
    using SkillSignal.ViewModels.Users;

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
            return this.unityContainer.Resolve<T>();
        }

        public ApplicationBootStrapper With<TDependency>(TDependency tDependency)
        {
           unityContainer.RegisterInstance(tDependency);
            return this;
        }
    }
}