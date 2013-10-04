using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.Repository
{
    using SkillSignal.IRepository;

    public class DALContext : IDALContext
    {
        private DataAccessContext dbContext;
        private IProjectRepository projects;
        private IUserAccountRepository products;

        public DALContext()
        {
            dbContext = new DataAccessContext();
        }

        public IProjectRepository Projects
        {
            get
            {
                if (this.projects == null)
                    this.projects = new ProjectRepository(dbContext);
                return this.projects;
            }
        }

        public IUserAccountRepository Users
        {
            get
            {
                if (products == null)
                    products = new UserAccountRepository(dbContext);
                return products;
            }
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.projects != null)
                this.projects.Dispose();
            if (products != null)
                products.Dispose();
            if (dbContext != null)
                dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
