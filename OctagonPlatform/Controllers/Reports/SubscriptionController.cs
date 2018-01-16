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

        public ActionResult Index(string a,string b)
        {
            if (a == "1")
                TempData["Success"] = "Report Subscription Created Successful";
            //int partnerId = Convert.ToInt32(Session["partnerId"]);
            int userId = -1;
            if (b == null)
                userId = Convert.ToInt32(Session["UserId"]);
            else
            if (b == "")
                userId = 0;
            else 
                userId = Convert.ToInt32(b); 
            return View(GetModel( userId));
        }
        public PartialViewResult List(string userId)
        {
              int id = Convert.ToInt32(userId);
            return PartialView("Table", GetModel(id));
        }
        private SubscriptionVM GetModel(int userId)
        {

            IEnumerable<Subreport> subscriptions = new List<Subreport>();
            int partnerId = Convert.ToInt32(Session["partnerId"]);
           
            string usern = "";
            IEnumerable<ReportModel> listrepost = _repoReport.All();
            if (userId > 0)
            {
                User user = _repoUser.GetReportsUser(userId);
                subscriptions = _repo.GetSubscriptionsIncluding(userId);
                string bussinesname = "";
                if (user.Partner != null)
                    bussinesname = user.Partner.BusinessName;
                usern = user.UserName + " - " + user.Name + " - " + bussinesname;
               // listrepost = user.Reports.ToList();
            }
            else
            {
                usern = userId == 0 ? "" : Session["userName"] + " - " + Session["Name"] + " - " + Session["businessName"].ToString();
               
                subscriptions = _repo.GetSubscriptionsParent(partnerId);
            }
              
            SubscriptionVM vmodel = new SubscriptionVM()
            {
                List = ProcessSubscription(subscriptions),
                User = usern,
                UserId = userId,
                ReportId = new SelectList(listrepost, "Id", "Name")
            };

            IEnumerable<Schedule> listscheduled = userId > 0 ?_repoScheduled.GetScheduleByUser(userId):_repoScheduled.GetScheduleByParent(partnerId);
            vmodel.ScheduledId = new SelectList(listscheduled, "Id", "Name");
            return vmodel;
        }

        private IEnumerable< ReportModel> populate(User item)
        {
            foreach (ReportModel rm in item.Reports)
                yield return rm;
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
        public JsonResult Create(FormCollection model)
        {
            if (ModelState.IsValid)
            {
                int scheduledId = Convert.ToInt32(model.GetValue("ScheduledId").AttemptedValue);              
                string email = model.GetValue("Email").AttemptedValue;
               
                if (scheduledId > 0 && IsValidEmail(email))
                {

                  return  AddSubscriptionsByUser(model);
                }
                else
                {
                    if (scheduledId <= 0)
                        ModelState.AddModelError("ScheduledId", "Please Select a Schedule");
                    if (!IsValidEmail(email))
                        ModelState.AddModelError("Email", "Please Enter a Valid Email Address");
                }
            }
            /* string messages = string.Join("<br> ", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));*/
           
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);

          //  return PartialView("../Error/_PartialAlert");
            // return "-1";
        }
        private JsonResult AddSubscriptionsByUser(FormCollection model)
        {
           
            int scheduledId = Convert.ToInt32(model.GetValue("ScheduledId").AttemptedValue);
            string description = model.GetValue("Description").AttemptedValue;
            string email = model.GetValue("Email").AttemptedValue;
            string emailComment = model.GetValue("EmailComment").AttemptedValue;
            int userid = model.GetValue("userId").AttemptedValue == string.Empty ? 0 : Convert.ToInt32(model.GetValue("userId").AttemptedValue);
           
            int reportId = Convert.ToInt32(model.GetValue("ReportId").AttemptedValue);
            
            try
            {
                if (userid > 0)
                {

                    FullAddModel(email, description, emailComment, scheduledId, userid, reportId, model);

                }
                else
                {
                    int partnerId = Convert.ToInt32(Session["partnerId"]);
                    IEnumerable<User> listuser = _repoUser.GetAllUsers(partnerId);
                    foreach (var item in listuser)
                    {
                        FullAddModel(email, description, emailComment, scheduledId, item.Id, reportId, model);
                       
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }          

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        private void FullAddModel(string email, string description, string emailComment, int scheduleId, int userId, int reportId, FormCollection model)
        {
            SubscriptionModel submodel = new SubscriptionModel( email,  description,  emailComment,  scheduleId,  userId); 
            _repo.Add(submodel);
            bool entro = false;
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
                    entro = true;

                }

            }
            if (!entro)
            {
                FilterModel f = _repoFilter.FindAllBy(p => p.Name == "ALL").FirstOrDefault();
                ReportFilter reportfilter = new ReportFilter()
                {
                    FilterID = f.Id,
                    ReportID = reportId,
                    SubscriptionID = submodel.Id,
                    Value = "ALL"
                };
                _repoReportfilter.Add(reportfilter);
            }

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
            int userid = _repo.FindBy(id).UserId;
            _repo.Delete(id);
           
            return PartialView("Table", GetModel(userid));
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
