using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        
        public ActionResult Index()
        {
            
            return View();
        }
        
    }
}