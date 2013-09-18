namespace SkillSignal.DependencyResolution
{
    public interface IViewModelFactory
    {
        TViewModel Get<TViewModel>();
    }
}