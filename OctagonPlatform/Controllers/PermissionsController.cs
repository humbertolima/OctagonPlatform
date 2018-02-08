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
    [Authorize]
    public class PermissionsController : Controller
    {
        private readonly IPermission _permissionRepository;

        public PermissionsController(IPermission permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }


        public ActionResult Index()
        {
            var permissions = _permissionRepository.GetAllPermissions();
            return View(permissions.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = _permissionRepository.FindBy(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // GET: Permissions/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(_permissionRepository.ToSelectControlPermissions(), "Id", "Name");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ParentID")] Permission permission)
        {

            if (ModelState.IsValid)
            {
                bool isPermission = ValidateExist(permission.Name);
                if (isPermission)
                {
                    throw new Exception("Ya existe el permiso");
                }
                _permissionRepository.Add(permission);

                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(_permissionRepository.ToSelectControlPermissions(), "Id", "Name", permission.ParentID);
            return View(permission);
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = _permissionRepository.FindBy(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(_permissionRepository.ToSelectControlPermissions(), "Id", "Name", permission.ParentID);
            return View(permission);
        }

        // POST: Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ParentID")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                bool isPermission = ValidateExist(permission.Name);
                if (isPermission)
                {
                    throw new Exception("Ya existe el permiso");
                }

                _permissionRepository.Edit(permission);
                //db.Entry(permission).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(_permissionRepository.ToSelectControlPermissions(), "Id", "Name", permission.ParentID);
            return View(permission);
        }

        private bool ValidateExist(string name)
        {
            var permissions = _permissionRepository.FindAllBy(p => p.Name == name);

            if (permissions.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Permission permission = _permissionRepository.FindBy(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permission permission = _permissionRepository.FindBy(id);
            _permissionRepository.Delete(permission);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _permissionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
