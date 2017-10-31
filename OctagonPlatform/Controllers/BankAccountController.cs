using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
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
            catch (Exception)
            {
                ViewBag.Error = "Bank accounts not found. ";
                return View("Error");
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

                var bankAccount = _bAccountRepository.BAccountDetails((int) id);

                if (bankAccount == null)
                {
                    return HttpNotFound();
                }
                var viewModel = Mapper.Map<BankAccount, BAccountFVModel>(bankAccount);

                return View(viewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
        }

        // GET: BankAccount/Create
        public ActionResult Create(int partnerId)
        {
            try
            {
                return View(_bAccountRepository.RenderBaFormViewModel(partnerId));
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
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

                    return View(_bAccountRepository.RenderBaFormViewModel(bankAccount.PartnerId));
                }
                _bAccountRepository.SaveBankAccount(bankAccount, "Create");
                return RedirectToAction("Details", "Partners", new {id = bankAccount.PartnerId});
          
                
            }
            catch (Exception)
            {
                ViewBag.Error = "Error creating Bank account, please check the entered values. ";
                return View(_bAccountRepository.RenderBaFormViewModel(bankAccount.PartnerId));
            }

        }

        // GET: BankAccount/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                
                var bankAccount = _bAccountRepository.BankAccountToEdit(id);
                if (bankAccount != null) return View(bankAccount);
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
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
               
                _bAccountRepository.SaveBankAccount(bankAccount, "Edit");
                return RedirectToAction("Details", "Partners", new { id = bankAccount.PartnerId });
                
                
            }
            catch (Exception)
            {
                ViewBag.Error = "Error editing BankAccount, please check the entered values.  ";
                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {

                var bankAccount = _bAccountRepository.BankAccountToEdit(id);

                if (bankAccount != null) return View(bankAccount);

                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
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
                catch (Exception)
                {
                    ViewBag.Error = "Bank account not found. ";
                    return RedirectToAction("Details", "Partners", new { id = partnerId });
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }

        }
        
        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_bAccountRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception)
            {
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
        }
    }
}
