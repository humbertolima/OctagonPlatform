using Microsoft.Practices.Unity;
using OctagonPlatform.Repositories.Logos;
using OctagonPlatform.Repositories.PartnerContacts;
using OctagonPlatform.Repositories.Partners;
using System.Web.Mvc;
using Unity.Mvc5;

namespace OctagonPlatform
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPartnerRepository, PartnerRepository>();
            container.RegisterType<ILogoRepository, LogoRepository>();
            container.RegisterType<IPartnerContactRepository, PartnerContactRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}