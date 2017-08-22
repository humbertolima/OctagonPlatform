using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<User> result = _userRepository.GetAllUsers();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            UserFormViewModel userRender = _userRepository.RenderUserFormViewModel();
            return View(userRender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userRepository.SaveUser(viewModel, "Create");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Error add User. " + ex.Message, ex);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserFormViewModel userEdit = _userRepository.UserToEdit(id);
            return View(userEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _userRepository.SaveUser(viewModel, "Edit");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Error Edit User. " + ex.Message, ex);
            }


        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                UserDetailsViewModel result = _userRepository.UserDetails(id);
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Show User Details. " + ex.Message, ex);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _userRepository.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Error Show User Details. " + ex.Message, ex);
            }
        }
    }
}