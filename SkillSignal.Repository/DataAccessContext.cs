using System.Data.Entity;
using System.Configuration;
using SkillSignal.Domain;

namespace SkillSignal.Repository
{

    public class DataAccessContext : DbContext
    {
        static readonly string ConnectionString =
            "server=.;database=SkillSignal.Repository.DataAccessContext;Trusted_Connection=True;";//ConfigurationManager.ConnectionStrings["DataAccessContext"].ConnectionString; 

        public DataAccessContext() : base (ConnectionString)
        {
            
        }

        public DbSet<UserAccount> Users { get; set; }
    }
}
