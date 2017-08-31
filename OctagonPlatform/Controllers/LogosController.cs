using OctagonPlatform.Models.InterfacesRepository;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class LogosController : Controller
    {
        private readonly ILogoRepository _logoRepository;

        public LogosController(ILogoRepository logoRepository)
        {
            _logoRepository = logoRepository;
        }

        [HttpGet]
        public ActionResult Upload()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, int partnerId)
        {
            _logoRepository.UploadLogo(file, partnerId);
            return RedirectToAction("Details", "Partners");
        }
    }
}