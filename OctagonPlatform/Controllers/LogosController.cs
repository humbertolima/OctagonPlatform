using OctagonPlatform.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class LogosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogosController()
        {
           _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Upload(int partnerId)
        {
            ViewBag.PartnerId = partnerId;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, int partnerId)
        {
            if (file == null)
            {
                return RedirectToAction("Upload", partnerId);
            }
            var partner = _context.Partners.FirstOrDefault(x => x.Id == partnerId);
            var fileName = Path.GetFileName(file.FileName);

            if (fileName == null || partner == null) return RedirectToAction("Upload", partnerId);

            var path = Path.Combine(Server.MapPath("~/Content/Uploads/PartnerLogos"), fileName);
            file.SaveAs(path);

            partner.Logo = path;
            _context.SaveChanges();

            return RedirectToAction("Details", "Partners", partnerId);
        }
    }
}