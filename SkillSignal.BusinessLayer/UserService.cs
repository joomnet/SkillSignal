namespace SkillSignal.BusinessLayer
{
    using System;
    using System.Collections.Generic;

    using SkillSignal.Domain;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;

    public class UserService : IUserService
    {
        readonly IDALContext dalContext;

        public UserService(IDALContext dalContext)
        {
            this.dalContext = dalContext;
        }

        public UserService()
        {
            
        }

        public bool CanAuthenticate(string userName, string password)
        {
            return false;
        }

        public IEnumerable<UserAccount> Where(Func<UserAccount, bool> userFilter)
        {
            return this.dalContext.Users.Filter(x => userFilter(x));
        }

        public void SaveOrUpdate(UserAccount userAccount)
        {
            this.dalContext.Users.Create(userAccount);
        }

        public void Delete(string id)
        {
            var deleted = this.dalContext.Users.Delete(user => user.Id == id);
        }

        public IEnumerable<UserAccount> GetActiveUsers()
        {
            return this.dalContext.Users.Filter(x => x.IsActive);
        }

        public IEnumerable<UserAccount> GetAllUsers()
        {
            return this.dalContext.Users.All();
        }
    }
}