using System;
using SkillSignal.IBusinessLayer;

namespace SkillSignal.ServiceClients
{
    using System.ServiceModel;

    using SkillSignal.Domain;

    public class ProjectServiceClient : IProjectService
    {
        ProjectServices.IProjectService projectServiceClient;
        public ProjectServiceClient()
        {
            projectServiceClient = new ProjectServices.ProjectServiceClient(new BasicHttpBinding(), new EndpointAddress("http://localhost:9001/ProjectService"));            
        }
        public Project Create(string projectName)
        {
            return projectServiceClient.Create(projectName);
        }
    }
}
