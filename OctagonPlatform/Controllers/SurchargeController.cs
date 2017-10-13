using OctagonPlatform.Models.InterfacesRepository;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class SurchargeController : Controller
    {
        private readonly ISurchargeRepository _surchargeRepository;

        public SurchargeController(ISurchargeRepository repository)
        {
            _surchargeRepository = repository;
        }
        // GET: Surcharge
        public ActionResult Index()
        {
            return View();
        }
    }
}