
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
        private ITerminalRepository repo_terminal;
        public ReportsSmartController(IReports repo, ITerminalRepository repoterminal)
        {
            _repo = repo;
            repo_terminal = repoterminal;
        }
        // GET: reportModels
        public ActionResult Index()
        {
            IEnumerable<ReportModel> list = _repo.GetReportsDasboard();
            return View(list);
        }
        public ActionResult CashLoad()
        {
            CashLoadViewModel model = new CashLoadViewModel();

            Session["businessName"] = "";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashLoad([Bind(Include = "TerminalId,StartDate,EndDate,Status")] CashLoadViewModel vmodel)
        {
           
            if (ModelState.IsValid)
            {
                List<JsonLoadCashReport> listaux = new List<JsonLoadCashReport>();
                List<JsonLoadCash> list = new List<JsonLoadCash>();
                ApiATM api = new ApiATM();


                if (vmodel.TerminalId != null && vmodel.StartDate != null && vmodel.EndDate != null)
                {
                    DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    list = await api.CashLoad(start, end, vmodel.TerminalId);
                }
                if (vmodel.TerminalId == null && vmodel.StartDate != null && vmodel.EndDate != null)
                {
                    DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    list = await api.CashLoad(start, end);
                }
                if (vmodel.TerminalId != null && vmodel.StartDate == null && vmodel.EndDate == null)
                    list = await api.CashLoad(null,null,vmodel.TerminalId);
                if (vmodel.TerminalId == null && vmodel.StartDate == null && vmodel.EndDate == null)
                    list = await api.CashLoad();

                IEnumerable<dynamic> listTn = repo_terminal.LoadCashList(list,vmodel.Status);

                if (listTn.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        string locationname = "";
                        foreach (dynamic x in listTn)
                        {
                            if (x.TerminalId == item.TerminalId)
                            {
                                locationname = x.LocationName;
                                break;
                            }
                        }
                        JsonLoadCashReport obj = new JsonLoadCashReport(locationname, item.Date, item.AmountPrevius, item.AmountLoad, item.AmountCurrent, item.TerminalId);
                        listaux.Add(obj);
                    }
                   
                }
                TempData["List"] = listaux;
                Session["businessName"] = "";
                return View();
            }

            return RedirectToAction("Index");
        }


        public ActionResult AutoTerminal(string term)
        {
          
            IEnumerable<string> list = repo_terminal.GetAllTerminalId(term);         
           
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Autocomplete(string term)
        {
            var items = new[] {"Apple", "Pear", "Banana", "Pineapple", "Peach"};

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
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