
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers.Reports
{
    [AllowAnonymous]
    public class ReportsSmartController : Controller
    {
        private IReports _repo;
        public ReportsSmartController(IReports repo)
        {
            _repo = repo;
        }
        // GET: reportModels
        public ActionResult Index()
        {
            IEnumerable<ReportModel>  list = _repo.GetReportsDasboard();            
            return View(list);
        }
        public  ActionResult CashLoad()
        {
            CashLoadViewModel model = new CashLoadViewModel();
         
            Session["businessName"] = "";
            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashLoad([Bind(Include = "TerminalId,StartDate,EndDate")] CashLoadViewModel vmodel)
        {
            List<JsonLoadCash> list = new List<JsonLoadCash>();
            if (ModelState.IsValid)
            {
                ApiATM api = new ApiATM();
                DateTime start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                list = await api.CashLoad(vmodel.TerminalId, start, end);
                TempData["List"] = list;
                return View();
               
            }

            return RedirectToAction("Index");
        }
        public ActionResult CashBalanceatClose()
        {
            return View();
        }
        public ActionResult CashManagement()
        {
            return View();
        }
        public ActionResult CashManagementByReportGroup()
        {
            return View();
        }
        
    }
}