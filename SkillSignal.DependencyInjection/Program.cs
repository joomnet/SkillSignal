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
                .RegisterType<IDALContext, DALContext>()
                .RegisterType<IUserAccountRepository, UserAccountRepository>();

            Console.WriteLine("Initializing Service Host..");


                    Uri serviceAddress = new Uri("http://localhost:9001/UserService");
                    Uri mexAddress = new Uri("http://localhost:9001/UserService/mex");
                    using (UnityServiceHost host = new UnityServiceHost(unityContainer, unityContainer.Resolve<IUserService>().GetType()))
                    {
                        host.Description.Behaviors.Add(new ServiceMetadataBehavior());
                        host.AddServiceEndpoint(typeof(IUserService), new BasicHttpBinding(), serviceAddress);
                        host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), mexAddress);

                        Console.WriteLine("Opening service host.. ");

                        host.Open();

                        Console.WriteLine("Listening.. press any key to exit.");

                        Console.Read();
         
                        host.Close();
                    }
               }
           }
}
