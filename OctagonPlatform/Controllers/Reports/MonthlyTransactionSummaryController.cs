
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
    public class MonthlyTransactionSummaryController : ReportsSmartController
    {
       
        public MonthlyTransactionSummaryController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
            :base(repo, repoterminal, repopartner, repogroup)
        {           
          
          
        }

        public ActionResult MonthlyTransactionSummary()
        {
            MonthlyTransactionSummaryViewModel model = new MonthlyTransactionSummaryViewModel();
            TempData["Chart"] = null;
            TempData["sub"] = false;
            return View("MonthlyTransactionSummary", model);
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
                TempData["sub"] = false;
                #endregion

                return View("MonthlyTransactionSummary");
            }

            return RedirectToAction("Index");
        }


    }
}
