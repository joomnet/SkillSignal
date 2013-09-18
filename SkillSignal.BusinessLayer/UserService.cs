namespace SkillSignal.BusinessLayer
{
    using System;
    using System.Collections.Generic;

    using SkillSignal.Domain;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;

    public class UserService : IUserService
    {
        readonly IDALContext _dalContext;

        public UserService(IDALContext dalContext)
        {
            this._dalContext = dalContext;
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
            return this._dalContext.Users.Filter(x => userFilter(x));
        }

        public void SaveOrUpdate(UserAccount userAccount)
        {
            this._dalContext.Users.Create(userAccount);
        }

        public void Delete(string id)
        {
            var deleted = this._dalContext.Users.Delete(user => user.Id == id);
        }

        public IEnumerable<UserAccount> GetActiveUsers()
        {
            return this._dalContext.Users.Filter(x => x.IsActive);
        }
    }
}