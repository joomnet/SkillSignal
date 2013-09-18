using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.Server
{
    public class UserService : IUserService
    {
        public bool IsAuthenticated(string userName, string passWord)
        {
            return true;
        }
    }
}
