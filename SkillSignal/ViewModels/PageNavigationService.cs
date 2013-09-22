using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using SkillSignal.DependencyResolution;
using SkillSignal.ViewModels.Project;
using SkillSignal.ViewModels.Users;

namespace SkillSignal.ViewModels
{

    public class PageNavigationService : ViewModel, INavigationService
    {
        readonly IViewModelFactory viewModelFactory;

        private IPageViewModel _currentPage;

        public PageNavigationService(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public IPageViewModel CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value, () => CurrentPage, RaisePageChangedEvent);
            }
        }

        void RaisePageChangedEvent()
        {
            var handler = PageChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
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
                            var userManagementViewModel = viewModelFactory.Get<UserManagementViewModel>(); 
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
                    { "New", () => viewModelFactory.Get<CreateProjectViewModel>()},
                    { "Open", () => new PurchaseLedgerViewModel(this) }, 
                };
        }

        public event EventHandler PageChanged;

        public Task DisplayPage(IPageViewModel pageViewModel)
        {
            return TaskEx.Run(() => CurrentPage = pageViewModel);
        }
    }
}