using OctagonPlatform.Models;
using OctagonPlatform.Models.ManagmentViewModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class PartnerContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnerContactController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: PartnerContactManagmentViewModels
        public ActionResult Index()
        {
            return View(_context.PartnerContactManagmentViewModels.ToList());
        }

        // GET: PartnerContactManagmentViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerContactManagmentViewModel partnerContactManagmentViewModel = _context.PartnerContactManagmentViewModels.Find(id);
            if (partnerContactManagmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(partnerContactManagmentViewModel);
        }

        // GET: PartnerContactManagmentViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartnerContactManagmentViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartnerId,Name,LastName,Email,ContactTypeId,Address1,Address2,Zip,CountryId,StateId,CityId")] PartnerContactManagmentViewModel partnerContactManagmentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.PartnerContactManagmentViewModels.Add(partnerContactManagmentViewModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partnerContactManagmentViewModel);
        }

        // GET: PartnerContactManagmentViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerContactManagmentViewModel partnerContactManagmentViewModel = _context.PartnerContactManagmentViewModels.Find(id);
            if (partnerContactManagmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(partnerContactManagmentViewModel);
        }

        // POST: PartnerContactManagmentViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartnerId,Name,LastName,Email,ContactTypeId,Address1,Address2,Zip,CountryId,StateId,CityId")] PartnerContactManagmentViewModel partnerContactManagmentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(partnerContactManagmentViewModel).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partnerContactManagmentViewModel);
        }

        // GET: PartnerContactManagmentViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerContactManagmentViewModel partnerContactManagmentViewModel = _context.PartnerContactManagmentViewModels.Find(id);
            if (partnerContactManagmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(partnerContactManagmentViewModel);
        }

        // POST: PartnerContactManagmentViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartnerContactManagmentViewModel partnerContactManagmentViewModel = _context.PartnerContactManagmentViewModels.Find(id);
            _context.PartnerContactManagmentViewModels.Remove(partnerContactManagmentViewModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
