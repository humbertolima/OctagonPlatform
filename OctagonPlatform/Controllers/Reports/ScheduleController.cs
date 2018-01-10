using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.Views.Schedule;

namespace OctagonPlatform.Controllers.Reports
{
    public class ScheduleController : Controller
    {
        private ISchedule _repo;
        private readonly IUserRepository _userRepository;
        public ScheduleController(ISchedule repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        // GET: ScheduleOnces
        public ActionResult Index()
        {
            
            ScheduleVM vmodel = new ScheduleVM();
            vmodel.List = _repo.GetScheduleByUser(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["partnerId"]));
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
                List = _repo.GetScheduleByUser(user.Id, user.PartnerId);
            }
            else
                List = _repo.All();
            return PartialView("List",List);
        }
        // GET: ScheduleOnces/Create
        public PartialViewResult Create(string userId)
        {
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
                            model = new ScheduleWeekly(vmodel.Name, vmodel.Repeats, vmodel.StartDate, vmodel.Time, vmodel.RepeatOnWeeks, vmodel.RepeatOnDaysWeeks,vmodel.StopDate);
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
               
            if (schedule.GetType() == typeof(ScheduleOnce))
            {
                vmodel.Time = ((ScheduleOnce)schedule).Time;
            }
            if (schedule.GetType() == typeof(ScheduleDaily))
            {
               vmodel.RepeatOn = ((ScheduleDaily)schedule).RepeatOn;
               vmodel.StopDate = ((ScheduleDaily)schedule).StopDate;
               vmodel.Time = ((ScheduleDaily)schedule).Time;
            }
            if (schedule.GetType() == typeof(ScheduleWeekly))
            {
                vmodel.RepeatOnWeeks = ((ScheduleWeekly)schedule).RepeatOnWeeks;
                vmodel.RepeatOnDaysWeeks = ((ScheduleWeekly)schedule).RepeatOnDaysWeeks;
                vmodel.Time = ((ScheduleWeekly)schedule).Time;
            }
            if (schedule.GetType() == typeof(ScheduleMonthly))
            {
                vmodel.RepeatOnDay = ((ScheduleMonthly)schedule).RepeatOnDay;
                vmodel.RepeatOnMonth = ((ScheduleMonthly)schedule).RepeatOnMonth;
                vmodel.Time = ((ScheduleMonthly)schedule).Time;
            }
            if (schedule.GetType() == typeof(ScheduleMonthlyRelative))
            {
                vmodel.RepeatOnFirst = ((ScheduleMonthlyRelative)schedule).RepeatOnFirst;
                vmodel.RepeatOnDay2 = ((ScheduleMonthlyRelative)schedule).RepeatOnDay;
                vmodel.RepeatOnMonth2 = ((ScheduleMonthlyRelative)schedule).RepeatOnMonth;
                vmodel.Time = ((ScheduleMonthlyRelative)schedule).Time;
            }
            return PartialView(vmodel);
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
                   
                    ((ScheduleOnce)model).Time = vmodel.Time;
                }
                else
                {
                    if (vmodel.Repeats == ScheduleType.RepeatsEnum.Daily)
                    {                      
                        ((ScheduleDaily)model).Time = vmodel.Time;
                        ((ScheduleDaily)model).RepeatOn = vmodel.RepeatOn;
                        ((ScheduleDaily)model).StopDate = vmodel.StopDate;
                    }
                    else
                    {
                        if (vmodel.Repeats == ScheduleType.RepeatsEnum.Weekly)
                        {
                            ((ScheduleWeekly)model).Time = vmodel.Time;
                            ((ScheduleWeekly)model).RepeatOnWeeks = vmodel.RepeatOnWeeks;
                            ((ScheduleWeekly)model).RepeatOnDaysWeeks = vmodel.RepeatOnDaysWeeks;
                            ((ScheduleWeekly)model).StopDate = vmodel.StopDate;
                        }
                        else
                        {
                            if (vmodel.Repeats == ScheduleType.RepeatsEnum.Monthly)
                            {
                                ((ScheduleMonthly)model).Time = vmodel.Time;
                                ((ScheduleMonthly)model).RepeatOnDay = vmodel.RepeatOnDay;
                                ((ScheduleMonthly)model).RepeatOnMonth = vmodel.RepeatOnMonth;
                                ((ScheduleMonthly)model).StopDate = vmodel.StopDate;
                            }
                            else
                            {
                                if (vmodel.Repeats == ScheduleType.RepeatsEnum.MonthlyRelative)
                                {
                                    ((ScheduleMonthlyRelative)model).Time = vmodel.Time;
                                    ((ScheduleMonthlyRelative)model).RepeatOnFirst = vmodel.RepeatOnFirst;
                                    ((ScheduleMonthlyRelative)model).RepeatOnDay = vmodel.RepeatOnDay2;
                                    ((ScheduleMonthlyRelative)model).RepeatOnMonth = vmodel.RepeatOnMonth2;
                                    ((ScheduleMonthlyRelative)model).StopDate = vmodel.StopDate;
                                }
                            }
                        }
                    }
                }
                _repo.Edit(model);              
            }
            return RedirectToAction("Index");

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

        // POST: ScheduleOnces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            _repo.Delete(id);          
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

        
              public ActionResult AutoScheduled(string term)
        {

            IEnumerable<string> list = _repo.GetAllSchedule(term, Convert.ToInt32(Session["partnerId"]), Convert.ToInt32(Session["UserId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
