using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
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

            
            return View(bankAccounts);
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
        public ActionResult Create(int partnerId)
        {

            //ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "BusinessName");
            //ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View(_bAccountRepository.RenderBaFormViewModel(partnerId));
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BAEditFVModel bankAccount)
        {
            if (!ModelState.IsValid)
            {

                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }
            try
            {
                _bAccountRepository.SaveBankAccount(bankAccount, "Create");
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error creating BankAccount " + exDb.Message;

                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error creating BankAccount "
                                + ex.Message;
                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }

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
        public ActionResult Edit(BAEditFVModel bankAccount)
        {
            if (!ModelState.IsValid)
            {

                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }
            try
            {
                _bAccountRepository.SaveBankAccount(bankAccount, "Edit");
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing BankAccount " + exDb.Message;

                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing BankAccount "
                                + ex.Message;
                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }

        }

        // GET: BankAccount/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {

            return View(_bAccountRepository.BankAccountToEdit(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _bAccountRepository.DeleteBankAccount(id);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error deleting BankAccount" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting BankAccount" + ex.Message;
                return RedirectToAction("Index");
            }
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
