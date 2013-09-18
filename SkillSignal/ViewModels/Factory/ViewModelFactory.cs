namespace SkillSignal.DependencyResolution
{
    using Microsoft.Practices.Unity;

    public class ViewModelFactory : IViewModelFactory
    {
        readonly IUnityContainer container;

        public ViewModelFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public TViewModel Get<TViewModel>()
        {
            return container.Resolve<TViewModel>();
        }
    }
}