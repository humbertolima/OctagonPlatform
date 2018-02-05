using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.PersistanceRepository;
using OctagonPlatform.Views.Schedule;

namespace OctagonPlatform.Controllers.Reports
{
    public class ScheduleController : Controller
    {
        private ISchedule _repo;
        private readonly IUserRepository _userRepository;
        private ISubscription _repoSub;
        private IReportFilter _repofilter;
        public ScheduleController(ISchedule repo, IUserRepository userRepository, ISubscription repoSub, IReportFilter repofilter)
        {
            _repo = repo;
            _userRepository = userRepository;
            _repoSub = repoSub;
            _repofilter = repofilter;
        }

        // GET: ScheduleOnces
        public ActionResult Index()
        {
            
            ScheduleVM vmodel = new ScheduleVM();
            vmodel.List = _repo.GetScheduleByUser(Convert.ToInt32(Session["UserId"]));
            vmodel.User = Session["userName"]+" - "+ Session["Name"] +" - "+ Session["businessName"].ToString();
            vmodel.UserId =Convert.ToInt32( Session["UserId"]);
            return View(vmodel);
        }
        // GET: ScheduleOnces/Create
        public PartialViewResult List(string userId)
        {
            IEnumerable<Schedule> List = null;
            if (Convert.ToInt32(userId) > 0)
            {
                User user = _userRepository.FindBy(Convert.ToInt32(userId));
                List = _repo.GetScheduleByUser(user.Id);
            }
            else
                List = _repo.All();
            return PartialView("List",List);
        }
        // GET: ScheduleOnces/Create
        public PartialViewResult Create(string userId)
        {
            //User user = _userRepository.FindBy(Convert.ToInt32(userId));
            //DateTime time = Utils.ToTimeZoneTime(DateTime.Now, user.TimeZoneInfo);

            ScheduleViewModel vmodel = new ScheduleViewModel();
            vmodel.UserId = userId;
            return PartialView(vmodel);
        }

        // POST: ScheduleOnces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Repeats,RepeatsSelected,StartDate,Time,RepeatOn,StopDate,RepeatOnWeeks,RepeatOnDaysWeeks,RepeatOnDay,RepeatOnMonth,RepeatOnFirst,RepeatOnDay2,RepeatOnMonth2")] ScheduleViewModel vmodel)
        {
            if(vmodel.RepeatsSelected != -1)
                ModelState.Remove("Repeats");
            if (ModelState.IsValid)
            {
                Schedule model = null;
                if (vmodel.Repeats == ScheduleType.RepeatsEnum.Once)
                {
                    model = new ScheduleOnce(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time);
                }
                else
                {
                    if (vmodel.Repeats == ScheduleType.RepeatsEnum.Daily)
                    {
                        model = new ScheduleDaily(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time,vmodel.RepeatOn,vmodel.StopDate);
                    }
                    else
                    {
                        if (vmodel.Repeats == ScheduleType.RepeatsEnum.Weekly)
                        {
                            string days = vmodel.RepeatOnDaysWeeks == null ? "Mon" : vmodel.RepeatOnDaysWeeks;
                            model = new ScheduleWeekly(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnWeeks, days, vmodel.StopDate);
                        }
                        else
                        {
                            if (vmodel.Repeats == ScheduleType.RepeatsEnum.Monthly)
                            {
                                model = new ScheduleMonthly(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnDay, vmodel.RepeatOnMonth, vmodel.StopDate);
                            }
                            else
                            {
                                if (vmodel.Repeats == ScheduleType.RepeatsEnum.MonthlyRelative)
                                {
                                    model = new ScheduleMonthlyRelative(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnFirst, vmodel.RepeatOnDay2, vmodel.RepeatOnMonth2, vmodel.StopDate);
                                }
                            }
                        }
                    }
                }
                User user =_userRepository.FindBy(Convert.ToInt32(vmodel.UserId));
                model.UserId = user.Id;
                _repo.Add(model);               
                return RedirectToAction("Index");
            }

            return View(vmodel);
        }

        // GET: ScheduleOnces/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            Schedule schedule = _repo.FindBy(id);
            ScheduleViewModel vmodel = new ScheduleViewModel();
            vmodel.Name = schedule.Name;
            vmodel.Repeats = schedule.Repeats;
            vmodel.StartDate = schedule.StartDate;
            if (schedule == null)
            {
                return PartialView();
            }
               
            if (schedule.GetType().BaseType.FullName == typeof(ScheduleOnce).FullName)
            {
                vmodel.Time = ((ScheduleOnce)schedule).Time;
            }
            if (schedule.GetType().BaseType.FullName == typeof(ScheduleDaily).FullName)
            {
               vmodel.RepeatOn = ((ScheduleDaily)schedule).RepeatOn;
               vmodel.StopDate = ((ScheduleDaily)schedule).StopDate;
               vmodel.Time = ((ScheduleDaily)schedule).Time;
            }
            if (schedule.GetType().BaseType.FullName == typeof(ScheduleWeekly).FullName)
            {
                vmodel.RepeatOnWeeks = ((ScheduleWeekly)schedule).RepeatOnWeeks;
                vmodel.RepeatOnDaysWeeks = ((ScheduleWeekly)schedule).RepeatOnDaysWeeks;
                vmodel.Time = ((ScheduleWeekly)schedule).Time;
            }
            if (schedule.GetType().BaseType.FullName == typeof(ScheduleMonthly).FullName)
            {
                vmodel.RepeatOnDay = ((ScheduleMonthly)schedule).RepeatOnDay;
                vmodel.RepeatOnMonth = ((ScheduleMonthly)schedule).RepeatOnMonth;
                vmodel.Time = ((ScheduleMonthly)schedule).Time;
            }
            if (schedule.GetType().BaseType.FullName == typeof(ScheduleMonthlyRelative).FullName)
            {
                vmodel.RepeatOnFirst = ((ScheduleMonthlyRelative)schedule).RepeatOnFirst;
                vmodel.RepeatOnDay2 = ((ScheduleMonthlyRelative)schedule).RepeatOnDay;
                vmodel.RepeatOnMonth2 = ((ScheduleMonthlyRelative)schedule).RepeatOnMonth;
                vmodel.Time = ((ScheduleMonthlyRelative)schedule).Time;
            }
            return PartialView(vmodel);
        }
        public string Details(string id)
        {
            Schedule schedule = _repo.FindBy(Convert.ToInt32(id));
            string data = "";
            if (schedule is ScheduleDaily)
            { ScheduleDaily model = ((ScheduleDaily)schedule);
               
                data += "<p>Repeats:"+ model.Repeats + "</p>";
                data += "<p>Recurrence: Every " + model.RepeatOn + " day(s)</p>";
                data += "<p>Time:  " + model.Time + "</p>";
                data += "<p>Start Date:  " + model.StartDate + "</p>";
                data += "<p>Stop Date:  " + model.StopDate + "</p>";                
            }
            if (schedule is ScheduleOnce)
            {
                ScheduleOnce model = ((ScheduleOnce)schedule);

                data += "<p>Repeats:" + model.Repeats + "</p>";
                data += "<p>Recurrence: One Time</p>";
                data += "<p>Time:  " + model.Time + "</p>";
                data += "<p>Start Date:  " + model.StartDate + "</p>";
               
            }
            if (schedule is ScheduleWeekly)
            {
                ScheduleWeekly model = ((ScheduleWeekly)schedule);

                data += "<p>Repeats:" + model.Repeats + "</p>";
                data += "<p>Recurrence: Every " + model.RepeatOnWeeks + " weeks(s) on "+model.RepeatOnDaysWeeks+"</p>";
                data += "<p>Time:  " + model.Time + "</p>";
                data += "<p>Start Date:  " + model.StartDate + "</p>";
                data += "<p>Stop Date:  " + model.StopDate + "</p>";
            }
            if (schedule is ScheduleMonthly)
            {
                ScheduleMonthly model = ((ScheduleMonthly)schedule);

                data += "<p>Repeats:" + model.Repeats + "</p>";
                data += "<p>Recurrence:  Day " + model.RepeatOnDay + " every " + model.RepeatOnMonth + " month(s)</p>";
                data += "<p>Time:  " + model.Time + "</p>";
                data += "<p>Start Date:  " + model.StartDate + "</p>";
                data += "<p>Stop Date:  " + model.StopDate + "</p>";
            }
            if (schedule is ScheduleMonthlyRelative)
            {
                ScheduleMonthlyRelative model = ((ScheduleMonthlyRelative)schedule);

                data += "<p>Repeats:" + model.Repeats + "</p>";
                data += "<p>Recurrence:  The " + model.RepeatOnFirst+" "+model.RepeatOnDay + " every " + model.RepeatOnMonth + " month(s)</p>";
                data += "<p>Time:  " + model.Time + "</p>";
                data += "<p>Start Date:  " + model.StartDate + "</p>";
                data += "<p>Stop Date:  " + model.StopDate + "</p>";
            }

            return data;
        }
        // POST: ScheduleOnces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Repeats,StartDate,Time,RepeatOn,StopDate,RepeatOnWeeks,RepeatOnDaysWeeks,RepeatOnDay,RepeatOnMonth,RepeatOnFirst,RepeatOnDay2,RepeatOnMonth2")] ScheduleViewModel vmodel)
        {
            if (vmodel.RepeatsSelected != -1)
                ModelState.Remove("Repeats");
            if (ModelState.IsValid)
            {
                Schedule model = _repo.FindBy(vmodel.ID);
                model.Name = vmodel.Name;
                model.Repeats = vmodel.Repeats;
                model.StartDate = vmodel.StartDate;
                if (vmodel.Repeats == ScheduleType.RepeatsEnum.Once)
                {
                    if (!EditScheduleOnce(vmodel, model)) return RedirectToAction("Index");                  
                }
                else
                {
                    if (vmodel.Repeats == ScheduleType.RepeatsEnum.Daily)
                    {
                       if( !EditScheduleDaily(vmodel,model)) return RedirectToAction("Index"); //Si Add en lugar de edit retorna false y se redirecciona
                    }
                    else
                    {
                        if (vmodel.Repeats == ScheduleType.RepeatsEnum.Weekly)
                        {
                            if (!EditScheduleWeekly(vmodel, model)) return RedirectToAction("Index");                           
                        }
                        else
                        {
                            if (vmodel.Repeats == ScheduleType.RepeatsEnum.Monthly)
                            {
                                if (!EditScheduleMonthly(vmodel, model)) return RedirectToAction("Index");                                
                            }
                            else
                            {
                                if (vmodel.Repeats == ScheduleType.RepeatsEnum.MonthlyRelative)
                                {
                                    if (!EditScheduleMonthlyRelative(vmodel, model)) return RedirectToAction("Index");                                                                       
                                }
                            }
                        }
                    }
                }
                _repo.Edit(model);              
            }
            return RedirectToAction("Index");

        }

        private bool EditScheduleMonthlyRelative(ScheduleViewModel vmodel, Schedule model)
        {
            if (model.GetType().BaseType.FullName != typeof(ScheduleMonthlyRelative).FullName)
            {

                ScheduleMonthlyRelative model1 = new ScheduleMonthlyRelative(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnFirst, vmodel.RepeatOnDay2, vmodel.RepeatOnMonth2, vmodel.StopDate);
                IEnumerable<SubscriptionModel> subscriptions = model.Subscriptions;
               
                AddReportFilters(subscriptions, model1, model.UserId, model.ID);                
               
                return false;

            }
            else
            {
                ((ScheduleMonthlyRelative)model).Time = vmodel.Time;
                ((ScheduleMonthlyRelative)model).RepeatOnFirst = vmodel.RepeatOnFirst;
                ((ScheduleMonthlyRelative)model).RepeatOnDay = vmodel.RepeatOnDay2;
                ((ScheduleMonthlyRelative)model).RepeatOnMonth = vmodel.RepeatOnMonth2;
                ((ScheduleMonthlyRelative)model).StopDate = vmodel.StopDate;
            }
            return true;
        }
        //Add item in table reportfilter 
        private void AddReportFilters(IEnumerable<SubscriptionModel> subscriptions, Schedule model1,int UserId,int ID)
        {
            model1.UserId = UserId;
            _repo.Add(model1);
            foreach (var item in subscriptions)
            {
                SubscriptionModel subs = new SubscriptionModel(item.Email, item.Description, item.EmailComment, model1.ID, item.UserId,item.Format);

                _repoSub.Add(subs);
                foreach (var filter in item.ReportFilters)
                {
                    ReportFilter reportfilter = new ReportFilter();
                    reportfilter.FilterID = filter.FilterID;
                    reportfilter.ReportID = filter.ReportID;
                    reportfilter.SubscriptionID = subs.Id;
                    reportfilter.Value = filter.Value;
                    _repofilter.Add(reportfilter);

                }
            }
            _repo.Delete(ID);

        }

        private bool EditScheduleMonthly(ScheduleViewModel vmodel, Schedule model)
        {
            if (model.GetType().BaseType.FullName != typeof(ScheduleMonthly).FullName)
            {
                ScheduleMonthly model1 = new ScheduleMonthly(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnDay, vmodel.RepeatOnMonth, vmodel.StopDate);
                IEnumerable<SubscriptionModel> subscriptions = model.Subscriptions;
                AddReportFilters(subscriptions, model1, model.UserId, model.ID);

                return false;
            }
            else
            {
                ((ScheduleMonthly)model).Time = vmodel.Time;
                ((ScheduleMonthly)model).RepeatOnDay = vmodel.RepeatOnDay;
                ((ScheduleMonthly)model).RepeatOnMonth = vmodel.RepeatOnMonth;
                ((ScheduleMonthly)model).StopDate = vmodel.StopDate;
            }
            return true;
        }

        private bool EditScheduleWeekly(ScheduleViewModel vmodel, Schedule model)
        {
            if (model.GetType().BaseType.FullName != typeof(ScheduleWeekly).FullName)
            {
                ScheduleWeekly model1 = new ScheduleWeekly(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnWeeks, vmodel.RepeatOnDaysWeeks, vmodel.StopDate);

                IEnumerable<SubscriptionModel> subscriptions = model.Subscriptions;
                AddReportFilters(subscriptions, model1, model.UserId, model.ID);
                return false;
            }
            else
            {
                ((ScheduleWeekly)model).Time = vmodel.Time;
                ((ScheduleWeekly)model).RepeatOnWeeks = vmodel.RepeatOnWeeks;
                ((ScheduleWeekly)model).RepeatOnDaysWeeks = vmodel.RepeatOnDaysWeeks;
                ((ScheduleWeekly)model).StopDate = vmodel.StopDate;
            }
            return true;
        }

        private bool EditScheduleOnce(ScheduleViewModel vmodel, Schedule model)
        {
            if (model.GetType().BaseType.FullName != typeof(ScheduleOnce).FullName)
            {
                ScheduleOnce model1 = new ScheduleOnce(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time);

                IEnumerable<SubscriptionModel> subscriptions = model.Subscriptions;
                AddReportFilters(subscriptions, model1, model.UserId, model.ID);
                return false;
            }
            else
            {
                ((ScheduleOnce)model).Time = vmodel.Time;
            }
            return true;
        }

        private bool EditScheduleDaily(ScheduleViewModel vmodel, Schedule model)
        {
            if (model.GetType().BaseType.FullName != typeof(ScheduleDaily).FullName)
            {
                ScheduleDaily model1 = new ScheduleDaily(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOn, vmodel.StopDate);

                IEnumerable<SubscriptionModel> subscriptions = model.Subscriptions;
                AddReportFilters(subscriptions, model1, model.UserId, model.ID);
                return false;
            }
            else
            {
                ((ScheduleDaily)model).Time = vmodel.Time;
                ((ScheduleDaily)model).RepeatOn = vmodel.RepeatOn;
                ((ScheduleDaily)model).StopDate = vmodel.StopDate;
                return true;
            }
        }

        // GET: ScheduleOnces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = _repo.FindBy(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

      
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public PartialViewResult DeleteConfirmed(int id)
        {
            int userid = _repo.FindBy(id).UserId;
            _repo.Delete(id);
           
            return List(userid.ToString());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }

        
              public ActionResult AutoScheduled(string term)
        {

            IEnumerable<string> list = _repo.GetAllSchedule(term, Convert.ToInt32(Session["UserId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
