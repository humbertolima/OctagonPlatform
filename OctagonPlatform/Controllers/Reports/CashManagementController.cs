
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
    public class CashManagementController : ReportsSmartController
    {
        private List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
        public CashManagementController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup, IUserRepository userrepo)
            :base(repo, repoterminal, repopartner, repogroup,userrepo)
        {           
          
          
        }

        public ActionResult CashManagement()
        {
            
            CashManagementViewModel vmodel = new CashManagementViewModel();
            vmodel.UserId =  Convert.ToInt32(Session["userId"]);
            TempData["Chart"] = null;
            TempData["sub"] = false;
            return View("CashManagement",vmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashManagement([Bind(Include = "TerminalId,Status,Partner,PartnerId,Group,GroupId,UserId")] CashManagementViewModel vmodel)
        {
            bool envio = await RunReport(vmodel, "pdf");
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
                #region Variables Partial
                Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? Utils.ToDataTable(alist.Item1, alist.Item2) : null;

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

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                CashManagementViewModel vmodel = aviewmodel as CashManagementViewModel;

                // IEnumerable alist;
                Type type = typeof(CashManagementTableVM);

                List<CashManagementTableVM> listaux = new List<CashManagementTableVM>();
               
                List<JsonCashManagement> list = new List<JsonCashManagement>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                list = await api.CashManagement(vmodel.TerminalId, listtn);
                int partnerid = _repo_user.FindBy(vmodel.UserId).PartnerId;
                IEnumerable <dynamic> listTn = repo_terminal.LoadCashMngList(list, vmodel.Status, vmodel.PartnerId, partnerid);
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


                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public async Task<bool> RunReport(CashManagementViewModel vmodel, string format)
        {
            try
            {
                return await SendReport(vmodel, "CashManagement", format, "Cash Management");
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
