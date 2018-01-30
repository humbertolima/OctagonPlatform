
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
    public class TerminalStatusController : ReportsSmartController
    {
        private List<JsonTerminalStatusChart> listchart = new List<JsonTerminalStatusChart>();
        IUserRepository _repoUser;
        public TerminalStatusController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup, IUserRepository repoUser)
            :base(repo, repoterminal, repopartner, repogroup)
        {
            _repoUser = repoUser;


        }

      

        public ActionResult TerminalStatus()
        {
            TempData["Chart"] = null;
            TempData["sub"] = false;
            TerminalStatusViewModel vmodel = new TerminalStatusViewModel();
            vmodel.UserId = Convert.ToInt32(Session["userId"]);
            return View("TerminalStatus",vmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TerminalStatus([Bind(Include = "Status,Partner,PartnerId,Group,GroupId,City,Cityid,State,StateId,ZipCode,UserId")] TerminalStatusViewModel vmodel)
        {
            bool envio = await RunReport(vmodel, "pdf");
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("CityId");
            ModelState.Remove("StateId");

            if (ModelState.IsValid)
            {
               
                #region Variables Partial
                 Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? Utils.ToDataTable(alist.Item1, alist.Item2) : null;

                TempData["filename"] = "TerminalStatus";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                TempData["sub"] = false;
                #endregion

                return View("TerminalStatus");
            }

            return RedirectToAction("Index");
        }

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                TerminalStatusViewModel vmodel = aviewmodel as TerminalStatusViewModel;

                // IEnumerable alist;
                Type type = typeof(TerminalStatusTableVM);
               
                List<TerminalStatusTableVM> listaux = new List<TerminalStatusTableVM>();
               
                List<JsonTerminalStatusReport> list = new List<JsonTerminalStatusReport>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);

                User user = _repoUser.FindBy(vmodel.UserId);
                TimeSpan utcoffset = Utils.UtcOffset(user.TimeZoneInfo);
                
                list = await api.TerminalStatus( utcoffset, listtn);
                IEnumerable<dynamic> listTn = repo_terminal.TerminalStatus(list, vmodel.Status, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]), vmodel.CityId, vmodel.StateId, vmodel.ZipCode);
                if (listTn.Count() > 0)
                {
                    foreach (var item in listTn)
                    {
                        int? cashBalance = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.CashBalance).FirstOrDefault();
                        int? dayuntilcashload = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.Dayuntilcashload).FirstOrDefault();
                        DateTime? lastComunication = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastComunication).FirstOrDefault();
                        List<DateTime?> lastran = new List<DateTime?>
                        {
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction11).FirstOrDefault(),
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction12).FirstOrDefault(),
                            list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.LastTransaction15).FirstOrDefault()
                        };

                        DateTime? lastTransaction = lastran.Max();
                        int? hourInactive = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.HourInactive).FirstOrDefault();
                        TerminalStatusTableVM obj = new TerminalStatusTableVM(item.TerminalId, item.LocationName, cashBalance, dayuntilcashload, item.Name, item.Phone, "Falta esta columna", lastComunication, lastTransaction, hourInactive);
                        JsonTerminalStatusChart objchart = new JsonTerminalStatusChart(item.TerminalId, cashBalance, lastComunication?.ToString("yyyy-MM-dd H:m:s"));
                        listchart.Add(objchart);
                        listaux.Add(obj);

                    }

                }

                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public override async Task<bool> RunReport(object aviewmodel, string format)
        {
            TerminalStatusViewModel vmodel = aviewmodel as TerminalStatusViewModel;
            return await SendReport(vmodel, "TerminalStatus", format, "Terminal Status");
        }
    }
}
