namespace SkillSignal.IBusinessLayer
{
    using System.ServiceModel;

    using SkillSignal.Domain;

    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        Project Create(string projectName);
    }
}