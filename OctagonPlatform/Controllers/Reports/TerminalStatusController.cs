
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
       
        public TerminalStatusController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
            :base(repo, repoterminal, repopartner, repogroup)
        {           
          
          
        }

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {
            throw new NotImplementedException();
        }

       

        public override Task<bool> RunReport(object aviewmodel, string format)
        {
            throw new NotImplementedException();
        }

        public ActionResult TerminalStatus()
        {
            TempData["Chart"] = null;
            TempData["sub"] = false;
            return View("TerminalStatus");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TerminalStatus([Bind(Include = "Status,Partner,PartnerId,Group,GroupId,City,Cityid,State,StateId,ZipCode")] TerminalStatusViewModel vmodel)
        {
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("CityId");
            ModelState.Remove("StateId");

            if (ModelState.IsValid)
            {
                List<TerminalStatusTableVM> listaux = new List<TerminalStatusTableVM>();
                List<JsonTerminalStatusChart> listchart = new List<JsonTerminalStatusChart>();
                List<JsonTerminalStatusReport> list = new List<JsonTerminalStatusReport>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                list = await api.TerminalStatus(listtn);
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

                #region Variables Partial
                TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<TerminalStatusTableVM>(listaux) : null;
                TempData["filename"] = "TerminalStatus";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                TempData["sub"] = false;
                #endregion

                return View("TerminalStatus");
            }

            return RedirectToAction("Index");
        }


    }
}
