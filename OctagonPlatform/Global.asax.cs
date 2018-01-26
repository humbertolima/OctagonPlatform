using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;


namespace OctagonPlatform
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());


            ViewEngines.Engines.Add(new CustomViewEngine());


            Task<bool>.Run(()=> {

                DependencyResolver.Current.GetService(typeof(Controllers.Reports.CashBalanceatCloseController)) ;


                Controllers.Reports.ReportsSmartController ctrl =  DependencyResolver.Current.GetService(typeof(Controllers.Reports.CashBalanceatCloseController)) as Controllers.Reports.CashBalanceatCloseController;

                System.Web.Routing.RouteData route = new System.Web.Routing.RouteData();
                route.Values.Add("action", "trash56");
                route.Values.Add("controller", "ReportsCACA");

                ctrl.ControllerContext = new ControllerContext(new System.Web.HttpContextWrapper(System.Web.HttpContext.Current), route, ctrl);// this.ControllerContext;

                return ctrl.RunReport(new Models.FormsViewModels.CashBalanceatCloseViewModel() { }, "pdf");
            }).Wait();


            


        }
        //Para eliminar error en mozilla de Firefox en el console.
        protected void Application_Error(object sender, EventArgs e)
        {

        }
    }
}
