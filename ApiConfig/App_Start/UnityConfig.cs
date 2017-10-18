using Microsoft.Practices.Unity;
using ApiConfig.Models.InterfacesRepository;
using ApiConfig.PersistanceRepository;
using System.Web.Mvc;
using Unity.Mvc5;

namespace ApiConfig
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            
            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<ITerminalAlertRepo, TerminalAlertRepo>();
            

        }
    }
}