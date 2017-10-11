using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class TerminalsController : Controller
    {
        private readonly ITerminalRepository _repository;

        public TerminalsController(ITerminalRepository repository)
        {
            _repository = repository;
        }

        // GET: Terminals
        public ActionResult Index()
        {
            try
            {
                return View(_repository.GetAllTerminals(int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // GET: Terminals/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var terminal = _repository.TerminalDetails(id);
                if (terminal == null)
                {
                    return HttpNotFound();
                }
                return View(terminal);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // GET: Terminals/Create
        public ActionResult Create(int partnerId)
        {
            try
            {
                return View(_repository.RenderTerminalFormViewModel(partnerId));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TerminalFormViewModel terminalFormViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(_repository.InitializeNewFormViewModel(terminalFormViewModel));
                }
                try
                {
                    _repository.SaveTerminal(terminalFormViewModel, "Create");
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException exDb)
                {
                    ViewBag.Error = "Validation error creating Terminal " + exDb.Message;

                    return View(_repository.InitializeNewFormViewModel(terminalFormViewModel));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Validation error creating Terminal "
                                    + ex.Message;
                    return View(_repository.InitializeNewFormViewModel(terminalFormViewModel));
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        // GET: Terminals/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_repository.TerminalToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TerminalFormViewModel terminalFormViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(_repository.TerminalToEdit(terminalFormViewModel.Id));
            }
            try
            {
                _repository.SaveTerminal(terminalFormViewModel, "Edit");
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing Terminal " + exDb.Message;

                return View(_repository.TerminalToEdit(terminalFormViewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Terminal "
                                + ex.Message;
                return View(_repository.TerminalToEdit(terminalFormViewModel.Id));
            }

        }

        // GET: Terminals/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                return View(_repository.TerminalToEdit(id));
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
                _repository.DeleteTerminal(id);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)

            {
                ViewBag.Error = "Validation error deleting Terminal" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Terminal" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_repository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        #region TerminalPartialViews

        [HttpPost]
        public PartialViewResult Contacts(int terminalId)
        {
            var terminal = _repository.TerminalDetails(terminalId);
            return PartialView("Sections/Contacts", terminal);

        }

        [HttpPost]
        public PartialViewResult GeneralInfo(int terminalId)
        {
            var terminal = _repository.TerminalDetails(terminalId);
            return PartialView("Sections/GeneralInfo", terminal);

        }

        [HttpPost]
        public PartialViewResult ConfigNotification(int terminalId)
        {
            var configNotification = _repository.GetConfigNotification(terminalId);
            return PartialView("Sections/ConfigNotification", configNotification);
        }


        [HttpPost]      //pendiente quitar este tipo de dato por un viewModel
        public ActionResult EditConfigAlert(TerminalAlertConfig terminalAlertConfig, int terminalId)
        {
            var configNotification = _repository.SetConfigNotification(terminalAlertConfig, terminalId);

            return RedirectToAction("Details/"+terminalId);
        }

        #endregion

    }
}
