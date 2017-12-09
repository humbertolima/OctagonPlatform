
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
                            CashLoadTableVM obj = new CashLoadTableVM(item.TerminalId,locationname, item.AmountPrevius.ToString(), item.AmountLoad.ToString(), item.AmountCurrent.ToString(), item.Date);
                            JsonLoadCashChart objchart = new JsonLoadCashChart(item.Date.ToString("yyyy-MM-dd"), item.AmountPrevius, item.AmountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        }
                    }

                }
              
                #region Variables Partial
                TempData["List"] = Utils.ToDataTable<CashLoadTableVM>(listaux); 
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
                List<JsonCashManagementReport> listaux = new List<JsonCashManagementReport>();
                List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
                List<JsonCashManagement> list = new List<JsonCashManagement>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                list = await api.CashManagement(vmodel.TerminalId, listtn);
                IEnumerable<dynamic> listTn = repo_terminal.LoadCashMngList(list, vmodel.Status, vmodel.PartnerId);
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
                            JsonCashManagementReport obj = new JsonCashManagementReport(locationname, item.Date, item.AmountPrevius, item.AmountLoad, item.AmountCurrent, item.TerminalId);
                            JsonLoadCashChart objchart = new JsonLoadCashChart(item.Date.ToString("yyyy-MM-dd"), item.AmountPrevius, item.AmountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        }
                    }

                }
                List<CashManagementTableVM> listcash = GenerateCashManagementReport(listaux);
                DataTable tb = Utils.ToDataTable<CashManagementTableVM>(listcash);

                #region Variables Partial
                TempData["List"] = tb;
                TempData["Chart"] = JsonConvert.SerializeObject(listchart);
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                #endregion
                Session["businessName"] = "";
                return View();
            }

            return RedirectToAction("Index");
        }

        private List<CashManagementTableVM> GenerateCashManagementReport(List<JsonCashManagementReport> listaux)
        {
            List<CashManagementTableVM> list = new List<CashManagementTableVM>();

            string TerminalId = "";
            string Locationname = "";
            int CurrentCashBalance = 0;
            int DaysUntilCashLoad = 0;
            DateTime LastLoadDate = DateTime.Now;
            int PreviousBalance = 0;
            int CashLoadAmount = 0;
            int NewBalance = 0;
            CashManagementTableVM obj = null;
            foreach (var item in listaux)
            {
                List<JsonCashManagementReport> repeat = listaux.FindAll(b => b.TerminalId == item.TerminalId);
                TerminalId = item.TerminalId;
                Locationname = item.Locationname;
                LastLoadDate = item.Date;
                PreviousBalance = item.AmountPrevius;
                CashLoadAmount = item.AmountLoad;
                NewBalance = item.AmountCurrent;

                if (repeat.Count() > 1)
                {
                    CurrentCashBalance = repeat.Last().AmountCurrent;
                    // Difference in days, hours, and minutes.
                    TimeSpan ts = LastLoadDate - repeat.Last().Date;
                    DaysUntilCashLoad = ts.Days;
                }
                else
                {
                    CurrentCashBalance = 0;
                    DaysUntilCashLoad = 0;
                }

                if (!list.Exists(b => b.TerminalID == item.TerminalId))
                {
                    obj = new CashManagementTableVM(TerminalId, Locationname, CurrentCashBalance.ToString(), DaysUntilCashLoad.ToString(), LastLoadDate.ToString(), PreviousBalance.ToString(), CashLoadAmount.ToString(), NewBalance.ToString());
                    list.Add(obj);
                }

            }
            return list;
        }

        public ActionResult CashManagementByReportGroup()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string html,string filename)
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
                IEnumerable<TerminalTableVM> listvm =repo_terminal.GetTerminalsReport(vmodel, listtn);
                DataTable tb = Utils.ToDataTable<TerminalTableVM>(listvm);
                TempData["List"] = tb;
                return View();
            }
            return RedirectToAction("Index");
        }
       
    }
}
