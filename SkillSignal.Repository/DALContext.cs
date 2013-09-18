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
        private IProjectRepository categories;
        private IUserAccountRepository products;

        public DALContext()
        {
            dbContext = new DataAccessContext();
        }

        public IProjectRepository Projects
        {
            get
            {
                if (categories == null)
                    categories = new ProjectRepository(dbContext);
                return categories;
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
            if (categories != null)
                categories.Dispose();
            if (products != null)
                products.Dispose();
            if (dbContext != null)
                dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
