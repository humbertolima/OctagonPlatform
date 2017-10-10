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
    [Authorize]
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
            try
            {
                var bankAccounts = _bAccountRepository.GetAllBankAccount(int.Parse(Session["partnerId"].ToString()));


                return View(bankAccounts);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // GET: BankAccount/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                BankAccount bankAccount = _bAccountRepository.BAccountDetails((int) id);

                if (bankAccount == null)
                {
                    return HttpNotFound();
                }
                BAccountFVModel viewModel = Mapper.Map<BankAccount, BAccountFVModel>(bankAccount);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // GET: BankAccount/Create
        public ActionResult Create(int partnerId)
        {
            try
            {
                return View(_bAccountRepository.RenderBaFormViewModel(partnerId));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BAEditFVModel bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
                }
                try
                {
                    _bAccountRepository.SaveBankAccount(bankAccount, "Create");
                    return RedirectToAction("Details", "Partners", new {id = bankAccount.PartnerId});
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
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }

        }

        // GET: BankAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BAEditFVModel bankAccount = _bAccountRepository.BankAccountToEdit((int) id);
                if (bankAccount == null)
                {
                    return HttpNotFound();
                }
                
                return View(bankAccount);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BAEditFVModel bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
                }
                try
                {
                    _bAccountRepository.SaveBankAccount(bankAccount, "Edit");
                    return RedirectToAction("Details", "Partners", new { id = bankAccount.PartnerId });
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
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_bAccountRepository.BankAccountToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var partnerId = _bAccountRepository.BAccountDetails(id).PartnerId;
                try
                {
                    
                    _bAccountRepository.DeleteBankAccount(id);
                    return RedirectToAction("Details", "Partners", new { id = partnerId});
                }
                catch (DbEntityValidationException exDb)
                {
                    ViewBag.Error = "Validation error deleting BankAccount" + exDb.Message;
                    return RedirectToAction("Details", "Partners", new { id = partnerId });
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Validation error deleting BankAccount" + ex.Message;
                    return RedirectToAction("Details", "Partners", new { id = partnerId });
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }

        }
        
        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_bAccountRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
    }
}
