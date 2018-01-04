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
using Newtonsoft.Json;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class ReportGroupController : Controller
    {
        private IReportGroup _repo;
        public ITerminalRepository _repotn;
        public ReportGroupController(IReportGroup repo, ITerminalRepository repotn)
        {
            _repo = repo;
            _repotn = repotn;
        }
        // GET: ReportGroupModels
        public ActionResult Index()
        {
           
            ReportingGroupVM vmodel = new ReportingGroupVM(_repo.All());
            return View(vmodel);
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
                reportGroupModel.PartnerId = Convert.ToInt32(Session["partnerId"]);
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
           // string[] listid = Ids.Split(',');
           // _repo.DeleteRange(listid);          
          
             _repo.Delete(Int32.Parse(Ids));
            return Json(Ids);
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
        public ActionResult AutoState(string term)
        {

            IEnumerable<dynamic> list = _repotn.GetAllState(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoCity(string term)
        {

            IEnumerable<dynamic> list = _repotn.GetAllCity(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoZipCode(string term)
        {

            List<string> list = _repotn.GetAllZipCode(term);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DisplayTerminalsByGroup(string groupSelected,string partner,string state,string city,string zipcode)
        {
            return Json(ListTerminalFilter(groupSelected, partner, state, city, zipcode));
        }
        
        [HttpPost]
        public JsonResult AsignTerminal(string listtn,string groupSelected, string partner, string state, string city, string zipcode)
        {
             string[] list = listtn.Split(',');
             _repotn.EditRange(list,Int32.Parse(groupSelected));     

            return Json(ListTerminalFilter( groupSelected,  partner,  state,  city,  zipcode));
        }
        [HttpPost]
        public JsonResult UnasignTerminal(string listtn, string groupSelected, string partner, string state, string city, string zipcode)
        {
            string[] list = listtn.Split(',');
            _repotn.EditRange(list,null);

            return Json(ListTerminalFilter(groupSelected, partner, state, city, zipcode));
        }
        
        private string ListTerminalFilter(string groupSelected, string partner, string state, string city, string zipcode)
        {
            List<Terminal> unassoGroup = _repotn.GetTerminalAssociatedGroup(Int32.Parse(partner), Int32.Parse(state), Int32.Parse(city), zipcode); //terminals unassociated que tengan groupid null
            List<Terminal> assoGroup = _repotn.GetTerminalAssociatedGroup(Int32.Parse(partner), Int32.Parse(state), Int32.Parse(city), zipcode, Int32.Parse(groupSelected));
            List<List<Terminal>> list = new List<List<Terminal>>();
            list.Add(unassoGroup);
            list.Add(assoGroup);
            JsonResult ll = Json(list);


            string result = JsonConvert.SerializeObject(list, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return result;
        }
    }
}
