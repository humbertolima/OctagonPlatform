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

namespace OctagonPlatform.Controllers.Reports
{
    public class ScheduleController : Controller
    {
        private ISchedule _repo;
        public ScheduleController(ISchedule repo)
        {
            _repo = repo;
        }

        // GET: ScheduleOnces
        public ActionResult Index()
        {
            return View(_repo.All());
        }       

        // GET: ScheduleOnces/Create
        public PartialViewResult Create()
        {
            ScheduleViewModel vmodel = new ScheduleViewModel();
            return PartialView(vmodel);
        }

        // POST: ScheduleOnces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Repeats,RepeatsSelected,StartDate,Time,RepeatOn,StopDate,RepeatOnWeeks,RepeatOnDaysWeeks,RepeatOnDay,RepeatOnMonth,RepeatOnFirst,RepeatOnDay2,RepeatOnMonth2")] ScheduleViewModel vmodel)
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
                _repo.Add(model);               
                return RedirectToAction("Index");
            }

            return View(vmodel);
        }

        // GET: ScheduleOnces/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ScheduleOnces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,Name,Repeats,StartDate")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {              
                _repo.Edit(schedule);
                return RedirectToAction("Index");
            }
            return View(schedule);
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
            Schedule schedule = _repo.FindBy(id);
            _repo.Delete(schedule);          
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
    }
}
