using OctagonPlatform.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class LogosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogosController()
        {
           _context = new ApplicationDbContext();
        }

        public ActionResult Index(int partnerId, string error)
        {
            try
            {
                
                ViewBag.PartnerId = partnerId;
                ViewBag.Error = error;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "File not found. ";
                return RedirectToAction("Details", "Partners", new { id = partnerId });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, int partnerId)
        {
            try
            {
                if (file == null)
                {
                    const string error = "Could not upload the file, try egain!!!";
                    return RedirectToAction("Index", new {partnerId, error});
                }

                var partner = _context.Partners.SingleOrDefault(x => x.Id == partnerId);
                if (partner == null)
                {
                    const string error = "Could not find the Partner, try egain!!!";
                    return RedirectToAction("Index", new {partnerId, error});
                }
                file.SaveAs(Server.MapPath("~/Uploads/" + file.FileName.ToLower().Trim()));
                partner.Logo = file.FileName.ToLower().Trim();
                _context.SaveChanges();

                var userLogged = _context.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);

                if (userLogged == null || userLogged.PartnerId != partner.Id)
                    return RedirectToAction("Details", "Partners", new {id = partnerId});
                Session["logo"] = partner.Logo;
                Session["businessName"] = partner.BusinessName;


                return RedirectToAction("Details", "Partners", new {id = partnerId});
            }
            catch (Exception)
            {
                ViewBag.Error = "File not found. ";
                return RedirectToAction("Details", "Partners", new { id = partnerId });
            }
        }
    }
}