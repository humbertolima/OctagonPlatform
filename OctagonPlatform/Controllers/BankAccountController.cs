using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountRepository _bAccountRepository;

        public BankAccountController(IBankAccountRepository bAccountRepository)
        {
            _bAccountRepository = bAccountRepository;
        }

        // GET: BankAccount
        public ActionResult Index()
        {
            var bankAccounts = _bAccountRepository.GetAllBankAccount();

            var viewModel = new List<BAccountFVModel>();

            //var viewModel = bankAccounts.Select(item => Mapper.Map<BankAccount, BAccountFVModel>(item)).ToList();

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

            BankAccount bankAccount = _bAccountRepository.BAccountDetails((int)id);

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
            return View(_bAccountRepository.RenderBAFormViewModel());
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
                _bAccountRepository.SaveBankAccount(editBankAccount, "Create");

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
            BAEditFVModel bankAccount = _bAccountRepository.BankAccountToEdit((int)id);
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
        public ActionResult Edit([Bind(Include = "Id,NickName,RoutingNumber,AccountNumber,Status,NameOnCheck,FedTax,Ssn,CountryId,StateId,CityId,Address1,Address2,Zip,Phone,Email,PartnerId,AccountType,Deleted,CreatedAt,CreatedBy,DeletedAt,DeletedBy,UpdatedAt,UpdatedBy,UpdatedByName,CreatedByName,DeletedByName")] BAEditFVModel bankAccount)
        {
            if (ModelState.IsValid)
            {
                _bAccountRepository.SaveBankAccount(bankAccount, "Edit");
                return RedirectToAction("Index");
            }
            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", bankAccount.CityId);
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", bankAccount.CountryId);
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName", bankAccount.PartnerId);
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name", bankAccount.StateId);
            return View(bankAccount);
        }

        // GET: BankAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _bAccountRepository.DeleteBankAccount(Convert.ToInt32(id));

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

        [HttpPost]
        public ActionResult Search(string search)
        {
            return PartialView(_bAccountRepository.Search(search));
        }
    }
}
