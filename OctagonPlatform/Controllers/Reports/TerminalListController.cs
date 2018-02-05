
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
    public class TerminalListController : ReportsSmartController
    {
       
        public TerminalListController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup, IUserRepository userrepo)
            :base(repo, repoterminal, repopartner, repogroup,userrepo)
        {           
          
          
        }

     
        public ActionResult TerminalList()
        {

            TempData["sub"] = false;
            return View("TerminalList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TerminalList([Bind(Include = "TerminalId,Status,Partner,PartnerId,Group,GroupId,Account,AccountId,StartDate,EndDate,ConectionType,State,StateId,City,CityId,ZipCode")] TerminalListViewModel vmodel)
        {
            bool envio = await RunReport(vmodel, "pdf");
            ModelState.Remove("PartnerId");
            ModelState.Remove("AccountId");
            ModelState.Remove("GroupId");
            ModelState.Remove("StateId");
            ModelState.Remove("CityId");
            if (ModelState.IsValid)
            {

                Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? Utils.ToDataTable(alist.Item1, alist.Item2) : null;

                TempData["filename"] = "TerminalList";
                TempData["sub"] = false;
                return View("TerminalList");
            }
            return RedirectToAction("Index");
        }
        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                TerminalListViewModel vmodel = aviewmodel as TerminalListViewModel;

                // IEnumerable alist;
                Type type = typeof(TerminalTableVM);               

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                IEnumerable<TerminalTableVM> listvm = repo_terminal.GetTerminalsReport(vmodel, listtn, Convert.ToInt32(Session["partnerId"]));
                
                return new Tuple<IEnumerable, Type>(listvm, type);

            });

        }

        public async Task<bool> RunReport(TerminalListViewModel vmodel, string format)
        {
            return await SendReport(vmodel, "TerminalList", format, "Terminal List");
        }
    }
}
