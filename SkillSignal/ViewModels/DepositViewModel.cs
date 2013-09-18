namespace SkillSignal.ViewModels
{
    public class DepositViewModel : PageViewModel
    {
        public DepositViewModel(INavigationService viewNavigationService)
            : base(viewNavigationService)
        {
            this.Title = "Deposits";
        }
    }
}