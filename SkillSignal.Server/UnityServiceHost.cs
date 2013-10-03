using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.ServiceModel;

namespace SkillSignal.DependencyInjection
{
     public class UnityServiceHost : ServiceHost
     {
     private IUnityContainer unityContainer;
 
     public UnityServiceHost(IUnityContainer unityContainer, Type serviceType) : base(serviceType)
     {
         this.unityContainer = unityContainer;
     }
 
     protected override void OnOpening()
     {
         base.OnOpening();
 
         if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
         {
             this.Description.Behaviors.Add(new UnityServiceBehavior(this.unityContainer));
         }
     }
  }
}
