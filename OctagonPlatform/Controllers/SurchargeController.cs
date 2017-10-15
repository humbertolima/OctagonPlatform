using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class SurchargeController : Controller
    {
        private readonly ISurchargeRepository _surchargeRepository;

        public SurchargeController(ISurchargeRepository repository)
        {
            _surchargeRepository = repository;
        }

        [HttpPost]
        public PartialViewResult Index(int terminalId)
        {
            try
            {
                var surcharges = _surchargeRepository.GetAllSurcharges(terminalId);
                ViewBag.TerminalId = terminalId;
                return PartialView(surcharges);
            }
            catch (Exception)
            {
                return PartialView("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = _surchargeRepository.SurchargeToEdit(id);
                if (model != null) return View("SurchargeForm", model);
                ViewBag.Error = "Model not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }
        
        [HttpGet]
        public ActionResult Create(int terminalId)
        {
            try
            {
                var model = _surchargeRepository.RenderSurchargeFormViewModel(terminalId);
                if (model != null) return View("SurchargeForm", model);
                ViewBag.Error = "Model not found. ";
                return PartialView("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SurchargeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("SurchargeForm", _surchargeRepository.SurchargeToEdit(viewModel.Id));
            }
            try
            {
                _surchargeRepository.SaveSurcharge(viewModel, viewModel.Id == 0 ? "Create" : "Edit");

                return RedirectToAction("Details", "Terminals", new { id = viewModel.TerminalId });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing VaultCash " + exDb.Message;

                return View("SurchargeForm", _surchargeRepository.SurchargeToEdit(viewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing VaultCash "
                                + ex.Message;
                return View("SurchargeForm", _surchargeRepository.SurchargeToEdit(viewModel.Id));
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _surchargeRepository.DeleteSurcharge(id);
                var surcharge = _surchargeRepository.SurchargeToEdit(id);
                return RedirectToAction("Details", "Terminals", new { id = surcharge.TerminalId });

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }
    }
}