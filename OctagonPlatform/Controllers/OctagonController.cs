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

        public ActionResult BeComm()
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

        public ActionResult CryptoXone()
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

        public ActionResult AVT()
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

        public ActionResult DemMoney()
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

        public ActionResult CashEverywhere()
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

        public ActionResult VendBot()
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

        public ActionResult MicroLoans()
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