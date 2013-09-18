namespace SkillSignal.ViewModels
{
    using System.Collections.Generic;

    public class ProjectViewModel : PageViewModel
    {
        private readonly Stack<IPageViewModel> _pages = new Stack<IPageViewModel>();
        private IPageViewModel _currentPage;

        public ProjectViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "MyProject";
        }

        public IPageViewModel CurrentPage
        {
            get { return this._currentPage; }
            set { this.SetProperty(ref this._currentPage, value, () => this.CurrentPage); }
        }

        public static ProjectViewModel NullProjectViewModel { get; set; }
    }
}