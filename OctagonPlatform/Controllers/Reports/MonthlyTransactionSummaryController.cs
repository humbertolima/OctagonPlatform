
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
    public class MonthlyTransactionSummaryController : ReportsSmartController
    {
        private DateTime? start = null;
        private DateTime? end = null;
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
            bool envio = await RunReport(vmodel, "pdf");
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {


                #region Variables Partial

                Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? alist.Item1 : null;

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



        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                MonthlyTransactionSummaryViewModel vmodel = aviewmodel as MonthlyTransactionSummaryViewModel;

                // IEnumerable alist;
                Type type = typeof(TransMonthlyTableVM);

                List<TransMonthlyTableVM> listaux = new List<TransMonthlyTableVM>();
                List<JsonMonthlyTransactionSummary> list = new List<JsonMonthlyTransactionSummary>();
                ApiATM api = new ApiATM();

                 start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                 end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

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


                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public override async Task<bool> RunReport(object aviewmodel, string format)
        {
            MonthlyTransactionSummaryViewModel vmodel = aviewmodel as MonthlyTransactionSummaryViewModel;
            return await SendReport(vmodel, "MonthlyTransactionSummaryViewModel", format, "Monthly Transaction Summary");
        }
    }
}
