
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
using OctagonPlatform.Views.ReportsSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private IPartnerRepository repo_partner;
        private IReportGroup repo_group;
       
        public ReportsSmartController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
        {
            _repo = repo;
            repo_terminal = repoterminal;
            repo_partner = repopartner;
            repo_group = repogroup;
          
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
            TempData["Chart"] = null;
            return View("CashLoad/CashLoad",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashLoad([Bind(Include = "TerminalId,StartDate,EndDate,Status,Partner,PartnerId,Group,GroupId")] CashLoadViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
                List<CashLoadTableVM> listaux = new List<CashLoadTableVM>();
                List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
                List<JsonLoadCash> list = new List<JsonLoadCash>();
                ApiATM api = new ApiATM();

                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);

                list = await api.CashLoad(start, end, vmodel.TerminalId, listtn);

                IEnumerable<dynamic> listTn = repo_terminal.LoadCashList(list, vmodel.Status, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]));

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
                        if (locationname != "")
                        {
                            CashLoadTableVM obj = new CashLoadTableVM(item.TerminalId, locationname, item.Date, item.AmountPrevius.ToString(), item.AmountLoad.ToString(), item.AmountCurrent.ToString());
                            JsonLoadCashChart objchart = new JsonLoadCashChart(item.Date.ToString("yyyy-MM-dd"), item.AmountPrevius, item.AmountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        }
                    }

                }

                #region Variables Partial

                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<CashLoadTableVM>(listaux) : null;
                TempData["filename"] = "CashLoad";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = vmodel.StartDate;
                TempData["to"] = vmodel.EndDate;
                #endregion

                return View("CashLoad/CashLoad");
            }

            return RedirectToAction("Index");
        }

        private string[] ListTerminalByGroup(int groupId)
        {
            try
            {
                string[] listtn = null;
                if (groupId != -1)
                {
                    ReportGroupModel modelg = repo_group.GetGroupById(groupId);
                    IEnumerable<Terminal> listterminal = modelg.Terminals;
                    listtn = new string[modelg.Terminals.Count()];
                    int i = 0;
                    foreach (var item in listterminal)
                    {
                        listtn[i++] = item.TerminalId;
                    }
                }
                return listtn;
            }
            catch (Exception e)
            {

                throw new NullReferenceException(e.Message);
            }

        }

        public ActionResult AutoTerminal(string term)
        {

            IEnumerable<string> list = repo_terminal.GetAllTerminalId(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoPartner(string term)
        {

            IEnumerable<dynamic> list = repo_partner.GetAllPartner(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoGroup(string term)
        {

            IEnumerable<dynamic> list = repo_group.GetAllGroup(term,Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult CashManagement()
        {
            TempData["Chart"] = null;
            return View("CashManagement/CashManagement");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashManagement([Bind(Include = "TerminalId,Status,Partner,PartnerId,Group,GroupId")] CashManagementViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
                List<CashManagementTableVM> listaux = new List<CashManagementTableVM>();
                List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
                List<JsonCashManagement> list = new List<JsonCashManagement>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                list = await api.CashManagement(vmodel.TerminalId, listtn);
                IEnumerable<dynamic> listTn = repo_terminal.LoadCashMngList(list, vmodel.Status, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]));
                if (listTn.Count() > 0)
                {
                    foreach (var item in listTn)
                    {
                        int? cashBalance = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.CashBalance).FirstOrDefault();
                        int? amountPrevius = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.AmountPrevius).FirstOrDefault();
                        int? amountLoad = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.AmountLoad).FirstOrDefault();
                        int? amountCurrent = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.AmountCurrent).FirstOrDefault();
                        int? dayuntilcashload = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.Dayuntilcashload).FirstOrDefault();
                        DateTime? lastLoad = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastLoad).FirstOrDefault();



                        CashManagementTableVM obj = new CashManagementTableVM(item.TerminalId, item.LocationName, cashBalance.ToString(), dayuntilcashload.ToString(), lastLoad.ToString(), amountPrevius.ToString(), amountLoad.ToString(), amountCurrent.ToString());
                        JsonLoadCashChart objchart = new JsonLoadCashChart(lastLoad?.ToString("yyyy-MM-dd"), amountPrevius, amountLoad);
                        listchart.Add(objchart);
                        listaux.Add(obj);

                    }

                }


                #region Variables Partial
                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<CashManagementTableVM>(listaux) : null;
                TempData["filename"] = "CashManagement";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                #endregion
                Session["businessName"] = "";
                return View("CashManagement/CashManagement");
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string html, string filename, string orientation)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                Rectangle pageSize = orientation == "Landscape" ? PageSize.LETTER.Rotate() : PageSize.LETTER;
                StringReader sr = new StringReader(html);
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(pageSize, 20f, 20f, 20f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", filename);
            }
        }
        public ActionResult TerminalList()
        {


            return View("TerminalList/TerminalList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TerminalList([Bind(Include = "TerminalId,Status,Partner,PartnerId,Group,GroupId,Account,AccountId,StartDate,EndDate,ConectionType,State,StateId,City,CityId,ZipCode")] TerminalListViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("AccountId");
            ModelState.Remove("GroupId");
            ModelState.Remove("StateId");
            ModelState.Remove("CityId");
            if (ModelState.IsValid)
            {
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                IEnumerable<TerminalTableVM> listvm = repo_terminal.GetTerminalsReport(vmodel, listtn, Convert.ToInt32(Session["partnerId"]));

                TempData["List"] = listvm.Count() > 0 ? Utils.ToDataTable<TerminalTableVM>(listvm) : null;
                TempData["filename"] = "TerminalList";
                return View("TerminalList/TerminalList");
            }
            return RedirectToAction("Index");
        }

        public ActionResult TerminalStatus()
        {
            TempData["Chart"] = null;
            return View("TerminalStatus/TerminalStatus");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TerminalStatus([Bind(Include = "Status,Partner,PartnerId,Group,GroupId,City,Cityid,State,StateId,ZipCode")] TerminalStatusViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("CityId");
            ModelState.Remove("StateId");

            if (ModelState.IsValid)
            {
                List<TerminalStatusTableVM> listaux = new List<TerminalStatusTableVM>();
                List<JsonTerminalStatusChart> listchart = new List<JsonTerminalStatusChart>();
                List<JsonTerminalStatusReport> list = new List<JsonTerminalStatusReport>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                list = await api.TerminalStatus(listtn);
                IEnumerable<dynamic> listTn = repo_terminal.TerminalStatus(list, vmodel.Status, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]), vmodel.CityId, vmodel.StateId, vmodel.ZipCode);
                if (listTn.Count() > 0)
                {
                    foreach (var item in listTn)
                    {
                        int? cashBalance = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.CashBalance).FirstOrDefault();
                        int? dayuntilcashload = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.Dayuntilcashload).FirstOrDefault();
                        DateTime? lastComunication = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastComunication).FirstOrDefault();
                        List<DateTime?> lastran = new List<DateTime?>
                        {
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction11).FirstOrDefault(),
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction12).FirstOrDefault(),
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction15).FirstOrDefault()
                        };

                        DateTime? lastTransaction = lastran.Max();
                        int? hourInactive = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.HourInactive).FirstOrDefault();
                        TerminalStatusTableVM obj = new TerminalStatusTableVM(item.TerminalId, item.LocationName, cashBalance, dayuntilcashload, item.Name, item.Phone, "Falta esta columna", lastComunication, lastTransaction, hourInactive);
                        JsonTerminalStatusChart objchart = new JsonTerminalStatusChart(item.TerminalId, cashBalance, lastComunication?.ToString("yyyy-MM-dd H:m:s"));
                        listchart.Add(objchart);
                        listaux.Add(obj);

                    }

                }

                #region Variables Partial
                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<TerminalStatusTableVM>(listaux) : null;
                TempData["filename"] = "TerminalStatus";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                #endregion

                return View("TerminalStatus/TerminalStatus");
            }

            return RedirectToAction("Index");
        }
        public ActionResult DailyTransactionSummary()
        {
            DailyTransactionSummaryViewModel model = new DailyTransactionSummaryViewModel();
            TempData["Chart"] = null;

            return View("DailyTransactionSummary/DailyTransactionSummary", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DailyTransactionSummary([Bind(Include = "TerminalId,StartDate,EndDate,Partner,PartnerId,Group,GroupId,Surcharge,Dispensed")] DailyTransactionSummaryViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
                List<TransDailyTableVM> listaux = new List<TransDailyTableVM>();
                List<JsonDailyTransactionSummary> list = new List<JsonDailyTransactionSummary>();
                ApiATM api = new ApiATM();

                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);

                list = await api.DailyTransactionSummary(start, end, vmodel.TerminalId, listtn, vmodel.Surcharge, vmodel.Dispensed);

                IEnumerable<dynamic> listTn = repo_terminal.TransDailyList(list, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]));

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
                        if (locationname != "")
                        {
                            TransDailyTableVM obj = new TransDailyTableVM(item.TerminalId, locationname, item.Date, item.ApprovedWithdrawals, item.Declined, item.SurchargableWithdrawals, item.OtherApproved, item.Reversed, item.SurchargeAmount, item.TotalTransaction, item.Surcharge, item.Dispensed);

                            listaux.Add(obj);
                        }
                    }

                }

                #region Variables Partial

                TempData["List"] = listaux.Count() > 0 ? listaux : null;
                TempData["filename"] = "DailyTransactionSummary";
                TempData["Chart"] = null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = start?.ToString("MMMM d, yyyy");
                TempData["to"] = end?.ToString("MMMM d, yyyy");
                TempData["model"] = vmodel;
                #endregion

                return View("DailyTransactionSummary/DailyTransactionSummary");
            }

            return RedirectToAction("Index");
        }


        public ActionResult MonthlyTransactionSummary()
        {
            MonthlyTransactionSummaryViewModel model = new MonthlyTransactionSummaryViewModel();
            TempData["Chart"] = null;

            return View("MonthlyTransactionSummary/MonthlyTransactionSummary", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MonthlyTransactionSummary([Bind(Include = "TerminalId,StartDate,EndDate,Partner,PartnerId,Group,GroupId,Surcharge")] MonthlyTransactionSummaryViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
                List<TransMonthlyTableVM> listaux = new List<TransMonthlyTableVM>();
                List<JsonMonthlyTransactionSummary> list = new List<JsonMonthlyTransactionSummary>();
                ApiATM api = new ApiATM();

                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);

                list = await api.MonthlyTransactionSummary(start, end, vmodel.TerminalId, listtn, vmodel.Surcharge);

                IEnumerable<dynamic> listTn = repo_terminal.TransMonthlyList(list, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]));

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
                        if (locationname != "")
                        {
                            TransMonthlyTableVM obj = new TransMonthlyTableVM(item.TerminalId, locationname, item.Date, item.ApprovedWithdrawals, item.Declined, item.SurchargableWithdrawals, item.OtherApproved, item.Reversed, item.SurchargeAmount, item.TotalTransaction, item.Surcharge);
                            listaux.Add(obj);
                        }
                    }

                }

                #region Variables Partial

                TempData["List"] = listaux.Count() > 0 ? listaux : null;
                TempData["filename"] = "MonthlyTransactionSummary";
                TempData["Chart"] = null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = start?.ToString("MMMM , yyyy");
                TempData["to"] = end?.ToString("MMMM , yyyy");
                TempData["model"] = vmodel;
                #endregion

                return View("MonthlyTransactionSummary/MonthlyTransactionSummary");
            }

            return RedirectToAction("Index");
        }
       
        public ActionResult CashBalanceatClose()
        {
            CashBalanceatCloseViewModel vmodel = new CashBalanceatCloseViewModel();
            TempData["Chart"] = null;
            return View("CashBalanceatClose/CashBalanceatClose", vmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashBalanceatClose([Bind(Include = "Partner,PartnerId,Group,GroupId,StartDate")] CashBalanceatCloseViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
          
            if (ModelState.IsValid)
            {
                List<CashBalanceAtCloseTableVM> listaux = new List<CashBalanceAtCloseTableVM>();              
                List<JsonCashBalanceClose> list = new List<JsonCashBalanceClose>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                list = await api.CashBalanceClose(start, listtn);
                IEnumerable<dynamic> listTn = repo_terminal.CashBalanceClose(list, vmodel.PartnerId, Convert.ToInt32( Session["partnerId"]));
                if (listTn.Count() > 0)
                {
                    foreach (var item in listTn)
                    {
                        int? cashBalance = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.CashBalance).FirstOrDefault();
                        string time = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.Time).FirstOrDefault();
                        CashBalanceAtCloseTableVM obj = new CashBalanceAtCloseTableVM(item.TerminalId, item.LocationName,time, cashBalance.ToString());
                        listaux.Add(obj);

                    }

                }


                #region Variables Partial
                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<CashBalanceAtCloseTableVM>(listaux) : null;
                TempData["filename"] = "CashManagement";
                TempData["Chart"] =  null;               
                TempData["partner"] = vmodel.Partner;
                #endregion
                Session["businessName"] = "";
                return View("CashBalanceatClose/CashBalanceatClose");
            }

            return RedirectToAction("Index");
        }


    }
}
