
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
    public class CashManagementController : ReportsSmartController
    {
       
        public CashManagementController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
            :base(repo, repoterminal, repopartner, repogroup)
        {           
          
          
        }

        public ActionResult CashManagement()
        {
            TempData["Chart"] = null;
            TempData["sub"] = false;
            return View("CashManagement");
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
                TempData["Sub"] = false;
                #endregion
                Session["businessName"] = "";
                return View("CashManagement");
            }

            return RedirectToAction("Index");
        }


    }
}
