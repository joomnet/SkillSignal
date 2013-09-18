namespace SkillSignal.ViewModels
{
    public class VATViewModel : PageViewModel
    {
        public VATViewModel(INavigationService viewNavigationService)
            : base(viewNavigationService)
        {
            this.Title = "VAT";
        }
    }
}