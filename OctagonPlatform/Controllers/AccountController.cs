﻿using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel viewModel)
        {
            var userToLogin = _accountRepository.Login(viewModel);

            if (userToLogin == null)
            {
                ViewBag.Message = "Invalid User";
                return View(viewModel);
            }
            if (!userToLogin.IsLocked) return RedirectToAction("Index", "Dashboard");
            ViewBag.Message = "User Locked, please call the Administrator";
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _accountRepository.Logout();
            return RedirectToAction("Index", "Home");
        }

        //Implementacion
    }
}