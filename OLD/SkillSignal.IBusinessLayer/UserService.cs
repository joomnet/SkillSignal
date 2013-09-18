namespace SkillSignal.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SkillSignal.Domain;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;
    using SkillSignal.Repository;

    public class UserService : IUserService
    {
        readonly IDALContext _dalContext;

        public UserService(IDALContext dalContext)
        {
            this._dalContext = dalContext;
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
    }
}