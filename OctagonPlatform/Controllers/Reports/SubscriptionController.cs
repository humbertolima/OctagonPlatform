using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;

namespace OctagonPlatform.Controllers.Reports
{
    public class SubscriptionController : Controller
    {
        private ISubscription _repo;
        private ISchedule _repoScheduled;
        private IReports _repoReport;
        private IFilter _repoFilter;
        public SubscriptionController(ISubscription repo, ISchedule reposchedule, IReports repoReport, IFilter repoFilter)
        {
            _repo = repo;
            _repoScheduled = reposchedule;
            _repoReport = repoReport;
            _repoFilter = repoFilter;
        }
        // GET: SubscriptionModels
        public ActionResult Index()
        {
            int partnerId = Convert.ToInt32(Session["partnerId"]);
            int userId = Convert.ToInt32(Session["UserId"]);
            var subscriptions = _repo.FindAllBy(s => s.PartnerId == partnerId);
            IEnumerable<ReportModel> listrepost = _repoReport.GetAllReports(partnerId, userId);
            SubscriptionVM vmodel = new SubscriptionVM()
            {
                List = subscriptions,
                User = Session["userName"] + " - " + Session["Name"] + " - " + Session["businessName"].ToString(),
                UserId = userId,
                ReportId = new SelectList(listrepost, "Id", "Name")
            };
           
            IEnumerable<Schedule> listscheduled = _repoScheduled.GetScheduleByUser(userId, partnerId);
            vmodel.ScheduledId = new SelectList(listscheduled,"Id","Name");
            return View(vmodel);
        }

        // GET: SubscriptionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = _repo.FindBy(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionModel);
        }

        // GET: SubscriptionModels/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SubscriptionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]       
        public ActionResult Create(FormCollection model)
        {
            if (ModelState.IsValid)
            {
                int reportId = Convert.ToInt32(model.GetValue("ReportId").ToString());
              
                // _repo.Add(subscriptionModel);
                
               // dynamic foo = JObject.Parse(model);
                return RedirectToAction("Index");
            }


            return View(model);
        }

        // GET: SubscriptionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = _repo.FindBy(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }

            return View(subscriptionModel);
        }

        // POST: SubscriptionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Description,EmailComment,ScheduledId,PartnerId")] SubscriptionModel subscriptionModel)
        {
            if (ModelState.IsValid)
            {

                _repo.Edit(subscriptionModel);
                return RedirectToAction("Index");
            }
            return View(subscriptionModel);
        }

        // GET: SubscriptionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionModel subscriptionModel = _repo.FindBy(id);
            if (subscriptionModel == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionModel);
        }

        // POST: SubscriptionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubscriptionModel subscriptionModel = _repo.FindBy(id);
            _repo.Delete(subscriptionModel);

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
        public PartialViewResult FilterReport(string id)
        {
            ReportModel report = _repoReport.FindBy(Convert.ToInt32(id));
            string name = report.Name.Replace(" ", string.Empty);
            
            object handle = Activator.CreateInstance(Type.GetType("OctagonPlatform.Models.FormsViewModels." + name + "ViewModel"));
            List<int> days = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                days.Add(i);
            }
           
            TempData["StartDate"] = new SelectList(days);
            TempData["Sub"] = true;
            return PartialView("../ReportsSmart/"+name + "/" + "_PartialForm", handle);
         
        }
    }
}
