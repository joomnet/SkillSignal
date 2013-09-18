using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SkillSignal.Domain;
using SkillSignal.Repository;
using SkillSignal.ViewModels;

namespace SkillSignal
{
    using System.ServiceModel;

    using Microsoft.Practices.Unity;

    using SkillSignal.DependencyResolution;
    using SkillSignal.ServiceClients;
    using SkillSignal.WCFUserService;

    using global::SkillSignal.BusinessLayer;
    using global::SkillSignal.ViewModels.Project;

    using UserServiceClient = SkillSignal.ServiceClients.UserServiceClient;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly IApplicationViewModel _applicationViewModel;
        

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this.Resolve<IApplicationViewModel>();
        }

        private TViewModel Resolve<TViewModel>() where TViewModel : class
        {
            var channelFactory = new ChannelFactory<IUserService>();

            var dalContext = new DALContext();
            return new ApplicationViewModel(
                new CreateProjectViewModel(
                    new ProjectService(
                        dalContext),
                        new PageNavigationService(new UserServiceClient(), new ViewModelFactory(new UnityContainer()))),
                    new PageNavigationService(new UserServiceClient(), new ViewModelFactory(new UnityContainer())), new UserService(dalContext)
                 ) as TViewModel;
        }
    }
}
