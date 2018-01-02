using System;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class OctagonController : Controller
    {
        
        public ActionResult AMS()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        public ActionResult Cryptoxone()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
    }
}