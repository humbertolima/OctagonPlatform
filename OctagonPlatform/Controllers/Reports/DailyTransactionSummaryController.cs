
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
    public class DailyTransactionSummaryController : ReportsSmartController
    {
        private DateTime? start = null;
        private DateTime? end = null;
        public DailyTransactionSummaryController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
            :base(repo, repoterminal, repopartner, repogroup)
        {           
          
          
        }

        public ActionResult DailyTransactionSummary()
        {
            DailyTransactionSummaryViewModel model = new DailyTransactionSummaryViewModel();
            TempData["Chart"] = null;
            TempData["sub"] = false;
            return View("DailyTransactionSummary", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DailyTransactionSummary([Bind(Include = "TerminalId,StartDate,EndDate,Partner,PartnerId,Group,GroupId,Surcharge,Dispensed")] DailyTransactionSummaryViewModel vmodel)
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

                //TempData["List"] = listaux.Count() > 0 ? listaux : null;
                TempData["filename"] = "DailyTransactionSummary";
                TempData["Chart"] = null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = start?.ToString("MMMM d, yyyy");
                TempData["to"] = end?.ToString("MMMM d, yyyy");
                TempData["model"] = vmodel;
                TempData["sub"] = false;
                #endregion

                return View("DailyTransactionSummary");
            }

            return RedirectToAction("Index");
        }

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                DailyTransactionSummaryViewModel vmodel = aviewmodel as DailyTransactionSummaryViewModel;

                // IEnumerable alist;
                Type type = typeof(TransDailyTableVM);

                List<TransDailyTableVM> listaux = new List<TransDailyTableVM>();
                List<JsonDailyTransactionSummary> list = new List<JsonDailyTransactionSummary>();
                ApiATM api = new ApiATM();

                 start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                 end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

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


                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public async Task<bool> RunReport(object aviewmodel, string format)
        {
            DailyTransactionSummaryViewModel vmodel = aviewmodel as DailyTransactionSummaryViewModel;
            return await SendReport(vmodel, "DailyTransactionSummary", format, "Daily Transaction Summary");
        }
    }
}
