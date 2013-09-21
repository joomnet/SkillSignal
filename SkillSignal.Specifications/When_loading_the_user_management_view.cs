using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using SkillSignal.ViewModels;
using Moq;
using SkillSignal.BusinessLayer;
using SkillSignal.Domain;
using SkillSignal.Repository;
using It = Machine.Specifications.It;
using System;
using System.Linq.Expressions;
using System.Threading;
using SkillSignal.ViewModels.Users;

namespace SkillSignal.Specifications
{
    using System.Windows.Input;

    using Microsoft.Practices.Unity;

    using SkillSignal.BootStrapper;
    using SkillSignal.DependencyResolution;
    using SkillSignal.IRepository;
    using SkillSignal.ServiceClients;

    public class When_loading_the_user_management_view
    {
        Establish context = () =>
            {
                var bootStrapper = new ApplicationBootStrapper(new UnityContainer());
               
                _userRepositoryMock = new Mock<IUserAccountRepository>();
                _userRepositoryMock.Setup(repo => repo.Filter(Moq.It.IsAny<Expression<Func<UserAccount, bool>>>()))
                                  .Returns(
                                      new EnumerableQuery<UserAccount>(
                                          new List<UserAccount>
                                              {
                                                  new UserAccount("id1", "Segun","Meduoye", AccessLevel.Admin),
                                                  new UserAccount("id2","Wumi","Meduoye", AccessLevel.Admin),
                                                  new UserAccount("id3","Jishua","Meduoye", AccessLevel.Admin)

                                              }));

                //var dalContext = new Mock<IDALContext>();
                //dalContext.SetupGet(d => d.Users).Returns(_userRepositoryMock.Object);
                bootStrapper.With(_userRepositoryMock.Object);
                _userManagementViewModel = bootStrapper.Resolve<UserManagementViewModel>();
                _userManagementViewModel.Load.Execute(null);
                Thread.Sleep(5000);
            };
        protected static UserManagementViewModel _userManagementViewModel;

        protected static Mock<IUserAccountRepository> _userRepositoryMock;
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
                    (_userManagementViewModel.UserCollection.First().Edit as AsyncRelayCommand).ExecuteAsync(null);
                    (_userManagementViewModel.UserCollection.Skip(1).First().Edit as AsyncRelayCommand).ExecuteAsync(null);
                };

        It the_user_should_be_marked_as_edittable =
            () => _userManagementViewModel.UserCollection.First().IsEdittable.ShouldBeFalse();

        It all_other_users_should_become_readonly =
            () => _userManagementViewModel.UserCollection.Count(x => x.FirstName != "Wumi" && x.IsEdittable == false).ShouldEqual(2);

        It should_save_the_user =
            () => _userRepositoryMock.Verify(x => x.Create(Moq.It.Is<UserAccount>(o => o.Id == "id1")));
    }

    public class and_a_user_is_deleted : When_loading_the_user_management_view
    {
        Establish context = () => _userRepositoryMock.Setup(repo => repo.Delete(user => user.Id == "id1"));

        Because of = () =>
            {
                (_userManagementViewModel.UserCollection.First(x => x.ID == "id1").Delete as AsyncRelayCommand).ExecuteAsync(null);
                Thread.Sleep(5000);
            };

        It the_user_should_be_deleted =
            () =>
                {
                    _userManagementViewModel.UserCollection.Select(x => x.ID).ShouldNotContain("id1");
                    _userRepositoryMock.Verify();
                };
    }

    public class and_user_admin_level_is_changed : When_loading_the_user_management_view
    {

    }
}
