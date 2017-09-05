using OctagonPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class PopulateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PopulateController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetAllStates(int? countryId)
        {
            

            IEnumerable<SelectListItem> result = _context.States
                .Where(c => c.CountryId == countryId)
                .Select(c => new SelectListItem { Text = c.Name, Value= c.Id.ToString() });
            
                return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCities(int? stateId)
        {

            IEnumerable<SelectListItem> result = _context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}