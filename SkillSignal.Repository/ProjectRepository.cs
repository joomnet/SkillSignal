using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSignal.Domain;
using SkillSignal.IRepository;

namespace SkillSignal.Repository
{

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DataAccessContext dataAccessContext):base(dataAccessContext)
        {
            
        }
    }
}
