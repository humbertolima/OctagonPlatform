using OctagonPlatform.Helpers;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class VaultCashController : BaseController
    {
        private readonly IVaultCashRepository _vaultCashRepository;

        public VaultCashController(IVaultCashRepository repository)
        {
            _vaultCashRepository = repository;
        }

        // GET: VaultCash
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.GetVaultCash)]
        public PartialViewResult Index(int? terminalId)
        {
            try
            {
                if (terminalId == null)
                {
                    ViewBag.Error = "Vaultcash not found. ";
                    return PartialView("Error");
                }
                var model = _vaultCashRepository.GetVaultCash((int)terminalId);

                ViewBag.TerminalId = terminalId;
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }

        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.GetVaultCash)]
        public ActionResult Details(int? id)
        {
            try
            {
               var viewModel = _vaultCashRepository.GetVaultCash((int)id);

                if (id != null) return View(viewModel);

                ViewBag.Error = "Vaultcash not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.EditVaultCash)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    VaultCashVM viewModel = _vaultCashRepository.VaultCashToEdit((int)id);
                    viewModel.Action = "Edit";      //para poder usar el mismo formulario modal de adicionar y edit, necesito hacer
                                                    //dinamico la accion que va tomar el Form cuando se le de submit.
                    return PartialView("Modal/AddVaultCash", viewModel);
                }
                ViewBag.Error = "Vaultcash not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.EditVaultCash)]
        public ActionResult Edit(VaultCashVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please check the entered values. ";
                return View(_vaultCashRepository.VaultCashToEdit(viewModel.Id));
            }
            try
            {
                _vaultCashRepository.SaveVaultCash(viewModel, "Edit");
                return RedirectToAction("Details", "Terminals", new { id = viewModel.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(_vaultCashRepository.VaultCashToEdit(viewModel.Id));
            }
        }


        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.AddVaultCash)]
        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    VaultCashVM model = _vaultCashRepository.RenderVaultCashFormViewModel(id);
                    //devuelvo el partialView siempre porque siempre se solicida desde el details y se mostrara como modal
                    //si se quiere devolver en el View("create") seria validar quien lo solicito para devolver. 
                    //se puede usar el mismo formulario si en vairas vistas.
                    return PartialView("Modal/AddVaultCash", model);
                }
                ViewBag.Error = "Vaultcash not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.AddVaultCash)]
        public ActionResult Create(VaultCashVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please check the entered values. ";
                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
            }
            try
            {
                _vaultCashRepository.SaveVaultCash(viewModel, "Create");
                return RedirectToAction("Details", "Terminals", new { id = viewModel.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
            }
        }


        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.DeleteVaultCash)]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Vaultcash not found. ";
                    return PartialView("Error");
                }
                _vaultCashRepository.DeleteVaultCash((int)id);
                return RedirectToAction("Details", "Terminals", new { id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Details", "Terminals", new { id });
            }
        }

    }
}