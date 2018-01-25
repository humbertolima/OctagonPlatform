
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
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers.Reports
{
    [AllowAnonymous]
    public class ReportsSmartController : Controller
    {
        protected IReports _repo;
        protected ITerminalRepository repo_terminal;
        protected IPartnerRepository repo_partner;
        protected IReportGroup repo_group;
       
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
        

     
      
        public ActionResult CashBalanceatClose()
        {
            CashBalanceatCloseViewModel vmodel = new CashBalanceatCloseViewModel();
            TempData["Chart"] = null;
            TempData["sub"] = false;
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
                TempData["sub"] = false;
                #endregion
               
                return View("CashBalanceatClose/CashBalanceatClose");
            }

            return RedirectToAction("Index");
        }

        protected string[] ListTerminalByGroup(int groupId)
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

    }
}
public class CustomViewEngine : RazorViewEngine
{
    public CustomViewEngine()
        : base()
    {

        var viewLocations = new[] {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/ReportsSmart/{1}/{0}.cshtml"

        };

        this.PartialViewLocationFormats = viewLocations;
        this.ViewLocationFormats = viewLocations;


    }
}