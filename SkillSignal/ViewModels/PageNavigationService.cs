using System.Threading.Tasks;

namespace SkillSignal.ViewModels
{
    using System;
    using System.Collections.Generic;

    using SkillSignal.DependencyResolution;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.ServiceClients;
    using SkillSignal.ViewModels.Project;
    using SkillSignal.ViewModels.Users;

    public class PageNavigationService : ViewModel, INavigationService
    {
        readonly IUserService userService;

        readonly IViewModelFactory viewModelFactory;

        private IPageViewModel _currentPage;

        public PageNavigationService(IUserService userService, IViewModelFactory viewModelFactory)
        {
            this.userService = userService;
            this.viewModelFactory = viewModelFactory;
        }

        public IPageViewModel CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value, () => CurrentPage); }
        }

        public Dictionary<string, Func<IPageViewModel>> GetPagesByNames()
        {
            return new Dictionary<string, Func<IPageViewModel>>
                {
                    { viewModelFactory.Get<PurchaseLedgerViewModel>().Title, () => viewModelFactory.Get<PurchaseLedgerViewModel>() },
                    { viewModelFactory.Get<VATViewModel>().Title, () => viewModelFactory.Get<VATViewModel>() },
                    { viewModelFactory.Get<DepositViewModel>().Title, () => viewModelFactory.Get<DepositViewModel>() },
                    { viewModelFactory.Get<UserManagementViewModel>().Title, () =>
                        {
                            var userManagementViewModel = viewModelFactory.Get<UserManagementViewModel>();//new UserManagementViewModel(this, userService); 
                            //this.CurrentPage = userManagementViewModel;
                            userManagementViewModel.IsSelected = true;
                            return userManagementViewModel; 
                        }
                    },
                };

        }

        public Dictionary<string, Func<IPageViewModel>> GetDefaultPagesByNames()
        {
            return new Dictionary<string, Func<IPageViewModel>>
                {
                    //{ "New", () => new CreateProjectViewModel(new ProjectServiceClient(), ) },
                    { "Open", () => new PurchaseLedgerViewModel(this) }, 
                };
        }

        public Task DisplayPage(IPageViewModel pageViewModel)
        {
            return TaskEx.Run(() => CurrentPage = pageViewModel);
        }
    }
}