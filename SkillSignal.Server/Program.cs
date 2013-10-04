using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.Unity;
using SkillSignal.BusinessLayer;
using SkillSignal.IBusinessLayer;

namespace SkillSignal.DependencyInjection
{
    using SkillSignal.IRepository;
    using SkillSignal.Repository;

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Building Unity Container...");

                   var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager())
                .RegisterType<IDALContext, DALContext>(new ContainerControlledLifetimeManager())
                .RegisterType<IUserAccountRepository, UserAccountRepository>()
                .RegisterType<IProjectService, ProjectService>(new ContainerControlledLifetimeManager());

            Console.WriteLine("Initializing Service Host..");

                    var userServiceWcfConfiguration = new WCFServiceConfiguration("http://localhost:9001/UserService", "http://localhost:9001/UserService/mex");
                    var projectServiceWcfConfiguration = new WCFServiceConfiguration("http://localhost:9001/ProjectService", "http://localhost:9001/ProjectService/mex");

                    using (UnityServiceHost projectServiceHost = new UnityServiceHost(unityContainer, unityContainer.Resolve<IProjectService>().GetType()))
                    using (UnityServiceHost userServiceHost = new UnityServiceHost(unityContainer, unityContainer.Resolve<IUserService>().GetType()))
                    {
                        userServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior());

                        userServiceHost.AddServiceEndpoint(typeof(IUserService), new BasicHttpBinding(), userServiceWcfConfiguration.ServiceAddress);
                        userServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), userServiceWcfConfiguration.MexAddress);

                        projectServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior());

                        projectServiceHost.AddServiceEndpoint(typeof(IProjectService), new BasicHttpBinding(), projectServiceWcfConfiguration.ServiceAddress);
                        projectServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), projectServiceWcfConfiguration.MexAddress);

                        Console.WriteLine("Opening user service host.. ");
                        userServiceHost.Open();
                        Console.WriteLine("UserService host open.. ");

                        Console.WriteLine("Opening ProjectService host.. ");
                        projectServiceHost.Open();
                        Console.WriteLine("ProjectService host open.. ");

                        Console.WriteLine("Listening.. press any key to exit.");
                        Console.Read();
         
                        userServiceHost.Close();
                        projectServiceHost.Close();
                    }
               }
           }
}
