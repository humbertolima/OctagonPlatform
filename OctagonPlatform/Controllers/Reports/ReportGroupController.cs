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
using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class ReportGroupController : Controller
    {
        private IReportGroup _repo;
        public ReportGroupController(IReportGroup repo)
        {
            _repo = repo;
        }
        // GET: ReportGroupModels
        public ActionResult Index()
        {
            Session["businessName"] = "";
            return View(_repo.All());
        }

      
        // GET: ReportGroupModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportGroupModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,Name")] ReportGroupModel reportGroupModel)
        {
            List<string> modelErrors = new List<string>();
         //quitar esto
            if (ModelState.IsValid && !IsNameExists(reportGroupModel.Name))
            {
                _repo.Add(reportGroupModel);
                return Json(reportGroupModel);
            }
            else
            {
                
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                if (IsNameExists(reportGroupModel.Name))
                    modelErrors.Add("Name already exist");
            }

            return Json(modelErrors);
        }             
        
        
        [HttpPost]
        public JsonResult DeleteAjax(string Ids)
        {
            string[] listid = Ids.Split(',');
            _repo.DeleteRange(listid);
            //ReportGroupModel reportGroupModel = _repo.FindBy(id);
            // _repo.Delete(reportGroupModel);
            return Json(listid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
              

        private bool IsNameExists(string name) => _repo.FindByName(name) != null; // => este operador dice que es una funcion que va a return bool segun la expresion 
    }
}
