using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [System.Web.Mvc.Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountRepository _bAccountRepository;

        public BankAccountController(IBankAccountRepository bAccountRepository)
        {
            _bAccountRepository = bAccountRepository;

        }


        public ActionResult Index()
        {
            try
            {
                var bankAccounts = _bAccountRepository.GetAllBankAccount(int.Parse(Session["partnerId"].ToString()));


                return View(bankAccounts);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        public ActionResult Details(int id)
        {
            try
            {
                
                var bankAccount = _bAccountRepository.BAccountDetails(id);
                
                var viewModel = Mapper.Map<BankAccount, BAccountFVModel>(bankAccount);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

       
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BAEditFVModel bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_bAccountRepository.RenderBaFormViewModel(bankAccount.PartnerId));
                }
                _bAccountRepository.SaveBankAccount(bankAccount, "Create");
                return RedirectToAction("Details", "Partners", new {id = bankAccount.PartnerId});
          
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_bAccountRepository.RenderBaFormViewModel(bankAccount.PartnerId));
            }

        }


        public ActionResult Edit(int id)
        {
            try
            {
                return View(_bAccountRepository.BankAccountToEdit(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

       
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BAEditFVModel bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
                }

                _bAccountRepository.SaveBankAccount(bankAccount, "Edit");
                return RedirectToAction("Details", "Partners", new {id = bankAccount.PartnerId});


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_bAccountRepository.BankAccountToEdit(bankAccount.Id));
            }

        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_bAccountRepository.BankAccountToEdit(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var partnerId = _bAccountRepository.BAccountDetails(id).PartnerId;

                _bAccountRepository.DeleteBankAccount(id);
                return RedirectToAction("Details", "Partners", new {id = partnerId});

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_bAccountRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }
}
