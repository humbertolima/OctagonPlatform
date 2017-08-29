using OctagonPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class PopulateController : Controller
    {
        public ApplicationDbContext Context;


        public ActionResult GetAllStates(int? countryId)
        {
            Context = new ApplicationDbContext();

            IEnumerable<SelectListItem> result = Context.States
                .Where(c => c.CountryId == countryId)
                .Select(c => new SelectListItem { Text = c.Name, Value= c.Id.ToString() });
            
                return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCities(int? stateId)
        {
            Context = new ApplicationDbContext();

            IEnumerable<SelectListItem> result = Context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}