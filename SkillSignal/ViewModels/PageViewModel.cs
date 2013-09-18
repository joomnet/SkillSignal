namespace SkillSignal.ViewModels
{
    using global::SkillSignal.Common;

    public class PageViewModel : ViewModel, IPageViewModel
    {
        private string _title;

        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value, () => this.Title); }
        }

        protected PageViewModel(INavigationService viewNavigationService)
        {
            ObjectValidator.IfNullThrowException(viewNavigationService, "viewNavigationService");
            this.ViewNavigationService = viewNavigationService;
        }
    }
}