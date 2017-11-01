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
        [HttpPost]
        public PartialViewResult Index(int terminalId)
        {
            try
            {
                var model = _vaultCashRepository.GetVaultCash(terminalId);

                ViewBag.TerminalId = terminalId;
                return PartialView(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                
                return View(_vaultCashRepository.GetVaultCash(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                
                return View(_vaultCashRepository.VaultCashToEdit(id));
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
                return RedirectToAction("Details", "Terminals", new {id = viewModel.Id});
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                
                return View(_vaultCashRepository.InitializeNewVaultCashFormViewModel(viewModel));
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

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_vaultCashRepository.VaultCashToEdit(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Details", "Terminals", new { id });
            }
        }

    }
}