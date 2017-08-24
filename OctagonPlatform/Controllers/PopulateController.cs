using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class PopulateController : Controller
    {
        public ApplicationDbContext Context;


        public ActionResult GetAllStates(int? CountryID)
        {
            Context = new ApplicationDbContext();

            IEnumerable<SelectListItem> result = Context.States
                .Where(c => c.CountryId == CountryID)
                .Select(c => new SelectListItem { Text = c.Name, Value= c.Id.ToString() });
            
                return Json(result, JsonRequestBehavior.AllowGet);
            
        }
    }
}