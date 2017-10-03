using AutoMapper;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Net;
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
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        //public ActionResult Details(int id)
        //{
        //    var result = _userRepository.UserDetails(id);
        //    return View(result);
        //}

        [HttpGet]
        public ActionResult Create(int partnerId)
        {
            try
            {
                return View(_userRepository.RenderUserFormViewModel(partnerId));
            }
            #region Exception
            catch (SqlException ex)
            {
                return View(new UserFormViewModel
                {
                    Error = "Error rendering UserFormModel. " + ex.Message.ToString(),
                    Partners = new List<Partner>(),
                    SetOfPermissions = new List<SetOfPermission>()
                });
            }
            catch (Exception ex)
            {
                return View(new UserFormViewModel
                {
                    Error = "Error rendering UserFormModel. " + ex.Message.ToString(),
                    Partners = new List<Partner>(),
                    SetOfPermissions = new List<SetOfPermission>()
                });
            }
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserFormViewModel viewModel, string[] permissions1)
        {
            if (!ModelState.IsValid)
            {
                //Create error en helper
                #region Create error messages       
                viewModel.Error = "Validation Error. ";
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count > 0)
                    {
                        for (int i = 0; i < item.Errors.Count; i++)
                        {
                            viewModel.Error += item.Errors[i].ErrorMessage.ToString() + ". ";
                        }
                    }
                }
                #endregion  
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
            try
            {
                viewModel.Permissions = _userRepository.AddPermissionToUser(permissions1);

                _userRepository.SaveUser(viewModel, "Create");
                return RedirectToAction("Index");
            }
            #region Exception


            catch (DbEntityValidationException exDb)
            {
               
                viewModel.Error = "Validation error in database. " + exDb.Message.ToString();
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
            catch (Exception ex)
            {
                viewModel.Error = "Error creating user. " + ex.Message.ToString();
                //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
            #endregion
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Parameter required in Edit.");
            }

            try
            {
                var userEdit = _userRepository.UserToEdit(Convert.ToInt32(id));
                return View(userEdit);
            }
            #region Exception
            catch (Exception ex)
            {
               
                var userEdit = _userRepository.UserToEdit(Convert.ToInt32(id));
                userEdit.Error = "Error edit user. " + ex.Message.ToString();
                return View(userEdit);
            }
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditFormViewModel editViewModel, string[] permissions1)
        {
            if (!ModelState.IsValid)
            {
                var userEdit = _userRepository.UserToEdit(editViewModel.Id);
                return View(userEdit);
            }
            try
            {
                editViewModel.Permissions = _userRepository.AddPermissionToUser(permissions1);

                //viewModel = new MapFrom<UserEditFormViewModel>().ToUserFormView(editViewModel);
                var viewModel = Mapper.Map<UserEditFormViewModel, UserFormViewModel>(editViewModel);

                _userRepository.SaveUser(viewModel, "Edit");
                return RedirectToAction("Index");
            }
            #region Exception
            catch (DbEntityValidationException exDb)
            {
                editViewModel.Error = "Validation error in database. " + exDb.Message.ToString();
                editViewModel.Partners = _userRepository.RenderUserFormViewModel(editViewModel.PartnerId).Partners;
                return View(editViewModel);
            }
            catch (Exception ex)
            {
                editViewModel.Error = "Error creating user. " + ex.Message.ToString();
                editViewModel.Partners = _userRepository.RenderUserFormViewModel(editViewModel.PartnerId).Partners;    //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(editViewModel);
            }
            #endregion
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(_userRepository.UserDetails(Convert.ToInt32(id)));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_userRepository.UserToEdit(id));
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
                _userRepository.DeleteUser(id);
                return RedirectToAction("Index");
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

        public PartialViewResult GetAllBankAccount(string userId, bool toAttach)
        {
   

            ViewBag.assigned = toAttach;

            var bankAccounts = _userRepository.GetAllBankAccount(userId, toAttach);
            
            return PartialView("Sections/BankAccounts", bankAccounts);
        }
    }
    #endregion


}