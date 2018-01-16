using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ReportsController(IReports repo, IUserRepository repoUser)
        {
            _repo = repo;
            _repoUser = repoUser;
        }
        // GET: reportModels
        public ActionResult Index()
        {
          
            return View(_repo.All());
        }      

        // GET: reportModels/Create
        public ActionResult Create()
        {
            return View();
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
     

    }
}
