using SkillSignal.ViewModels;

namespace SkillSignal
{
    using System.Windows.Input;

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
            this.MouseDown += HandleDragMove;
        }

        void HandleDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
            
        }
    }
}
