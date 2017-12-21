
using Microsoft.Practices.Unity;
using WebApiOctagon.Repository.InterfacesRepository;
using WebApiOctagon.Repository.PersistanceRepository;
using System.Web.Http;
using Unity.WebApi;

namespace WebApiOctagon
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<ITerminalAlertRepo, TerminalAlertRepo>();

            //POR DIOS !!!
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}