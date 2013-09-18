namespace SkillSignal.ViewModels
{
    using Microsoft.Practices.Unity;

    using SkillSignal.BusinessLayer;
    using SkillSignal.DependencyResolution;
    using SkillSignal.ServiceClients;

    public class AnswerViewModel : PageViewModel
    {
        private string _text;
        private bool _isSelected;
        private bool _isCorrect;


        public AnswerViewModel(INavigationService viewNavigationService, string text, bool isSelected, bool isCorrect) : base(viewNavigationService)
        {
            this._text = text;
            this._isSelected = isSelected;
            this._isCorrect = isCorrect;
        }

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value, () => this.Text); }
        }

        public bool IsSelected
        {
            get { return this._isSelected; }
            set { this.SetProperty(ref this._isSelected, value, () => this.IsSelected); }
        }

        public bool IsCorrect
        {
            get { return this._isCorrect; }
            set { this.SetProperty(ref this._isCorrect, value, () => this.IsCorrect); }
        }
    }

    public class DesignTimeAnswerViewModel : AnswerViewModel
    {

        public DesignTimeAnswerViewModel()
            : base(new PageNavigationService(new UserServiceClient(), new ViewModelFactory(new UnityContainer())), "The Capital city of the United Kingdom", false, false)
        {
            this.Text = "The Capital city of the United Kingdom";
            this.IsSelected = false;
            this.IsCorrect = false;
        }

        
    }
}