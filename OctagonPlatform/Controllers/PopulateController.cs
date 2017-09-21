using OctagonPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{

    public class PopulateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PopulateController()
        {
            _context = new ApplicationDbContext();
        }
        [AllowAnonymous]
        public ActionResult GetAllStates(int? countryId)
        {
            IEnumerable<SelectListItem> result = _context.States
                .Where(c => c.CountryId == countryId)
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GetAllCities(int? stateId)
        {
            IEnumerable<SelectListItem> result = _context.Cities
              .Where(c => c.StateId == stateId)
              .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //pendiente poner restriccion para qeu no se acceda de fofrma anonima.
        public ActionResult GetAllBankAccount()
        {
            IEnumerable<SelectListItem> result = _context.BankAccounts
                 .Select(c => new SelectListItem { Text = c.AccountNumber, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}