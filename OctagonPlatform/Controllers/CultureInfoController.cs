using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;

namespace OctagonPlatform.Controllers
{
    public class CultureInfoController : BaseController
    {
        protected ICultureInfo _repo;
        public CultureInfoController(ICultureInfo repo)
        {
            _repo = repo;
          
        }
        // GET: CultureInfoModels
        public ActionResult Index()
        {
            var cultureInfoModels = _repo.AllIncludeCountry();
            return View(cultureInfoModels.ToList());
        }

        // GET: CultureInfoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultureInfoModel cultureInfoModel = _repo.FindBy(id);
            if (cultureInfoModel == null)
            {
                return HttpNotFound();
            }
            return View(cultureInfoModel);
        }

        // GET: CultureInfoModels/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_repo.GetCountries(), "Id", "Name");
            return View();
        }

        // POST: CultureInfoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CountryId")] CultureInfoModel cultureInfoModel)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(cultureInfoModel);
               
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_repo.GetCountries(), "Id", "Name", cultureInfoModel.CountryId);
            return View(cultureInfoModel);
        }

        // GET: CultureInfoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultureInfoModel cultureInfoModel = _repo.FindBy(id);
            if (cultureInfoModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_repo.GetCountries(), "Id", "Name", cultureInfoModel.CountryId);
            return View(cultureInfoModel);
        }

        // POST: CultureInfoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CountryId")] CultureInfoModel cultureInfoModel)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(cultureInfoModel).State = EntityState.Modified;
                //db.SaveChanges();
                _repo.Edit(cultureInfoModel);
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_repo.GetCountries(), "Id", "Name", cultureInfoModel.CountryId);
            return View(cultureInfoModel);
        }

        // GET: CultureInfoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultureInfoModel cultureInfoModel = _repo.FindBy(id);
            if (cultureInfoModel == null)
            {
                return HttpNotFound();
            }
            return View(cultureInfoModel);
        }

        // POST: CultureInfoModels/Delete/5
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
    }
}
