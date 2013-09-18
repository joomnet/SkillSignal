using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.Repository
{
    using SkillSignal.Domain;
    using SkillSignal.IRepository;

    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(DataAccessContext dataAccessContext) : base(dataAccessContext)
        {
        }
    }

}
