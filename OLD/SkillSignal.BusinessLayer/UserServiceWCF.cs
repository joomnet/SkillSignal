using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.IBusinessLayer
{
    using SkillSignal.Domain;

    public class UserServiceWCF : IUserService
    {
        public bool CanAuthenticate(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAccount> Where(Func<UserAccount, bool> userFilter)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
