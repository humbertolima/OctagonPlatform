using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
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

        public ActionResult GetKey(string terminalId)
        {
            try
            {
                var result = _repository.GetKey(terminalId);
                var viewModel = new BindKeyViewModel() { Serial1 = result.K1.Serial, Serial2 = result.K2.Serial,
                    CheckDigt1 = result.K1.Kcv_PartAb,
                    CheckDigt2 = result.K2.Kcv_PartAb,
                    ATMCheckDigt = result.Checksum,
                    TerminalId = "" };
                
                return View("Sections/BindKey", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public PartialViewResult SetCassettes(CassetteViewModel cassette)
        {
            var terminal = _repository.SetCassettes(cassette);
            return PartialView("Details", terminal);
        }


        [HttpPost]
        public ActionResult SetBindKey(string terminalId, string serial1, string serial2)
        {
            try
            {
                var result = _repository.SetBindKey(terminalId, serial1, serial2);
                var result2 = _repository.GetKey(terminalId);
                
                ViewBag.ChkDigt1 = result2.Idk1;
                ViewBag.ChkDigt2 = result2.Idk2;
                ViewBag.AtmChkDigt = result2.Zmk;

                return View("Sections/BindKey", result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
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
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Terminals/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Terminal not found. ";
                    return View("Error");
                }
                var terminal = _repository.TerminalDetails((int)id);
                if (terminal == null)
                {
                    return HttpNotFound();
                }
                return View(terminal);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: Terminals/Create
        public ActionResult Create(int? partnerId)
        {
            try
            {
                if (partnerId != null) return View(_repository.RenderTerminalFormViewModel((int) partnerId));
                ViewBag.Error = "Terminal not found. ";
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
        public ActionResult Create(TerminalFormViewModel terminalFormViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return View(_repository.InitializeNewFormViewModel(terminalFormViewModel));
                }


                _repository.SaveTerminal(terminalFormViewModel, "Create");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Terminal, "
                                + ex.Message;
                return View(_repository.InitializeNewFormViewModel(terminalFormViewModel));
            }
        }

        // GET: Terminals/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_repository.TerminalToEdit((int) id));
                ViewBag.Error = "Terminal not found. ";
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
        public ActionResult Edit(TerminalFormViewModel terminalFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please check the entered values. ";
                return View(_repository.TerminalToEdit(terminalFormViewModel.Id));
            }
            try
            {
                _repository.SaveTerminal(terminalFormViewModel, "Edit");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Terminal "
                                + ex.Message;
                return View(_repository.TerminalToEdit(terminalFormViewModel.Id));
            }

        }

        // GET: Terminals/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id != null) return View(_repository.TerminalToEdit((int) id));
                ViewBag.Error = "Terminal not found. ";
                return View("Error");
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
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Terminal not found. ";
                    return View("Error");
                }
                _repository.DeleteTerminal((int)id);
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
        public PartialViewResult ConfigNotification(int id)
        {
            var configNotification = _repository.GetConfigNotification(id);
            return PartialView("Sections/ConfigNotification", configNotification);
        }


        [HttpPost]      //pendiente quitar este tipo de dato por un viewModel
        public ActionResult SetConfigNotification(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel)
        {
            var terminal = _repository.SetConfigNotification(terminalAlertIngnoredViewModel);

            return RedirectToAction("Details/" + terminal.Id);
        }

        [HttpPost]
        public ActionResult SetWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel, string WorkingHoursEdit)
        {
            var terminal = _repository.SetWorkingHours(terminalAlertIngnoredViewModel, WorkingHoursEdit);

            return RedirectToAction("Details/" + terminal.Id);
        }
        [HttpPost]
        public ActionResult AddWorkingHours(TerminalAlertIngnoredViewModel terminalAlertIngnoredViewModel)
        {
            var terminal = _repository.AddWorkingHours(terminalAlertIngnoredViewModel);

            return RedirectToAction("Details/" + terminal.Id);
        }
        #endregion

    }
}
