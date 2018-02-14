﻿
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using OctagonPlatform.Controllers.Reports.JSON;
using OctagonPlatform.Helpers;
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
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers.Reports
{
    [AllowAnonymous]
    public class CashBalanceatCloseController : ReportsSmartController
    {
       
        public CashBalanceatCloseController(IReports repo, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup, IUserRepository userrepo)
            :base(repo, repoterminal, repopartner, repogroup,userrepo)
        {           
          
          
        }
        //Esta funcion debe llamarse igual que el nombre del reporte , sin espacios en blanco
        public async Task<ActionResult> CashBalanceatClose()
        {
          
            CashBalanceatCloseViewModel vmodel = new CashBalanceatCloseViewModel();
            TempData["Chart"] = null;
            TempData["sub"] = false;
            ViewBag.Culture = CultureInfoSession.Name;
            return View("CashBalanceatClose", vmodel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CashBalanceatClose([Bind(Include = "Partner,PartnerId,Group,GroupId,StartDate")] CashBalanceatCloseViewModel vmodel)
        {
            //bool envio = await RunReport(vmodel,"excel");
            ModelState.Remove("PartnerId");
            ModelState.Remove("GroupId");

            if (ModelState.IsValid)
            {
               
                #region Variables Partial
                // TempData["List"] = listaux.Count() > 0 ? Utils.ToDataTable<CashBalanceAtCloseTableVM>(listaux) : null;
                Tuple<IEnumerable, Type> alist = await GetList(vmodel);
                TempData["List"] = alist.Item1.Cast<Object>().Count() > 0 ? Utils.ToDataTable(alist.Item1, alist.Item2) : null;

                 TempData["filename"] = "CashManagement";
                TempData["Chart"] = null;
                TempData["partner"] = vmodel.Partner;
                TempData["sub"] = false;
                #endregion

           
                return View("CashBalanceatClose");
            }

            return RedirectToAction("Index");
        }

        public override async Task<Tuple<IEnumerable, Type>> GetList(object aviewmodel)
        {

            return await Task<Tuple<IEnumerable, Type>>.Run( async ()=> {


                CashBalanceatCloseViewModel vmodel = aviewmodel as CashBalanceatCloseViewModel;

               // IEnumerable alist;
                Type type = typeof(CashBalanceAtCloseTableVM);

                List<CashBalanceAtCloseTableVM> listaux = new List<CashBalanceAtCloseTableVM>();
                List<JsonCashBalanceClose> list = new List<JsonCashBalanceClose>();
                ApiATM api = new ApiATM();
                string[] listtn = ListTerminalByGroup(vmodel.GroupId);
                DateTime? start = DateTime.ParseExact(vmodel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                list = await api.CashBalanceClose(start, listtn);
                int partnerid = 1; //Session["partnerId"]
                IEnumerable<dynamic> listTn = repo_terminal.CashBalanceClose(list, vmodel.PartnerId, partnerid);
                if (listTn.Count() > 0)
                {
                    foreach (var item in listTn)
                    {
                        int? cashBalance = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.CashBalance).FirstOrDefault();
                        string time = list.Where(m => m.TerminalId == item.TerminalId).Select(m => m.Time).FirstOrDefault();
                        CashBalanceAtCloseTableVM obj = new CashBalanceAtCloseTableVM(item.TerminalId, item.LocationName, time, cashBalance.ToString());
                        listaux.Add(obj);

                    }

                }


                return new Tuple<IEnumerable, Type>(listaux, type);

            });

        }

        public async Task<bool> RunReport(CashBalanceatCloseViewModel vmodel,string format)
        {
          
           return await SendReport(vmodel, "CashBalanceatClose", format, "Cash Balance at Close");            
        }

       
    }
}