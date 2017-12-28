﻿using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
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

        public ActionResult CashManagement(string terminalId)
        {
            DateTime start = DateTime.ParseExact(DateTime.Now.AddDays(-30).ToShortDateString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var result = _repository.GetCashLoad(start, end, terminalId);

            foreach (var item in result.Result)
            {

            }
            return PartialView("Sections/CashManagements", result.Result);
        }

        public ActionResult GetKey(string terminalId)
        {   // prueba de branch
            try
            {
                var result = _repository.GetKey(terminalId);
                var viewModel = new BindKeyViewModel()
                {
                    Serial1 = result.K1.Serial,
                    Serial2 = result.K2.Serial,
                    CheckDigt1 = result.K1.Kcv_PartAb,
                    CheckDigt2 = result.K2.Kcv_PartAb,
                    ATMCheckDigt = result.Checksum,
                    TerminalId = ""
                };

                return PartialView("Sections/BindKey", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public PartialViewResult GetInterchanges(TerminalInterchangeVM viewModel)
        {
            if (ModelState.IsValid)     //pendiente validar el model en todas los metodos del controladores.
            {
                viewModel = _repository.GetInterchanges(viewModel.Id);
            }
            else
            {
                ViewBag.Error = Helpers.ViewModelError.Get(ModelState);
            }

            return PartialView("Sections/Interchanges", viewModel);
        }


        [HttpPost]
        public PartialViewResult GetSurcharges(TerminalSurchargeVM viewModel)
        {
            if (ModelState.IsValid)     //pendiente validar el model en todas los metodos del controladores.
            {
                viewModel = _repository.GetSurcharges(viewModel.Id);
            }
            else
            {
                ViewBag.Error = Helpers.ViewModelError.Get(ModelState);
            }

            return PartialView("Sections/Surcharge", viewModel);
        }

        [HttpPost]
        public PartialViewResult GetVaultCash(TerminalVaultCashVM viewModel)
        {
            if (ModelState.IsValid)     //pendiente validar el model en todas los metodos del controladores.
            {
                viewModel = _repository.GetVaultCash(viewModel.Id);
            }
            else
            {
                ViewBag.Error = Helpers.ViewModelError.Get(ModelState);
            }

            return PartialView("Sections/VaultCash", viewModel);
        }

        [HttpPost]
        public PartialViewResult GetContacts(TerminalContactVM viewModel)
        {
            if (ModelState.IsValid)     //pendiente validar el model en todas los metodos del controladores.
            {
                viewModel = _repository.GetContacts(viewModel.Id);
            }
            else
            {
                ViewBag.Error = Helpers.ViewModelError.Get(ModelState);
            }

            return PartialView("Sections/Contacts", viewModel);
        }

        [HttpPost]
        public PartialViewResult GetPictures( TerminalPicturesVM viewModel)
        {
            if (viewModel.Id > 0)
            {
                viewModel = _repository.GetPictures(viewModel.Id);
            }
            else
            {
                ViewBag.Error = " parameter not recived";
            }

            return PartialView("Sections/Pictures", viewModel);
        }

        [HttpPost]
        public ViewResult SetPictures(int indexTerminalId, HttpPostedFileBase FileForm, int? pictureId)
        {
            Models.Terminal terminal = new Models.Terminal();

            if (pictureId == null || pictureId == 0)
            {   //addicionar
                terminal = _repository.SetPictures(indexTerminalId, FileForm, null);
            }
            else
            {   //editar porque viene un id de pictures

            }

            return View("Details", terminal);
        }

        [HttpPost]
        public PartialViewResult GetDocuments(TerminalDocumentsVM viewModel)
        {
            if (viewModel.Id > 0)
            {
                viewModel = _repository.GetDocuments(viewModel.Id);
            }

            return PartialView("Sections/Documents", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult SetDocuments(int indexTerminalId, HttpPostedFileBase FileForm, int? documentId)
        {
            Models.Terminal terminal = new Models.Terminal();

            if (FileForm != null)
            {
                if (documentId == null || documentId == 0)
                {   //addicionar
                    terminal = _repository.SetDocuments(indexTerminalId, FileForm, null);
                }
                else
                {   //editar porque viene un id de documents

                }

                return PartialView("Details", terminal);
            }
            else
            {
                return PartialView("Details", terminal);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult DocumentDelete(TerminalDocumentsVM viewModel, int documentId)
        {
            Models.Terminal terminal;
            try
            {
                if (documentId > 0)
                {
                    terminal = _repository.DocumentDelete(viewModel.Id, Convert.ToInt32(documentId));
                }
                else
                {   //creo el objeto terminal con los datos del viewmodel.
                    terminal = AutoMapper.Mapper.Map<TerminalDocumentsVM, Models.Terminal>(viewModel);
                    ViewBag.Error = " not value to Id";
                }

                return PartialView("Details", terminal);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Document" + ex.Message;
                return PartialView("Details", terminal = AutoMapper.Mapper.Map<TerminalDocumentsVM, Models.Terminal>(viewModel));
            }
        }


        [HttpPost]
        public PartialViewResult GetNotes(int id)
        {
            TerminalNotesVM viewModel;
            if (id > 0)
            {
                viewModel = _repository.GetNotes(id);
            }
            else
            {
                viewModel = new TerminalNotesVM();
            }

            return PartialView("Sections/Notes", viewModel);
        }


        [HttpPost]
        public PartialViewResult SetNotes(int id, string notes, int? noteId)
        {
            Models.Terminal terminal;

            if (noteId != null && noteId > 0)
            {
                terminal = _repository.SetNotes(id, notes, Convert.ToInt32(noteId));
            }
            else
            {
                terminal = _repository.SetNotes(id, notes, null);
            }

            return PartialView("Details", terminal);
        }

        [HttpPost]
        public PartialViewResult DeleteNotes(int indexTerminalId, int noteId)
        {
            Models.Terminal terminal = new Models.Terminal();

            if (noteId > 0)
            {
                terminal = _repository.DeleteNotes(indexTerminalId, noteId);
            }

            return PartialView("Details", terminal);
        }

        [HttpPost]
        public PartialViewResult GetCassettes(string id)
        {
            try
            {
                TerminalCassetteVM viewModel = new TerminalCassetteVM();

                if (!string.IsNullOrEmpty(id))
                {
                    viewModel = _repository.GetCassettes(Convert.ToInt32(id));
                }
                return PartialView("Sections/Cassettes", viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Get Configuration error. " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SetCassettes(string autoRecord, int denomination, int id, int? cassetteId)
        {
            bool isAutoRecord;

            if (!String.IsNullOrEmpty(autoRecord) ? isAutoRecord = true : isAutoRecord = false) ;

            Models.Terminal terminal = new Models.Terminal();

            if (cassetteId != null && cassetteId > 0)    //si viene el ID del cassette es porque se le dio al boton editar.
            {
                terminal = _repository.CassettesEdit(isAutoRecord, denomination, id, Convert.ToInt32(cassetteId));
            }
            else
            {
                terminal = _repository.CassettesEdit(isAutoRecord, denomination, id, null);
            }

            return View("Details", terminal);
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
                if (partnerId != null) return View(_repository.RenderTerminalFormViewModel((int)partnerId));
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
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_repository.TerminalToEdit((int)id));
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
                if (id != null) return View(_repository.TerminalToEdit((int)id));
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
        public ActionResult DeleteConfirmed(int? id, string WorkingHoursId)
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
        //======================
        // POST: Terminals/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CassetteDelete(int? cassetteId, int id)
        {
            try
            {
                if (cassetteId == null)
                {
                    ViewBag.Error = "Terminal not found. ";
                    return View("Error");
                }
                _repository.CassettesDelete(Convert.ToInt32(cassetteId));

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Terminal" + ex.Message;

                return RedirectToAction("Details", new { id = id });
            }
        }
        //======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureDelete(int id, int? pictureId)
        {
            try
            {
                if (pictureId == null || pictureId <= 0)
                {
                    ViewBag.Error = "Picture not found. ";
                    return View("Error");
                }
                Models.Terminal terminal = _repository.PictureDelete(id, Convert.ToInt32(pictureId));

                return RedirectToAction("Details", new { id = terminal.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Document" + ex.Message;
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
        public PartialViewResult GetConfiguration(string id)
        {
            try
            {
                TerminalConfigViewModel configViewModel = new TerminalConfigViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    configViewModel = _repository.GetConfigNotification(Convert.ToInt32(id));
                }
                return PartialView("Sections/ConfigNotification", configViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Get Configuration error. " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SetConfiguration(TerminalConfigViewModel terminalConfigViewModel)
        {
            TerminalConfigViewModel configViewModel = _repository.SetConfiguration(terminalConfigViewModel);

            return RedirectToAction("Details", new { id = configViewModel.Id });
        }

        [HttpPost]
        public ActionResult SetWorkingHours(Models.FormsViewModels.TerminalConfigViewModel terminalAlertIngnoredViewModel, string WorkingHoursEdit)
        {
            var terminal = _repository.SetWorkingHours(terminalAlertIngnoredViewModel, WorkingHoursEdit);

            return RedirectToAction("Details/" + terminal.Id);
        }
        [HttpPost]
        public ActionResult AddWorkingHours(Models.FormsViewModels.TerminalConfigViewModel terminalAlertIngnoredViewModel)
        {
            var terminal = _repository.AddWorkingHours(terminalAlertIngnoredViewModel);

            return RedirectToAction("Details/" + terminal.Id);
        }

        [HttpPost]
        public ActionResult DeleteWorkingHours(string terminalId, string workingHoursId)
        {
            int workingHoursIdConvert = 0;
            int terminalIdConvert = 0;

            if (!String.IsNullOrEmpty(workingHoursId) || (!String.IsNullOrEmpty(terminalId)))
            {
                workingHoursIdConvert = Convert.ToInt32(workingHoursId);
                terminalIdConvert = Convert.ToInt32(terminalId);
            }
            else
            {
                throw new Exception(" Terminal Id or WorkingHours Id is null or empty");
            }

            var terminal = _repository.DeteteWorkingHours(terminalIdConvert, workingHoursIdConvert);

            return RedirectToAction("Details/" + terminal.Id);
        }
        #endregion

    }
}
