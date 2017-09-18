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
using OctagonPlatform.Models.InterfacesRepository;
using AutoMapper;
using OctagonPlatform.Models.FormsViewModels;
using System.Collections;

namespace OctagonPlatform.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountRepository _BAccountRepository;

        public BankAccountController(IBankAccountRepository BAccountRepository)
        {
            _BAccountRepository = BAccountRepository;
        }

        // GET: BankAccount
        public ActionResult Index()
        {
            var bankAccounts = _BAccountRepository.GetAllBankAccount();

            IList viewModel = new List<BAccountFVModel>();

            foreach (var item in bankAccounts)
            {   //creado porque no se puede mapear una lista de tipos de objetos. Solo se mapea un tipo de objeto.
                viewModel.Add(Mapper.Map<BankAccount, BAccountFVModel>(item));
            }

            return View(viewModel);
        }

        // GET: BankAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BankAccount bankAccount = _BAccountRepository.BAccountDetails((int)id);

            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            BAccountFVModel viewModel = Mapper.Map<BankAccount, BAccountFVModel>(bankAccount);

            return View(viewModel);
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {

            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName");
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View(_BAccountRepository.RenderBAFormViewModel());
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BAEditFVModel editBankAccount)
        {
            if (ModelState.IsValid)
            {
                _BAccountRepository.SaveBankAccount(editBankAccount, "Create");

                return RedirectToAction("Index");
            }

            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(editBankAccount);
        }

        // GET: BankAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAEditFVModel bankAccount = _BAccountRepository.BankAccountToEdit((int)id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // POST: BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NickName,RoutingNumber,AccountNumber,Status,NameOnCheck,FedTax,Ssn,CountryId,StateId,CityId,Address1,Address2,Zip,Phone,Email,PartnerId,AccountType,Deleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy,UpdatedAt,UpdatedBy,UpdatedByName,CreatedByName,DeletedByName")] BAEditFVModel bankAccount)
        {
            if (ModelState.IsValid)
            {
                _BAccountRepository.SaveBankAccount(bankAccount, "Edit");
                return RedirectToAction("Index");
            }
            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // GET: BankAccount/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BankAccount bankAccount = default(BankAccount);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            //if (bankAccount == null)
            //{
            //    return HttpNotFound();
            //}
            return View(bankAccount);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            //db.BankAccounts.Remove(bankAccount);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                //_BAccountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
