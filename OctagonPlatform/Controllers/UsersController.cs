using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var result = _userRepository.GetAllUsers(int.Parse(Session["partnerId"].ToString()));
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult Create(int? partnerId)
        {
            try
            {
                UserFormViewModel userFormVM = _userRepository.RenderUserFormViewModel((int)partnerId);

                if (partnerId != null)
                {
                    return View(userFormVM);
                }

                ViewBag.Error = "User not found. ";
                return View("Error");
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserFormViewModel viewModel, string permissions1)    //puse el 1 porque el controlador recibe un parametro "permissions" que es de tipo Models.Permission y trata de convertir el strin a este tipo de dato. por eso no puede
        {                                                                               //llamarse permissions/. https://stackoverflow.com/questions/7983023/the-parameter-conversion-from-type-system-string-to-type-x-failed-because-n
            if (!ModelState.IsValid)
            {

                ViewBag.Error = ViewModelError.Get(ModelState);
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
            try
            {
                string[] separator = { "," };
                string[] ids = (permissions1.Split(separator, StringSplitOptions.RemoveEmptyEntries));

                //viewModel.Permissions = _userRepository.AddPermissionToUser(permissions);
                viewModel.Permissions = _userRepository.GetPermissionsByArray(ids);
                
                _userRepository.SaveUser(viewModel, "Create");
                return RedirectToAction("Details", "Partners", new { id = viewModel.PartnerId });
            }
            catch (Exception ex)
            {
                viewModel.Error = "Error creating user. " + ex.Message;
                //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "User not found.";
                    return View("Error");
                }


                var userEdit = _userRepository.UserToEdit(Convert.ToInt32(id));
                return View(userEdit);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditFormViewModel editViewModel, string[] permissions1)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please check the entered values. ";
                var userEdit = _userRepository.UserToEdit(editViewModel.Id);
                return View(userEdit);
            }
            try
            {
                editViewModel.Permissions = _userRepository.GetPermissionsByArray(permissions1); //pendiente poner en el controlador de permissions

                //viewModel = new MapFrom<UserEditFormViewModel>().ToUserFormView(editViewModel);
                var viewModel = Mapper.Map<UserEditFormViewModel, UserFormViewModel>(editViewModel);

                _userRepository.SaveUser(viewModel, "Edit");
                return RedirectToAction("Details", "Partners", new { id = viewModel.PartnerId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var userFormViewModel = Mapper.Map<UserEditFormViewModel, UserFormViewModel>(editViewModel);
                var userFormViewModel2 = _userRepository.InitializeNewFormViewModel(userFormViewModel);
                var userEdit = Mapper.Map<UserFormViewModel, UserEditFormViewModel>(userFormViewModel2);
                return View(userEdit);
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                User user = _userRepository.UserDetails(Convert.ToInt32(id));

                if (id != null) return View(user);
                ViewBag.Error = "User not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id != null) return View(_userRepository.UserToEdit((int)id));
                ViewBag.Error = "User not found.";
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
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "User not found.";
                    return View("Error");
                }
                var partnerId = _userRepository.UserDetails((int)id).PartnerId;
                _userRepository.DeleteUser((int)id);
                return RedirectToAction("Details", "Partners", new { id = partnerId });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error deleting User" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting User" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_userRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        #region Get BankAccount Of User

        [HttpPost]
        // [ValidateAntiForgeryToken]       //pendiente probar los validatetoken
        public ActionResult Attach(string userId, string bankAccountId)
        {
            var userBaViewModel = _userRepository.AddBankAccountToUser(userId, bankAccountId);

            //pendiente retornar al partial view para refrescar solo el pedazo de la la lista de bank Account que tiene el usuario.
            return PartialView("Sections/BankAccounts", userBaViewModel);
        }

        public ActionResult DeAttach(string userId, string bankAccountId)
        {
            UserBAViewModel userBaViewModel = new UserBAViewModel();

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(bankAccountId) && (!string.IsNullOrEmpty(userId)))
                {
                    var userIdConvert = Convert.ToInt32(userId);
                    var bAId = Convert.ToInt32(bankAccountId);
                    userBaViewModel = _userRepository.DeAttachBankAccountToUser(userIdConvert, bAId);
                }
            }
            return PartialView("Sections/BankAccounts", userBaViewModel);
        }

        public ActionResult AddTerminalToUSer(string terminalId, string userId)
        {
            int userIdConvert = Convert.ToInt32(userId);
            int terminalConvert = Convert.ToInt32(terminalId);

            List<Terminal> terminals = _userRepository.AddTerminalToUser(terminalConvert, userIdConvert);
            return PartialView("Sections/TerminalsUser", terminals);
        }

        public ActionResult DeleteTerminalToUser(int terminalId, int userId)
        {
            int userIdConvert = Convert.ToInt32(userId);
            int terminalConvert = Convert.ToInt32(terminalId);

            List<Terminal> terminals = _userRepository.DeleteTerminalToUser(terminalConvert, userIdConvert);

            return PartialView("Sections/TerminalsUser", terminals);
        }

        public PartialViewResult GetAllBankAccount(string userId, bool toAttach)
        {
            ViewBag.assigned = toAttach;

            var bankAccounts = _userRepository.GetAllBankAccount(userId, toAttach);

            return PartialView("Sections/BankAccounts", bankAccounts);
        }
    }
    #endregion


}