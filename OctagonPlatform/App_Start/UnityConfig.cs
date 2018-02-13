using Microsoft.Practices.Unity;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
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

            
            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<IPartnerRepository, PartnerRepository>();

            // quitar comentarios cuando se cree class

            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<IPartnerContactRepository, PartnerContactRepository>();

            container.RegisterType<ITerminalRepository, TerminalRepository>();

            container.RegisterType<ITerminalContactRepository, TerminalContactRepository>();

            container.RegisterType<IBankAccountRepository, BankAccountRepository>();

            container.RegisterType<IVaultCashRepository, VaultCashRespository>();

            container.RegisterType<ISurchargeRepository, SurchargeRepository>();

            container.RegisterType<ITerminalAlertRepositoryAPI, TerminalAlertRepository>();

            container.RegisterType<IInterChangeRepository, InterChangeRepository>();

            container.RegisterType<IPermission, PermissionRepository>();

            container.RegisterType<IDisputeRepository, DisputeRepository>();

            container.RegisterType<IReports, ReportsRepository>();


            container.RegisterType<IDisputeRepresentRepository, DisputeRepresentRepository>();

            container.RegisterType<IReportGroup, ReportGroupRepository>();
            container.RegisterType<ISchedule, ScheduleRepository>();
            container.RegisterType<ISubscription, SubscriptionRepository>();
            container.RegisterType<IReportFilter, ReportFilterRepository>();
            container.RegisterType<IFilter, FilterRepository>();
            container.RegisterType<ICultureInfo, CultureInfoRepository>();

        }
    }
}