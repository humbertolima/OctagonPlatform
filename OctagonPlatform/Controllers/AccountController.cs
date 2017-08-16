using OctagonPlatform.Models;
using OctagonPlatform.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel viewModel)
        {
            var user = _context.Users.Any(u => u.UserName == viewModel.UserName && u.Password == viewModel.Password);
            if (!user)
            {
                
                viewModel.TriesToLogin++;
                if (viewModel.TriesToLogin >= 3)
                {
                    var userTrie = _context.Users.SingleOrDefault(u => u.UserName == viewModel.UserName);
                    if (userTrie != null)
                    {
                        userTrie.IsLocked = true;
                        _context.SaveChanges();
                        ViewBag.Message = "User Locked, please contact the Administrator";
                    }
                }
                else
                {
                    return View(viewModel);
                }
            }
            FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult Logout()
        {
           
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //Implementacion
    }
}