using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class PopulateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PopulateController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult GetAllStates(int? countryId)
        {
            try
            {
                IEnumerable<SelectListItem> result = _context.States
                    .Where(c => c.CountryId == countryId)
                    .Select(c => new SelectListItem {Text = c.Name, Value = c.Id.ToString()});

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
        public ActionResult GetAllCities(int? stateId)
        {
            try
            {
                IEnumerable<SelectListItem> result = _context.Cities
                    .Where(c => c.StateId == stateId)
                    .Select(c => new SelectListItem {Text = c.Name, Value = c.Id.ToString()});

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
        public ActionResult GetAllBankAccount()
        {
            try
            {
                IEnumerable<SelectListItem> result = _context.BankAccounts
                    .Where(c => c.Deleted == false)
                    .Select(c => new SelectListItem {Text = c.AccountNumber, Value = c.Id.ToString()});

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
    }
}