using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;
using System.Web.Security;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return RedirectToAction("Index", "Home", viewModel);
                }
                if (Session["tries"] != null)
                {
                    viewModel.TriesToLogin = int.Parse(Session["tries"].ToString());
                }
                var userToLogin = _accountRepository.Login(viewModel);
                if (userToLogin.IsLocked)
                {

                    if (Session["tries"] != null) Session["tries"] = 0;
                    ViewBag.Error = "User Locked, please call the Administrator";
                    return RedirectToAction("Index", "Home", viewModel);

                }

                if (userToLogin.Partner == null)
                {
                    ViewBag.Error = "Invalid User";
                    Session["tries"] = userToLogin.TriesToLogin;
                    return RedirectToAction("Index", "Home", viewModel);
                }
                FormsAuthentication.SetAuthCookie(userToLogin.UserName, false);
                Session["logo"] = userToLogin.Partner.Logo;
                Session["partnerId"] = userToLogin.Partner.Id;
                Session["businessName"] = userToLogin.Partner.BusinessName;
                Session["userName"] = userToLogin.UserName;
                Session["Name"] = userToLogin.Name;
                Session["UserId"] = userToLogin.UserId;
                if (Session["tries"] != null) Session.Remove("tries");
                return RedirectToAction("AMS", "Octagon");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", "Home", viewModel);
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                _accountRepository.Logout();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        //Implementacion
    }
}