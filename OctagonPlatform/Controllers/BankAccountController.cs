using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctagonPlatform.Models;

namespace OctagonPlatform.Controllers
{
    public class BankAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccount
        public async Task<ActionResult> Index()
        {
            var bankAccounts = db.BankAccounts
                .Include(b => b.City)
                .Include(b => b.Country)
                .Include(b => b.Partner)
                .Include(b => b.State);
            return View(await bankAccounts.ToListAsync());
        }

        // GET: BankAccount/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName");
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View();
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NickName,RoutingNumber,AccountNumber,Status,NameOnCheck,FedTax,Ssn,CountryId,StateId,CityId,Address1,Address2,Zip,Phone,Email,PartnerId,AccountType,Deleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy,UpdatedAt,UpdatedBy,UpdatedByName,CreatedByName,DeletedByName")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankAccount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // GET: BankAccount/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // POST: BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NickName,RoutingNumber,AccountNumber,Status,NameOnCheck,FedTax,Ssn,CountryId,StateId,CityId,Address1,Address2,Zip,Phone,Email,PartnerId,AccountType,Deleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy,UpdatedAt,UpdatedBy,UpdatedByName,CreatedByName,DeletedByName")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // GET: BankAccount/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            db.BankAccounts.Remove(bankAccount);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
