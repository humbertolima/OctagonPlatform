using OctagonPlatform.Models.InterfacesRepository;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class InterChangeController : Controller
    {

        private readonly IInterChangeRepository _repository;

        public InterChangeController(IInterChangeRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}