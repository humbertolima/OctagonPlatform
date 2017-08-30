using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
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
            return View(_userRepository.RenderUserFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(_userRepository.InitializeNewFormViewModel(viewModel));
            }
            try
            {
                _userRepository.SaveUser(viewModel, "Create");
                return RedirectToAction("Index");
            }
            #region Catch

           
            catch (DbEntityValidationException exDb)
            {
                viewModel.Error = "Validation error in database. " + exDb.Message.ToString();
                viewModel.Partners = _userRepository.RenderUserFormViewModel().Partners;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                viewModel.Error = "Error creating user. " + ex.Message.ToString();
                viewModel.Partners = _userRepository.RenderUserFormViewModel().Partners;    //porque el Partner en RenderUserFormViewModel se envia la primera vez que se crea el view pero para cuando retorna error, se envia un viewModel que tiene el Partner en NULL.
                return View(viewModel);
            }
            #endregion
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userEdit = _userRepository.UserToEdit(id);
            return View(userEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var userEdit = _userRepository.UserToEdit(viewModel.Id);
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