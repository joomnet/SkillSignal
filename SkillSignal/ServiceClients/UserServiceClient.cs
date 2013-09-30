namespace SkillSignal.ServiceClients
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    using SkillSignal.Domain;
    using SkillSignal.IBusinessLayer;

    public class UserServiceClient : IUserService
    {
        WCFUserService.IUserService _userService;

        public UserServiceClient()
        {
            this._userService = new WCFUserService.UserServiceClient(new BasicHttpBinding(), new EndpointAddress("http://localhost:9001/UserService"));
        }

        public bool CanAuthenticate(string userName, string password)
        {
            return this._userService.CanAuthenticate(userName, password);
        }

        public IEnumerable<UserAccount> Where(Func<UserAccount, bool> userFilter)
        {
            return this._userService.Where(userFilter);
        }

        public void SaveOrUpdate(UserAccount userAccount)
        {
            this._userService.SaveOrUpdate(userAccount);
        }

        public void Delete(int id)
        {
            this._userService.Delete(id);
        }

        public IEnumerable<UserAccount> GetActiveUsers()
        {
           return this._userService.GetActiveUsers();
        }

        public IEnumerable<UserAccount> GetAllUsers()
        {
            return this._userService.WhereAsync(u => u != null).Result;
        }
    }
}