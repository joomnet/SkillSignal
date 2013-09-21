using SkillSignal.ViewModels;

namespace SkillSignal
{
    using Microsoft.Practices.Unity;
    using SkillSignal.BootStrapper;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly IApplicationViewModel _applicationViewModel;
        

        public MainWindow()
        {
            InitializeComponent();
            var bootStrapper = new ApplicationBootStrapper(new UnityContainer()); 
            DataContext = bootStrapper.Resolve<IApplicationViewModel>();
        }
    }
}
