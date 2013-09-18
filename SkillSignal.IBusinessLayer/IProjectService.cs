namespace SkillSignal.IBusinessLayer
{
    using SkillSignal.Domain;

    public interface IProjectService
    {
        Project Create(string projectName);
    }
}