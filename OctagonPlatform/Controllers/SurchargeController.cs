﻿using OctagonPlatform.Models.FormsViewModels;
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
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
            try
            {
                if (!ModelState.IsValid && viewModel.Id > 0)
                {

                    return View("SurchargeForm", _surchargeRepository.SurchargeToEdit(viewModel.Id));
                }
                if (!ModelState.IsValid && viewModel.Id == 0)
                {
                    return View("SurchargeForm",
                        _surchargeRepository.InitializeNewSurchargeFormViewModel(viewModel));
                }


                _surchargeRepository.SaveSurcharge(viewModel, viewModel.Id == 0 ? "Create" : "Edit");

                return RedirectToAction("Details", "Terminals", new {id = viewModel.TerminalId});
            }
            //catch (DbEntityValidationException)
            //{
            //    ViewBag.Error = "Validation error editing or creating Surcharge";

            //    return View("SurchargeForm",
            //        viewModel.Id == 0
            //            ? _surchargeRepository.RenderSurchargeFormViewModel(viewModel.TerminalId)
            //            : _surchargeRepository.SurchargeToEdit(viewModel.Id));
            //}
            catch (Exception ex)
            {
                ViewBag.Error = "Error editing or creating Surcharge "
                                + ex.Message;
                return View("SurchargeForm",
                    viewModel.Id == 0
                        ? _surchargeRepository.RenderSurchargeFormViewModel(viewModel.TerminalId)
                        : _surchargeRepository.InitializeNewSurchargeFormViewModel(viewModel));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_surchargeRepository.SurchargeToEdit(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int terminalId)
        {
            try
            {
                _surchargeRepository.DeleteSurcharge(id);
                return RedirectToAction("Details", "Terminals", new { id = terminalId });
            }
            catch (DbEntityValidationException)

            {
                ViewBag.Error = "Validation error deleting Surcharge";
                return RedirectToAction("Details", "Terminals", new { id = terminalId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Surcharge" + ex.Message;
                return RedirectToAction("Details", "Terminals", new { id = terminalId});
            }
        }
    }
}