
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
            TempData["Chart"] = "[]";

            return View(model);
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

                IEnumerable<dynamic> listTn = repo_terminal.LoadCashList(list, vmodel.Status, vmodel.PartnerId);

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
                TempData["Chart"] = JsonConvert.SerializeObject(listchart);
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = vmodel.StartDate;
                TempData["to"] = vmodel.EndDate;
                #endregion
                Session["businessName"] = "";
                return View();
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

            IEnumerable<string> list = repo_terminal.GetAllTerminalId(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoPartner(string term)
        {

            IEnumerable<dynamic> list = repo_partner.GetAllPartner(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoGroup(string term)
        {

            IEnumerable<dynamic> list = repo_group.GetAllGroup(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CashBalanceatClose()
        {
            return View();
        }
        public ActionResult CashManagement()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashManagement([Bind(Include = "TerminalId,Status,Partner,PartnerId,Group,GroupId")] CashManagementVM vmodel)
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
                IEnumerable<dynamic> listTn = repo_terminal.LoadCashMngList(list, vmodel.Status, vmodel.PartnerId);
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
                       
      

        CashManagementTableVM obj = new CashManagementTableVM(item.TerminalId,item.LocationName, cashBalance.ToString(), dayuntilcashload.ToString(), lastLoad.ToString(), amountPrevius.ToString(), amountLoad.ToString(), amountCurrent.ToString());
                            JsonLoadCashChart objchart = new JsonLoadCashChart(lastLoad?.ToString("yyyy-MM-dd"), amountPrevius, amountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        
                    }

                }
             

                #region Variables Partial
                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<CashManagementTableVM>(listaux) : null;
                TempData["Chart"] = JsonConvert.SerializeObject(listchart);
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                #endregion
                Session["businessName"] = "";
                return View();
            }

            return RedirectToAction("Index");
        }

      

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string html, string filename)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(html);
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", filename);
            }
        }
        public ActionResult TerminalList()
        {


            return View();
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
                IEnumerable<TerminalTableVM> listvm = repo_terminal.GetTerminalsReport(vmodel, listtn);

                TempData["List"] = listvm.Count() > 0 ? Utils.ToDataTable<TerminalTableVM>(listvm) : null;
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult TerminalStatus()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TerminalStatus([Bind(Include = "Status,Partner,PartnerId,Group,GroupId,City,Cityid,State,StateId,ZipCode")] TerminalStatusFormFilterVM vmodel)
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
                IEnumerable<dynamic> listTn = repo_terminal.TerminalStatus(list, vmodel.Status, vmodel.PartnerId, vmodel.CityId, vmodel.StateId, vmodel.ZipCode);
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
                TempData["Chart"] = JsonConvert.SerializeObject(listchart);

                #endregion

                return View();
            }

            return RedirectToAction("Index");
        }

    }
}
