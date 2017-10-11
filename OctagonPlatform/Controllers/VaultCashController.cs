using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
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
        [HttpPost]
        public PartialViewResult Index(int terminalId)
        {
            try
            {
                var model = _vaultCashRepository.GetVaultCash(terminalId);

                ViewBag.TerminalId = terminalId;
                return PartialView(model);
            }
            catch (Exception)
            {
                return PartialView("Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var model = _vaultCashRepository.VaultCashDetails(id);
                if (model == null) return HttpNotFound("Model not found. ");
                return View(model);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + "Page not found. ");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var model = _vaultCashRepository.VaultCashToEdit(id);
                if (model == null) return HttpNotFound("Model not found. ");
                return View(model);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VaultCashFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(_vaultCashRepository.VaultCashToEdit(viewModel.Id));
            }
            try
            {
                _vaultCashRepository.SaveVaultCash(viewModel, "Edit");
                return RedirectToAction("Details", "Terminals", new {id = viewModel.Id});
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing VaultCash " + exDb.Message;

                return View(_vaultCashRepository.VaultCashToEdit(viewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing VaultCash "
                                + ex.Message;
                return View(_vaultCashRepository.VaultCashToEdit(viewModel.Id));
            }
        }

        public ActionResult Create(int terminalId)
        {
            try
            {
                return View(_vaultCashRepository.RenderVaultCashFormViewModel(terminalId));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + "Page not found. ");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VaultCashFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
            }
            try
            {
                _vaultCashRepository.SaveVaultCash(viewModel, "Create");
                return RedirectToAction("Details", "Terminals", new { id = viewModel.Id });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error creating VaultCash " + exDb.Message;

                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error creating VaultCash "
                                + ex.Message;
                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_vaultCashRepository.VaultCashToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _vaultCashRepository.DeleteVaultCash(id);
                return RedirectToAction("Details", "Terminals", new { id });
            }
            catch (DbEntityValidationException exDb)

            {
                ViewBag.Error = "Validation error deleting VaultCash" + exDb.Message;
                return RedirectToAction("Details", "Terminals", new { id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting VaultCash" + ex.Message;
                return RedirectToAction("Details", "Terminals", new { id });
            }
        }

    }
}