using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SkillSignal.Common;
using Microsoft.Practices.Unity;
using SkillSignal.DependencyResolution;
using SkillSignal.Domain;
using SkillSignal.IBusinessLayer;

namespace SkillSignal.ViewModels.Users
{

    public class UserManagementViewModel : PageViewModel
    {
        readonly IUserService _userService;

        ObservableCollection<UserAccountViewModel> _userCollection;

        bool _isSelected;

        public UserManagementViewModel(INavigationService pageNavigationService, IUserService userService) : base(pageNavigationService)
        {
            this._userService = userService;
            Title = "User Mgt";
        }

        public ObservableCollection<UserAccountViewModel> UserCollection
        {
            get { return this._userCollection; }
            set { this.SetProperty(ref this._userCollection, value, () => this.UserCollection); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value)
                {
                  this._Load().Wait();
                }
                this.SetProperty(ref _isSelected, value, () => IsSelected);
            }
        }

        protected override async Task _Load()
        {
            var userAccounts = await TaskEx.Run(() => this._userService.GetAllUsers());
            UserCollection = new ObservableCollection<UserAccountViewModel>(userAccounts.Select(x => new UserAccountViewModel(x.Id, ViewNavigationService, x.IsActive)
                {
                    FirstName = x.FirstName, 
                    LastName = x.LastName,
                    SelectedAccessLevel = x.AccessLevel,
                }));

            UserCollection.Each(
                x => x.PropertyChanged += (sender, args) => TaskEx.Run(async () =>
                    {
                        var userAccountViewModel = sender as UserAccountViewModel;
                        if (userAccountViewModel == null)
                        {
                            return;
                        }
                        var isDeleted = await _CheckIsDeleteted(userAccountViewModel);
                        if (!isDeleted)
                        {
                            await _UpdateIsEdittableStatus(userAccountViewModel);
                            await _Save(userAccountViewModel);  
                        }
                    }));
        }

        async Task<bool> _CheckIsDeleteted(UserAccountViewModel userAccountViewModel)
        {
            if (userAccountViewModel.IsDeleted)
            {
                _userCollection.Remove(userAccountViewModel);
                _userService.Delete(userAccountViewModel.ID);
                return true;
            }
            return false;
        }

        async Task _UpdateIsEdittableStatus(UserAccountViewModel userAccountViewModel)
        {
            await TaskEx.Run(
                () =>
                    {
                        if (userAccountViewModel.IsEdittable)
                        {
                            this._userCollection.Where(u => u.ID != userAccountViewModel.ID)
                                .Each(user => user.IsEdittable = false);
                        }
                    });
        }

        async Task _Save(UserAccountViewModel userAccountViewModel)
        {
            await TaskEx.Run(
                () =>
                    {
                        var userAccount = new UserAccount(
                            userAccountViewModel.ID,
                            userAccountViewModel.FirstName,
                            userAccountViewModel.LastName,
                            userAccountViewModel.SelectedAccessLevel);

                        this._userService.SaveOrUpdate(userAccount);
                    });
        }
    }

    public class DesignTimeUserManagementViewModel : UserManagementViewModel
    {
        static PageNavigationService pageNavigationService = new PageNavigationService(new ViewModelFactory(new UnityContainer()));

        public DesignTimeUserManagementViewModel()
            : base(pageNavigationService, null)
        {
            this.UserCollection = new ObservableCollection<UserAccountViewModel>(new List<UserAccountViewModel>
                {
                    new UserAccountViewModel(Guid.NewGuid().ToString(), pageNavigationService, true)
                        {
                            FirstName = "Segun", LastName = "Meduoye", IsEdittable = true
                        },
                    new UserAccountViewModel(Guid.NewGuid().ToString(), pageNavigationService, true)
                        {
                            FirstName = "Ayo", LastName = "Meduoye"
                        },
                    new UserAccountViewModel(Guid.NewGuid().ToString(), pageNavigationService, false)
                        {
                            FirstName = "Gbemiga", LastName = "Meduoye"
                        },
                });
        }
    }
}