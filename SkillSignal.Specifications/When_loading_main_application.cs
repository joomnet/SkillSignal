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

    using SkillSignal.BootStrapper;
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
