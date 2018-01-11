using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;

namespace OctagonPlatform.Controllers.Reports
{
    public class SubscriptionController : Controller
    {
        private ISubscription _repo;
        private ISchedule _repoScheduled;
        private IReports _repoReport;
        private IFilter _repoFilter;
        private IUserRepository _repoUser;
        private IReportFilter _repoReportfilter;
        public SubscriptionController(ISubscription repo, ISchedule reposchedule, IReports repoReport, IFilter repoFilter, IUserRepository repoUser, IReportFilter repoReportfilter)
        {
            _repo = repo;
            _repoScheduled = reposchedule;
            _repoReport = repoReport;
            _repoFilter = repoFilter;
            _repoUser = repoUser;
            _repoReportfilter = repoReportfilter;
        }
        // GET: SubscriptionModels

        public ActionResult Index()
        {           
            return View(GetModel());
        }
        private SubscriptionVM GetModel()
        {
            int partnerId = Convert.ToInt32(Session["partnerId"]);
            int userId = Convert.ToInt32(Session["UserId"]);
            IEnumerable<Subreport> subscriptions = _repo.GetSubscriptionsIncluding(userId);

            IEnumerable<ReportModel> listrepost = _repoReport.GetAllReports(partnerId, userId);
            SubscriptionVM vmodel = new SubscriptionVM()
            {
                List = ProcessSubscription(subscriptions),
                User = Session["userName"] + " - " + Session["Name"] + " - " + Session["businessName"].ToString(),
                UserId = userId,
                ReportId = new SelectList(listrepost, "Id", "Name")
            };

            IEnumerable<Schedule> listscheduled = _repoScheduled.GetScheduleByUser(userId, partnerId);
            vmodel.ScheduledId = new SelectList(listscheduled, "Id", "Name");
            return vmodel;
        }
        private List<SubsTableViewModel> ProcessSubscription(IEnumerable<Subreport> list)
        {
            List<SubsTableViewModel> aux = new List<SubsTableViewModel>();
            foreach (var item2 in list)
            {
                var item = item2.Model;
                if (item.ReportFilters != null && item.Schedule !=null)
                {
                    Schedule schedule = item.Schedule;
                    string reportname = item.ReportFilters.First().Report.Name;
                    SubsTableViewModel obj = new SubsTableViewModel()
                    {
                        ReportName = reportname,
                        Description = item.Description,
                        ScheduleName = item.Schedule.Name,
                        Username = item2.Username,
                        NextRunDate = NextRunDate(schedule),
                        LastRunDate = LastRunDate(schedule),
                        Id = item.Id
                    };                   
                    aux.Add(obj);     

                }

            }
            return aux;
        }

        private string NextRunDate(Schedule schedule)
        {
            string datestart = schedule.StartDate.ToShortDateString();
            string daterun = "";
            if (schedule is ScheduleOnce)
            {
                datestart += " " + ((ScheduleOnce)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                    daterun = "One execution";
            }
            return daterun;
        }
        private string LastRunDate(Schedule schedule)
        {
            string datestart = schedule.StartDate.ToShortDateString();
            string daterun = "";
            if (schedule is ScheduleOnce)
            {
                datestart += " " + ((ScheduleOnce)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt < DateTime.Now)
                    daterun = dt.ToString();
                else
                    daterun = "Not execution yet";
            }
            return daterun;
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

                int reportId = Convert.ToInt32(model.GetValue("ReportId").AttemptedValue);
                int scheduledId = Convert.ToInt32(model.GetValue("ScheduledId").AttemptedValue);
                string description = model.GetValue("Description").AttemptedValue;
                string email = model.GetValue("Email").AttemptedValue;
                string emailComment = model.GetValue("EmailComment").AttemptedValue;
                int userid = Convert.ToInt32(model.GetValue("userId").AttemptedValue);
                int partnerid = _repoUser.FindBy(userid).PartnerId;
                if (scheduledId > 0 && IsValidEmail(email))
                {
                    SubscriptionModel submodel = new SubscriptionModel();
                    submodel.Description = description;
                    submodel.Email = email;
                    submodel.EmailComment = emailComment;
                    submodel.UserId = userid;
                    submodel.ScheduleId = scheduledId;
                    _repo.Add(submodel);
                    for (int i = 0; i < model.Count; i++)
                    {
                        string key = model.GetKey(i).ToString();
                        FilterModel f = _repoFilter.FindAllBy(p => p.Name == key).FirstOrDefault();
                        if (f != null && model.GetValue(key).AttemptedValue != string.Empty && model.GetValue(key).AttemptedValue != "-1")
                        {
                            ReportFilter reportfilter = new ReportFilter()
                            {
                                FilterID = f.Id,
                                ReportID = reportId,
                                SubscriptionID = submodel.Id,
                                Value = model.GetValue(key).AttemptedValue
                            };
                            _repoReportfilter.Add(reportfilter);

                        }

                    }

                    TempData["Success"] = "Report Subscription Created Successful";
                    return PartialView("../Error/_PartialAlert");
                }
                else
                {
                    if (scheduledId <= 0)
                        ModelState.AddModelError("ScheduledId", "Please Select a Schedule");
                    if (!IsValidEmail(email))
                        ModelState.AddModelError("ScheduledId", "Please Enter a Valid Email Address");
                }
            }
            string messages = string.Join("<br> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            TempData["Danger"] = messages;
            return PartialView("../Error/_PartialAlert");
            // return "-1";
        }

        private bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
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
       // [ValidateAntiForgeryToken]
        public PartialViewResult DeleteConfirmed(int id, SubscriptionVM model)
        {           
            _repo.Delete(id);
            return PartialView("Table", GetModel());
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
            return PartialView("../ReportsSmart/" + name + "/" + "_PartialForm", handle);

        }
    }
}
