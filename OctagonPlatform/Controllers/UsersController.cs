using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
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
            var result = _userRepository.GetAllUsers();
            return View(result);
        }

        //public ActionResult Details(int id)
        //{
        //    var result = _userRepository.UserDetails(id);
        //    return View(result);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View(_userRepository.RenderUserFormViewModel());
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
                viewModel = _userRepository.RenderUserFormViewModel();
                viewModel.Error = "Validation error in database. " + exDb.Message.ToString();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                viewModel.Error = "Error creating user. " + ex.Message.ToString();
                viewModel = _userRepository.RenderUserFormViewModel();    //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(viewModel);
            }
            #endregion
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userEdit = default(UserEditFormViewModel);
            try
            {
                userEdit = _userRepository.UserToEdit(id);
                return View(userEdit);
            }
            #region Exception
            catch (Exception ex)
            {
                userEdit = new UserEditFormViewModel();
                userEdit.Error = "Error edit user. " + ex.Message.ToString();
                userEdit.Partners = _userRepository.RenderUserFormViewModel().Partners;    //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(userEdit);
            }
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditFormViewModel editViewModel)
        {
            var viewModel = default(UserFormViewModel);

            if (!ModelState.IsValid)
            {
                var userEdit = _userRepository.UserToEdit(editViewModel.Id);
                return View(userEdit);
            }
            try
            {
                //viewModel = new MapFrom<UserEditFormViewModel>().ToUserFormView(editViewModel);
                viewModel = Mapper.Map<UserEditFormViewModel, UserFormViewModel>(editViewModel);

                _userRepository.SaveUser(viewModel, "Edit");
                return RedirectToAction("Index");
            }
            #region Exception
            catch (DbEntityValidationException exDb)
            {
                editViewModel.Error = "Validation error in database. " + exDb.Message.ToString();
                editViewModel.Partners = _userRepository.RenderUserFormViewModel().Partners;
                return View(editViewModel);
            }
            catch (Exception ex)
            {
                editViewModel.Error = "Error creating user. " + ex.Message.ToString();
                editViewModel.Partners = _userRepository.RenderUserFormViewModel().Partners;    //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(editViewModel);
            }
            #endregion
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(_userRepository.UserDetails(id));
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