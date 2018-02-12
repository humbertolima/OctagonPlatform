
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
    public class CashLoadController : ReportsSmartController
    {
       private List<JsonLoadCashChart> listchart = new List<JsonLoadCashChart>();
        public CashLoadController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup, IUserRepository userrepo)
            :base(repo, repoterminal, repopartner, repogroup, userrepo)
        {

            
        }
      
        public ActionResult CashLoad()
        {
            CashLoadViewModel model = new CashLoadViewModel();
            TempData["Chart"] = null;
            TempData["Sub"] = false;
            ViewBag.Culture = CultureInfoSession.Name;
            return View("CashLoad", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CashLoad([Bind(Include = "TerminalId,StartDate,EndDate,Status,Partner,PartnerId,Group,GroupId")] CashLoadViewModel vmodel)
        {
            bool envio = await RunReport(vmodel, "excel");
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");
            ModelState.Remove("TerminalId");
            if (ModelState.IsValid)
            {
               

                #region Variables Partial
               
                Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? Utils.ToDataTable(alist.Item1, alist.Item2) : null;

                TempData["filename"] = "CashLoad";
                TempData["Chart"] = listchart.Count() > 0 ? JsonConvert.SerializeObject(listchart) : null;
                TempData["terminal"] = vmodel.TerminalId;
                TempData["partner"] = vmodel.Partner;
                TempData["from"] = vmodel.StartDate;
                TempData["to"] = vmodel.EndDate;
                TempData["Sub"] = false;
                #endregion

                return View("CashLoad");
            }

            return RedirectToAction("Index");
        }

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run(async () => {


                CashLoadViewModel vmodel = aviewmodel as CashLoadViewModel;

                // IEnumerable alist;
                Type type = typeof(CashLoadTableVM);

                List<CashLoadTableVM> listaux = new List<CashLoadTableVM>();
                
                List<JsonLoadCash> list = new List<JsonLoadCash>();
                ApiATM api = new ApiATM();

                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime? end = DateTime.ParseExact(vmodel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                string[] listtn = ListTerminalByGroup(vmodel.GroupId);

                list = await api.CashLoad(start, end, vmodel.TerminalId, listtn);

                IEnumerable<dynamic> listTn = repo_terminal.LoadCashList(list, vmodel.Status, vmodel.PartnerId, Convert.ToInt32(Session["partnerId"]));

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
                            CashLoadTableVM obj = new CashLoadTableVM(item.TerminalId, locationname, item.Date, item.AmountPrevius.ToString(), item.AmountLoad.ToString(), item.AmountCurrent.ToString());
                            JsonLoadCashChart objchart = new JsonLoadCashChart(item.Date.ToString("yyyy-MM-dd"), item.AmountPrevius, item.AmountLoad);
                            listchart.Add(objchart);
                            listaux.Add(obj);
                        }
                    }

                }


                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public async Task<bool> RunReport(CashLoadViewModel vmodel, string format)
        {
           
            return await SendReport(vmodel, "CashLoad", format, "Cash Load");
        }
    }
}
