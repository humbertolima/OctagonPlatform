using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class InterChangeController : BaseController
    {

        private readonly IInterChangeRepository _repository;

        public InterChangeController(IInterChangeRepository repository)
        {
            _repository = repository;
        }

        //[HttpPost]
        //public PartialViewResult Index(int? terminalId)
        //{
        //    try
        //    {
        //        if (terminalId == null)
        //        {
        //            ViewBag.Error = "InterChange not found. ";
        //            return PartialView("Error");
        //        }
        //        var interchanges = _repository.GetAllInterChanges((int) terminalId);
        //        ViewBag.TerminalId = terminalId;
        //        return PartialView(interchanges);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return PartialView("Error");
        //    }
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "InterChange not found. ";
                    return View("Error");
                }
                var viewModel = _repository.InterChangeToEdit((int) id);
                if (viewModel != null) return PartialView("Modal/AddInterChange", viewModel); ;
                ViewBag.Error = "Model not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "InterChange not found. ";
                    return View("Error");
                }
                var viewModel = _repository.RenderInterChangeFormViewModel((int) id);
                if (viewModel != null) return PartialView("Modal/AddInterChange", viewModel);
                ViewBag.Error = "Model not found. ";
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
        public ActionResult Save(InterChangeFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid && viewModel.Id > 0)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return View("InterChangeForm", _repository.InterChangeToEdit(viewModel.Id));
                }
                if (!ModelState.IsValid && viewModel.Id == 0)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return View("InterChangeForm",
                        _repository.InitializeNewInterChangeFormViewModel(viewModel));
                }


                _repository.SaveInterChange(viewModel, viewModel.Id == 0 ? "Create" : "Edit");

                return RedirectToAction("Details", "Terminals", new {id = viewModel.TerminalId});
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + ", Please check the entered values. ";
                return RedirectToAction("Details", "Terminals", new { id = viewModel.Id});
            }
        }

        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (id != null) return View(_repository.InterChangeToEdit((int)id));
        //        ViewBag.Error = "InterChange not found. ";
        //        return View("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View("Error");
        //    }
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? terminalId)
        {
            try
            {
                if (terminalId == null || id == null)
                {
                    ViewBag.Error = "InterChange not found. ";
                    return View("Error");
                }
                _repository.DeleteInterChange((int) id);
                return RedirectToAction("Details", "Terminals", new {id = id});
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting InterChange" + ex.Message;
                return RedirectToAction("Details", "Terminals", new {id = id});
            }
        }
    }
}
    
