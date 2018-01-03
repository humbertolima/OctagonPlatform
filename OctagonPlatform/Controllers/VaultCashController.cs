using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class VaultCashController : Controller
    {
        private readonly IVaultCashRepository _vaultCashRepository;

        public VaultCashController(IVaultCashRepository repository)
        {
            _vaultCashRepository = repository;
        }

        // GET: VaultCash

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

        public ActionResult Details(int? id)
        {
            try
            {
                if (id != null) return View(_vaultCashRepository.GetVaultCash((int)id));
                ViewBag.Error = "Vaultcash not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    VaultCashFormViewModel viewModel = _vaultCashRepository.VaultCashToEdit((int)id);
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
        public ActionResult Edit(VaultCashFormViewModel viewModel)
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

        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    VaultCashFormViewModel model = _vaultCashRepository.RenderVaultCashFormViewModel(id);
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
        public ActionResult Create(VaultCashFormViewModel viewModel)
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