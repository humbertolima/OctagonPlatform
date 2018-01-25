﻿
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
using System.Collections;
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
    public abstract class ReportsSmartController : Controller
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


        public abstract Task<Tuple<IEnumerable, Type>>  GetList(object aviewmodel);

        public async Task<string> HTML(string title, object aviewmodel)
        {

            Tuple<System.Collections.IEnumerable, Type> tempList =  await GetList(aviewmodel);

            IEnumerable listaux = tempList.Item1;
            int count = listaux.Cast<Object>().Count();
            TempData["List"] = count > 0 ? Utils.ToDataTable(listaux, tempList.Item2) : null;
            TempData["filename"] = "CashManagement";
           

            ViewData["Title"] = title;
            string fffff = RazorViewToString.RenderRazorViewToString(this, "../_PartialTable",null);
            string fffff2 = RazorViewToString.RenderRazorViewToString(this, "../_PartialHead", null);


            //string gggg = RazorViewToString.RenderViewToString("CashBalanceatClose", "../_PartialHead", new CashBalanceatCloseViewModel(), this);

            string css = "<style>.mitable table { border-collapse: collapse;}" +
  ".mitable td,.mitable  th {text-align:left } " +
  ".mitable tr td,.mitable tr th {border-bottom:solid black 0.5pt; padding: .75pt .75pt .75pt .75pt; }" +
  ".mitable tr td {font-size:10pt;background: white}" +
  ".mitable tr th { background: #f9f9f9;font-size:10pt; height: 18.6pt}</style>";
            //var ahtml = $('<div></div>').append($("#" + idtable).clone()).html();
            string head = fffff2.Replace("logo.png\">", "logo.png\" />");
            string html1 = css + "<div id ='head'>" + head + "</div>" + "<div id ='table' class='mitable'>" + fffff + "</div>";

            return html1;

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