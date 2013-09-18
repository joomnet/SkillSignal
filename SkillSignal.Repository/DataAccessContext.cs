using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SkillSignal.Repository
{
    using SkillSignal.Domain;

    public class DataAccessContext : DbContext
    {
        public DbSet<UserAccount> Users { get; set; }
    }
}
