﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18331
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkillSignal.UserServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServices.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CanAuthenticate", ReplyAction="http://tempuri.org/IUserService/CanAuthenticateResponse")]
        bool CanAuthenticate(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CanAuthenticate", ReplyAction="http://tempuri.org/IUserService/CanAuthenticateResponse")]
        System.Threading.Tasks.Task<bool> CanAuthenticateAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Where", ReplyAction="http://tempuri.org/IUserService/WhereResponse")]
        SkillSignal.Domain.UserAccount[] Where(System.Func<SkillSignal.Domain.UserAccount, bool> userFilter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Where", ReplyAction="http://tempuri.org/IUserService/WhereResponse")]
        System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> WhereAsync(System.Func<SkillSignal.Domain.UserAccount, bool> userFilter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SaveOrUpdate", ReplyAction="http://tempuri.org/IUserService/SaveOrUpdateResponse")]
        void SaveOrUpdate(SkillSignal.Domain.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SaveOrUpdate", ReplyAction="http://tempuri.org/IUserService/SaveOrUpdateResponse")]
        System.Threading.Tasks.Task SaveOrUpdateAsync(SkillSignal.Domain.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Delete", ReplyAction="http://tempuri.org/IUserService/DeleteResponse")]
        void Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Delete", ReplyAction="http://tempuri.org/IUserService/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetActiveUsers", ReplyAction="http://tempuri.org/IUserService/GetActiveUsersResponse")]
        SkillSignal.Domain.UserAccount[] GetActiveUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetActiveUsers", ReplyAction="http://tempuri.org/IUserService/GetActiveUsersResponse")]
        System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> GetActiveUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        SkillSignal.Domain.UserAccount[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> GetAllUsersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : SkillSignal.UserServices.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<SkillSignal.UserServices.IUserService>, SkillSignal.UserServices.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CanAuthenticate(string userName, string password) {
            return base.Channel.CanAuthenticate(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> CanAuthenticateAsync(string userName, string password) {
            return base.Channel.CanAuthenticateAsync(userName, password);
        }
        
        public SkillSignal.Domain.UserAccount[] Where(System.Func<SkillSignal.Domain.UserAccount, bool> userFilter) {
            return base.Channel.Where(userFilter);
        }
        
        public System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> WhereAsync(System.Func<SkillSignal.Domain.UserAccount, bool> userFilter) {
            return base.Channel.WhereAsync(userFilter);
        }
        
        public void SaveOrUpdate(SkillSignal.Domain.UserAccount userAccount) {
            base.Channel.SaveOrUpdate(userAccount);
        }
        
        public System.Threading.Tasks.Task SaveOrUpdateAsync(SkillSignal.Domain.UserAccount userAccount) {
            return base.Channel.SaveOrUpdateAsync(userAccount);
        }
        
        public void Delete(int id) {
            base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public SkillSignal.Domain.UserAccount[] GetActiveUsers() {
            return base.Channel.GetActiveUsers();
        }
        
        public System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> GetActiveUsersAsync() {
            return base.Channel.GetActiveUsersAsync();
        }
        
        public SkillSignal.Domain.UserAccount[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<SkillSignal.Domain.UserAccount[]> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
    }
}
