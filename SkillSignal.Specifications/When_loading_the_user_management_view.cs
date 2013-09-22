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
                                                  new UserAccount("id1", "Segun","Meduoye", AccessLevel.Admin),
                                                  new UserAccount("id2","Wumi","Meduoye", AccessLevel.Admin),
                                                  new UserAccount("id3","Jishua","Meduoye", AccessLevel.Admin)

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
            () => _userServiceClientMock.Verify(x => x.SaveOrUpdate(Moq.It.Is<UserAccount>(o => o.Id == "id1")));

        static UserAccountViewModel firstUserAccount;

        static UserAccountViewModel secondUserAccount;
    }

    public class and_a_user_is_deleted : When_loading_the_user_management_view
    {
        Establish context = () => _userServiceClientMock.Setup(repo => repo.Delete("id1"));

        Because of = async () =>
            {
               await  _userManagementViewModel.UserCollection.First(x => x.ID == "id1").Delete.ExecuteAsync(null);
            };

        It the_user_should_be_deleted =
            () =>
                {
                    _userManagementViewModel.UserCollection.Select(x => x.ID).ShouldNotContain("id1");
                    _userServiceClientMock.Verify();
                };
    }

    public class and_user_admin_level_is_changed : When_loading_the_user_management_view
    {

    }
}
