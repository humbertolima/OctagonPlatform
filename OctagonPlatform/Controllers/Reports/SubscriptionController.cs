using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
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
                    string reportname = "";
                    if (item.ReportFilters != null)
                     reportname = item.ReportFilters.First().Report.Name;
                    SubsTableViewModel obj = new SubsTableViewModel()
                    {
                        ReportName = reportname,
                        Description = item.Description,
                        ScheduleName = item.Schedule.Name,
                        Username = item2.Username,
                        NextRunDate = NextRunDate(schedule),
                        LastRunDate = "Tomar la ejecucion real",//LastRunDate(schedule),
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
            DateTime today = DateTime.Now;
            if (schedule is ScheduleOnce)
            {
                datestart += " " + ((ScheduleOnce)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                    daterun = "One execution";
            }
            if (schedule is ScheduleDaily)
            {
                datestart += " " + ((ScheduleDaily)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {                    
                    string nextday = today.AddDays(((ScheduleDaily)schedule).RepeatOn).ToShortDateString();
                    daterun = nextday +" " + ((ScheduleDaily)schedule).Time; 
                }
            }
            if (schedule is ScheduleWeekly)
            {
                datestart += " " + ((ScheduleWeekly)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    ScheduleWeekly week = ((ScheduleWeekly)schedule);
                    string[] days = week.RepeatOnDaysWeeks.Split('_');
                    int days_week = week.RepeatOnWeeks * 7;
                    DateTime nextweek = today.AddDays(days_week);
                    DateTime first_date_week = Utils.GetFirstDayOfWeek(nextweek);
                    DateTime nextrun = GetNextRun(first_date_week, days[0]);                 
                    daterun = nextrun.ToShortDateString() + " " + ((ScheduleWeekly)schedule).Time;
                   
                }
            }
            if (schedule is ScheduleMonthly)
            {
                datestart += " " + ((ScheduleMonthly)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    ScheduleMonthly month = ((ScheduleMonthly)schedule);
                    int day = month.RepeatOnDay;
                    int every_month = month.RepeatOnMonth;

                    DateTime next_month = today.AddMonths(every_month);

                    daterun = next_month.Month + "/" + day + "/" + next_month.Year + " " + ((ScheduleMonthly)schedule).Time;

                }
            }
            return daterun;
        }

        private DateTime GetNextRun(DateTime first_date_week, string dayrun)
        {
            string day = first_date_week.DayOfWeek.ToString().Substring(0, 3);
            if (day == dayrun)
                return first_date_week;
            else
            {               
                DateTime next = first_date_week.AddDays(1);
               return GetNextRun( next,  dayrun);                
            }
           
        }

        //arreglar esta funcion para que tome el tiempo real de ejecucion de la subscription
        private string LastRunDate(Schedule schedule)
        {
            string datestart = schedule.StartDate.ToShortDateString();
            string daterun = "";
            DateTime today = DateTime.Now;
            if (schedule is ScheduleOnce)
            {
                datestart += " " + ((ScheduleOnce)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt < DateTime.Now)
                    daterun = dt.ToString();
                else
                    daterun = "Not execution yet";
            }
            if (schedule is ScheduleDaily)
            {
                datestart += " " + ((ScheduleDaily)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = "Not execution yet";
                else
                {
                    string nextday = today.AddDays(-((ScheduleDaily)schedule).RepeatOn).ToShortDateString();
                    daterun = nextday + " " + ((ScheduleDaily)schedule).Time;
                }
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
                int reportid = Convert.ToInt32(model.GetValue("ReportId").AttemptedValue);
               
                string email = model.GetValue("Email").AttemptedValue;
               
                if (scheduledId > 0 && IsValidEmail(email) && reportid > 0)
                {
                   
                      return  SaveSubscriptionsByUser(model);
                  
                }
                else
                {
                    if (scheduledId <= 0)
                        ModelState.AddModelError("ScheduledId", "Please Select a Schedule");
                    if (!IsValidEmail(email))
                        ModelState.AddModelError("Email", "Please Enter a Valid Email Address");
                    if (reportid <= 0)
                        ModelState.AddModelError("Report", "Please Select a Report");
                }
            }
                     
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);

        }
        private JsonResult SaveSubscriptionsByUser(FormCollection model)
        {
           
            int scheduledId = Convert.ToInt32(model.GetValue("ScheduledId").AttemptedValue);
            string description = model.GetValue("Description").AttemptedValue;
            string email = model.GetValue("Email").AttemptedValue;
            string emailComment = model.GetValue("EmailComment").AttemptedValue;
            int userid = model.GetValue("userId").AttemptedValue == string.Empty ? 0 : Convert.ToInt32(model.GetValue("userId").AttemptedValue);
            string subId = model.GetValue("subId").AttemptedValue;
            int reportId = Convert.ToInt32(model.GetValue("ReportId").AttemptedValue);
            
            try
            {
                if (userid > 0)
                {
                    if (subId == "")
                        FullAddModel(email, description, emailComment, scheduledId, userid, reportId, model);
                    else
                        FullAddModel(email, description, emailComment, scheduledId, userid, reportId, model, Convert.ToInt32(subId));
                }
                else
                {
                    int partnerId = Convert.ToInt32(Session["partnerId"]);
                    IEnumerable<User> listuser = _repoUser.GetAllUsers(partnerId);
                    foreach (var item in listuser)
                    {
                        if (subId == "")
                            FullAddModel(email, description, emailComment, scheduledId, item.Id, reportId, model);
                        else
                            FullAddModel(email, description, emailComment, scheduledId, item.Id, reportId, model, Convert.ToInt32(subId));

                    }
                }
            }
            catch (Exception e)
            {
              
                return Json(new { success = false , errors = e.Message }, JsonRequestBehavior.AllowGet);
            }          

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        //crear el Subscription con los filters correspondientes
          private void FullAddModel(string email, string description, string emailComment, int scheduleId, int userId, int reportId, FormCollection model,int subId = 0)
        {

            SubscriptionModel submodel = null;
            if (subId == 0) //Si es Add
            {
                submodel = new SubscriptionModel(email, description, emailComment, scheduleId, userId);
                _repo.Add(submodel);
            }
            else
            {                
                submodel = Edit(subId, email, description, emailComment, scheduleId);
            }
            bool entro = false;
            for (int i = 0; i < model.Count; i++)
            {
                string key = model.GetKey(i).ToString();
                FilterModel f = _repoFilter.FindAllBy(p => p.Name == key).FirstOrDefault();
                if (f != null && model.GetValue(key).AttemptedValue != string.Empty && model.GetValue(key).AttemptedValue != "-1")
                {
                    //Si subId == 0 adiciono Report Filter
                    if (subId == 0)
                    {
                        AddReportFilter(f.Id, reportId, submodel.Id, model.GetValue(key).AttemptedValue);                       
                    }
                    else
                    {
                        ReportFilter rfilter = _repoReportfilter.FindAllBy(p => p.SubscriptionID == subId && p.ReportID == reportId && p.FilterID == f.Id).FirstOrDefault();
                        if (rfilter != null)
                        {
                            rfilter.Value = model.GetValue(key).AttemptedValue;
                            _repoReportfilter.Edit(rfilter); //Si subId > 0 edit Report Filter
                        }
                        else
                        {//Si rfilter == null , significa que ese subscription no tenia ese filter y en la modificacion se creo nuevo,entonces se adiciona
                            AddReportFilter(f.Id, reportId, submodel.Id, model.GetValue(key).AttemptedValue);
                        }
                    }
                    entro = true;

                }
                else
                {//Si El subscription tenia un filter, pero en la modificacion se lo quitaron ,entonces se elimina
                    if (f != null && subId > 0)
                    {
                        ReportFilter rfilter = _repoReportfilter.FindAllBy(p => p.SubscriptionID == subId && p.ReportID == reportId && p.FilterID == f.Id).FirstOrDefault();
                        _repoReportfilter.DeleteReportFilter(rfilter);
                    }
                }

            }
            if (!entro && subId == 0)
            {//Si El report no tiene ningun filter se adiciona el filter ALL
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

        private void AddReportFilter(int id1, int reportId, int id2, string attemptedValue)
        {
            ReportFilter reportfilter = new ReportFilter()
            {
                FilterID = id1,
                ReportID = reportId,
                SubscriptionID = id2,
                Value = attemptedValue
            };
            _repoReportfilter.Add(reportfilter);
        }

        private SubscriptionModel Edit(int subId,string email,string description,string emailComment,int scheduleId)
        {
            SubscriptionModel model = _repo.GetSubscriptionById(subId);
            model.Email = email;
            model.Description = description;
            model.EmailComment = emailComment;
            model.ScheduleId = scheduleId;
            _repo.Edit(model);
            return model;
        }
        private bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }

        // GET: SubscriptionModels/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                return null;
            }
            int id1 = id ?? 0;
            SubscriptionModel subscriptionModel = _repo.GetSubscriptionById(id1);
            if (subscriptionModel == null)
            {
                return null;
            }
            
            return PartialView("Table", GetModelEdit(subscriptionModel));
        }

        private SubscriptionVM GetModelEdit(SubscriptionModel subscriptionModel)
        {

            IEnumerable<Subreport> subscriptions = new List<Subreport>();
            int partnerId = Convert.ToInt32(Session["partnerId"]);
            int userId = subscriptionModel.User.Id;
            string usern = "";
            IEnumerable<ReportModel> listrepost = _repoReport.All();
            
                User user = subscriptionModel.User;
                subscriptions = _repo.GetSubscriptionsIncluding(userId);
                string bussinesname = "";
                if (user.Partner != null)
                    bussinesname = user.Partner.BusinessName;
                usern = user.UserName + " - " + user.Name + " - " + bussinesname;
            // listrepost = user.Reports.ToList();
            ReportModel report = subscriptionModel.ReportFilters.First().Report;

            SubscriptionVM vmodel = new SubscriptionVM()
            {
                List = ProcessSubscription(subscriptions),
                User = usern,
                UserId = userId,
                ReportId = new SelectList(listrepost, "Id", "Name", report.Id)
            };

            IEnumerable<Schedule> listscheduled =  _repoScheduled.GetScheduleByUser(userId) ;
            vmodel.ScheduledId = new SelectList(listscheduled, "Id", "Name", subscriptionModel.ScheduleId);
            vmodel.Description = subscriptionModel.Description;
            vmodel.Email = subscriptionModel.Email;
            vmodel.EmailComment = subscriptionModel.EmailComment;
            return vmodel;
        }

        // POST: SubscriptionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        

        public PartialViewResult FilterReportEdit( string idsub)
        {
            ReportModel report = null;
            int subId = Convert.ToInt32(idsub);
            SubscriptionModel subscriptionModel = _repo.GetSubscriptionById(subId);
            if (subscriptionModel.ReportFilters != null)
            {
                report = subscriptionModel.ReportFilters.First().Report;
            }

            string name = report.Name.Replace(" ", string.Empty);
            Type type = Type.GetType("OctagonPlatform.Models.FormsViewModels." + name + "ViewModel");
            object handle = Activator.CreateInstance(type);

           
            List<int> days = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                days.Add(i);
            }
            IEnumerable<SelectListItem> selectList = new SelectList(days);
          //  UpdateSubReportFilter(subscriptionModel.ReportFilters, handle,days);
            //Llenar el viewmodel
            foreach (var item in subscriptionModel.ReportFilters)
            {
                if (item.Report.Name != "ALL")
                {
                    FilterModel f = item.Filter;
                    PropertyInfo[] collection = handle.GetType().GetProperties();

                    foreach (var prop in collection)
                    {
                        var value = item.Value;
                        if (prop.Name == f.Name)
                        {
                            switch (f.Name)
                            {
                                case "Status":
                                    int status = Convert.ToInt32(value);
                                    StatusType.Status sta = (StatusType.Status)status;
                                    handle.GetType().GetProperty(prop.Name).SetValue(handle, sta, null);
                                    break;
                                case "StartDate":
                                    int val = Convert.ToInt32(value);
                                    var aList2 = days.Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (i == val) });
                                    selectList = aList2;  // new SelectList(aList2);
                                    break;
                                default:
                                    handle.GetType().GetProperty(prop.Name).SetValue(handle, item.Value, null);
                                    break;
                            }     

                            //handle.GetType().GetProperty(prop.Name).SetValue(handle, item.Value, null);
                        }
                    }
                }
            }
            //-----//
            TempData["StartDate"] = selectList;
            TempData["Sub"] = true;
            return PartialView("../ReportsSmart/" + name + "/" + "_PartialForm", handle);
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
