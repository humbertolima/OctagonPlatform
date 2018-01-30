using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers.Reports
{
    [AllowAnonymous]
    public class ReportsController : Controller
    {
        
        private IReports _repo;
        private IUserRepository _repoUser;
        protected ITerminalRepository repo_terminal;
        protected IPartnerRepository repo_partner;
        protected IReportGroup repo_group;
        public ReportsController(IReports repo, IUserRepository repoUser, ITerminalRepository repoterminal, IPartnerRepository repopartner, IReportGroup repogroup)
        {
            _repo = repo;
            _repoUser = repoUser;
            repo_terminal = repoterminal;
            repo_partner = repopartner;
            repo_group = repogroup;
        }
        // GET: reportModels
        public ActionResult Index()
        {
          
            return View(_repo.All());
        }
        public ActionResult Selection()
        {
            var listprueba = _repoUser.GetAllUsersSubscription();
         
            IEnumerable<ReportModel> list = _repo.GetReportsDasboard();
            return View("../ReportsSmart/Index",list);
        }
        // GET: reportModels/Create
        public ActionResult Create()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> trash()
        {

            Controllers.Reports.ReportsSmartController ctrl = DependencyResolver.Current.GetService(typeof(Controllers.Reports.CashBalanceatCloseController)) as Controllers.Reports.CashBalanceatCloseController;
            
            System.Web.Routing.RouteData route = new System.Web.Routing.RouteData();
            route.Values.Add("action", "trash56");
            route.Values.Add("controller", "ReportsCACA");

            ctrl.ControllerContext = new ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, ctrl);// this.ControllerContext;

            await ctrl.RunReport(new Models.FormsViewModels.CashBalanceatCloseViewModel() { }, "pdf");

            return new ContentResult();
        }

        // POST: reportModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ReportModel reportModel)
        {
            if (ModelState.IsValid)
            {
                
                 reportModel.IsShowDashboard = false;
                _repo.Add(reportModel);               
                return RedirectToAction("Index");
            }

            return View(reportModel);
        }

        // GET: reportModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportModel reportModel = _repo.FindBy(id);
            if (reportModel == null)
            {
                return HttpNotFound();
            }
            return View(reportModel);
        }

        // POST: reportModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsShowDashboard")] ReportModel reportModel)
        {
            if (ModelState.IsValid)
            {
                 reportModel.IsShowDashboard = false;
                _repo.Edit(reportModel);
                return RedirectToAction("Index");
            }
            return View(reportModel);
        }

        // GET: reportModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportModel reportModel = _repo.FindBy(id);
            if (reportModel == null)
            {
                return HttpNotFound();
            }
            return View(reportModel);
        }

        // POST: reportModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportModel reportModel = _repo.FindBy(id);
            _repo.Delete(reportModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Personalizar reports del dashboard
        private IList<SelectListItem> GetReports()
        {
            IEnumerable<ReportModel> list = _repo.All();
            List<SelectListItem> listitem = new List<SelectListItem>();
            foreach (var item in list)
            {
                listitem.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(),Selected = item.IsShowDashboard });              
            }
            return listitem;
        }
        public ActionResult CheckReports()
        {
            ReportsViewModel model = new ReportsViewModel
            {
                AvailableReports = GetReports()
            };
            Session["businessName"] = "";
            return View("RepoDashboard",model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckReports(ReportsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fruits = string.Join(",", model.SelectedReports);
                ReportModel reportModel = null;
                IEnumerable<ReportModel> list = _repo.All();
                foreach (var item in model.SelectedReports)
                {
                         reportModel = _repo.FindBy(Convert.ToInt32(item));
                         reportModel.IsShowDashboard = true;
                        _repo.Edit(reportModel);                   
                   
                }
                foreach (var item in list)
                {
                    if (!model.SelectedReports.Contains(item.Id.ToString()))
                    {
                        item.IsShowDashboard = false;
                        _repo.Edit(item);
                    }
                }

                // Save data to database, and redirect to Success page.
                TempData["Success"] = "Reports save successfull.";
              //  ViewBag.Success = true;
              //  ViewBag.Message = "Reports save successfull.";
                return RedirectToAction("CheckReports");
            }
            TempData["Danger"] = "Reports not save , contains error.";
           
            return RedirectToAction("CheckReports");
        }

        public ActionResult AutoTerminal(string term)
        {

            IEnumerable<string> list = repo_terminal.GetAllTerminalId(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoPartner(string term)
        {

            IEnumerable<dynamic> list = repo_partner.GetAllPartner(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoGroup(string term)
        {

            IEnumerable<dynamic> list = repo_group.GetAllGroup(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string html, string filename, string orientation)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                iTextSharp.text.Rectangle pageSize = orientation == "Landscape" ? PageSize.LETTER.Rotate() : PageSize.LETTER;
                StringReader sr = new StringReader(html);
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(pageSize, 20f, 20f, 20f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", filename);
            }
        }



    }
}
