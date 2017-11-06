using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 200;
            return View("NotFoundError");
        }

        public ActionResult InternalServer()
        {
            Response.StatusCode = 200;
            return View("InternalServerError");
        }

        public ActionResult BadGateway()
        {
            Response.StatusCode = 200;
            return View("BadGatewayError");
        }

        public ActionResult ServiceUnavailable()
        {
            Response.StatusCode = 200;
            return View("ServiceUnavailableError");
        }
    }
}