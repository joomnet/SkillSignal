using System;
using SkillSignal.Domain;


namespace SkillSignal.BusinessLayer
{
    using SkillSignal.IRepository;

    public class ProjectService : IProjectBuilder
    {
        readonly IDALContext dalContext;

        public ProjectService(IDALContext dalContext)
        {
            this.dalContext = dalContext;
        }

        public Project Load(string projectId)
        {
            return !dalContext.Projects.Contains(x => x.ID == projectId) ? Project.NullProject : null;
        }

        public void Create(string projectName)
        {
            dalContext.Projects.Create(new Project {Name = projectName, ID = Guid.NewGuid().ToString()});
        }
    }
}