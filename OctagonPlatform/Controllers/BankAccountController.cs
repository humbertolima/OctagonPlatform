﻿using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
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

        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetBankAccounts)]
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

        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetBankAccounts)]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Bank account not found. ";
                    return View("Error");
                }
                
                var bankAccount = _bAccountRepository.BAccountDetails((int)id);
                
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddtBankAccounts)]
        public ActionResult Create(int? partnerId)
        {
            try
            {
                if (partnerId != null) return View(_bAccountRepository.RenderBaFormViewModel((int) partnerId));
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

       
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddtBankAccounts)]
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

        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditBankAccounts)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_bAccountRepository.BankAccountToEdit((int)id));
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

       
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditBankAccounts)]
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.DeleteBankAccounts)]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id != null) return View(_bAccountRepository.BankAccountToEdit((int)id));
                ViewBag.Error = "Bank account not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.DeleteBankAccounts)]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Bank account not found. ";
                    return View("Error");
                }
                var partnerId = _bAccountRepository.BAccountDetails((int)id).PartnerId;

                _bAccountRepository.DeleteBankAccount((int)id);
                return RedirectToAction("Details", "Partners", new {id = partnerId});

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }
        
        [System.Web.Mvc.HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.SearchBankAccount)]
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

        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AutoAccountBankAccount)]
        public ActionResult AutoAccount(string term)
        {

            IEnumerable<dynamic> list = _bAccountRepository.GetAllAccount(term, Convert.ToInt32(Session["partnerId"]));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
