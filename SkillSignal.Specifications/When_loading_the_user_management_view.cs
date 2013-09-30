using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using SkillSignal.Domain;
using It = Machine.Specifications.It;
using System.Threading;
using SkillSignal.ViewModels.Users;
using Microsoft.Practices.Unity;
using SkillSignal.BootStrapper;
using SkillSignal.IBusinessLayer;

namespace SkillSignal.Specifications
{

    public class When_loading_the_user_management_view
    {
        Establish context = () =>
            {
                var bootStrapper = new ApplicationBootStrapper(new UnityContainer());
               
                _userServiceClientMock = new Mock<IUserService>();
                _userServiceClientMock.Setup(repo => repo.GetAllUsers())
                                  .Returns(
                                      new EnumerableQuery<UserAccount>(
                                          new List<UserAccount>
                                              {
                                                  new UserAccount(1, "Segun","Meduoye", AccessLevel.Admin),
                                                  new UserAccount(2,"Wumi","Meduoye", AccessLevel.Admin),
                                                  new UserAccount(3,"Jishua","Meduoye", AccessLevel.Admin)

                                              }));

                bootStrapper.With(_userServiceClientMock.Object);
                _userManagementViewModel = bootStrapper.Resolve<UserManagementViewModel>();
                _userManagementViewModel.Load.Execute(null);
                Thread.Sleep(5000);
            };
        protected static UserManagementViewModel _userManagementViewModel;

        protected static Mock<IUserService> _userServiceClientMock;
    }

    public class and_the_view_is_selected : When_loading_the_user_management_view
    {
        It should_load_all_users_into_the = () => _userManagementViewModel.UserCollection.Count().ShouldEqual(3);

        It should_set_all_user_admin_levels_correctly =
            () => _userManagementViewModel.UserCollection.First().SelectedAccessLevel.ShouldEqual(AccessLevel.Admin);
    }

    public class and_a_user_is_editted : When_loading_the_user_management_view
    {
        Because of =
            () =>
                {
                    firstUserAccount = _userManagementViewModel.UserCollection.First();
                    firstUserAccount.Edit.ExecuteAsync(null).Wait();
                    secondUserAccount = _userManagementViewModel.UserCollection.Skip(1).First();
                    secondUserAccount.Edit.ExecuteAsync(null).Wait(2000);
                };

        It the_user_should_be_marked_as_edittable = () =>
        {
            firstUserAccount.IsEdittable.ShouldBeFalse();
            secondUserAccount.IsEdittable.ShouldBeTrue();
        };

        It all_other_users_should_become_readonly =
            () => _userManagementViewModel.UserCollection.Count(x => x.IsEdittable).ShouldEqual(1);

        It should_save_the_user =
            () => _userServiceClientMock.Verify(x => x.SaveOrUpdate(Moq.It.Is<UserAccount>(o => o.Id == 1)));

        static UserAccountViewModel firstUserAccount;

        static UserAccountViewModel secondUserAccount;
    }

    public class CacheKey 
    {
        protected bool Equals(CacheKey other)
        {
            return string.Equals(this.id, other.id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((CacheKey)obj);
        }

        public override int GetHashCode()
        {
            return (this.id != null ? this.id.GetHashCode() : 0);
        }

        public static bool operator ==(CacheKey left, CacheKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CacheKey left, CacheKey right)
        {
            return !Equals(left, right);
        }

        string id;

        public CacheKey(string id)
        {
            this.id = id;
        }
    }

    public class and_a_user_is_deleted : When_loading_the_user_management_view
    {
        Establish context = () => _userServiceClientMock.Setup(repo => repo.Delete(1));

        Because of = async () =>
            {
               await  _userManagementViewModel.UserCollection.First(x => x.ID == 1).Delete.ExecuteAsync(null);
            };

        It the_user_should_be_deleted =
            () =>
                {
                    _userManagementViewModel.UserCollection.Select(x => x.ID).ShouldNotContain(1);
                    _userServiceClientMock.Verify();
                };
    }

    public class and_user_admin_level_is_changed : When_loading_the_user_management_view
    {

    }
}
