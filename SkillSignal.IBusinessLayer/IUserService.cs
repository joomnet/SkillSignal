namespace SkillSignal.IBusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    using SkillSignal.Domain;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool CanAuthenticate(string userName, string password);

        [OperationContract]
        IEnumerable<UserAccount> Where(Func<UserAccount, bool> userFilter);

        [OperationContract]
        void SaveOrUpdate(UserAccount userAccount);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        IEnumerable<UserAccount> GetActiveUsers();

        [OperationContract]
        IEnumerable<UserAccount> GetAllUsers();
    }
}