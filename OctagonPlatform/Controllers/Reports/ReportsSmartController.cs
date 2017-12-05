
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

            if (ModelState.IsValid)
            {
                List<JsonLoadCashReport> listaux = new List<JsonLoadCashReport>();
                List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
                List<JsonLoadCash> list = new List<JsonLoadCash>();
                ApiATM api = new ApiATM();

                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);    

                list = await api.CashLoad(start, end, vmodel.TerminalId, listtn);

                IEnumerable<dynamic> listTn = repo_terminal.LoadCashList(list, vmodel.Status,vmodel.PartnerId);

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
                            JsonLoadCashReport obj = new JsonLoadCashReport(locationname, item.Date, item.AmountPrevius, item.AmountLoad, item.AmountCurrent, item.TerminalId);
                            JsonLoadCashChart objchart = new JsonLoadCashChart(item.Date.ToString("yyyy-MM-dd"), item.AmountPrevius, item.AmountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        }
                    }

                 }
                #region Variables Partial
                TempData["List"] = listaux;
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
        public ActionResult CashManagementByReportGroup()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string html)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(html);
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "ReportLoadCash.pdf");
            }
        }
    }
}