using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class VaultCashController : Controller
    {
        // GET: VaultCash
        public ActionResult Index()
        {
            return View();
        }
    }
}