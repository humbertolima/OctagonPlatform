using OctagonPlatform.Helpers;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class TerminalContactsController : Controller
    {
        private readonly ITerminalContactRepository _terminalContactRepository;

        public TerminalContactsController(ITerminalContactRepository repository)
        {
            _terminalContactRepository = repository;
        }


        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.GetContacts)]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id != null) return View(_terminalContactRepository.Details((int)id));
                ViewBag.Error = "Contact not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.AddContacts)]
        public ActionResult Create(int? id)
        {
            try
            {
                if (id != null)
                {
                    TerminalContactFormViewModel model = _terminalContactRepository.RenderTerminalContactFormViewModel((int)id);
                    return PartialView("Modal/AddTerminalContact", model);
                }

                ViewBag.Error = "Contact not found. ";
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
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.AddContacts)]
        public ActionResult Create(TerminalContactFormViewModel terminalContactFormViewModel)
        {
            if (!User.IsInRole("Add Terminal Contacts"))
            {
                ViewBag.Error = "Access Denied";
                return PartialView("../Shared/ErrorWithLayout");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
                }

                _terminalContactRepository.SaveTerminalContact(terminalContactFormViewModel, "Save");
                return RedirectToAction("Details", "Terminals", new { id = terminalContactFormViewModel.TerminalId });


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
        }

        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.EditContacts)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_terminalContactRepository.TerminalContactToEdit((int)id));
                ViewBag.Error = "Contact not found. ";
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
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.EditContacts)]
        public ActionResult Edit(TerminalContactFormViewModel terminalContactFormViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(_terminalContactRepository.TerminalContactToEdit(terminalContactFormViewModel.Id));
                }
                _terminalContactRepository.SaveTerminalContact(terminalContactFormViewModel, "Edit");
                return RedirectToAction("Details", "Terminals", new { id = terminalContactFormViewModel.TerminalId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_terminalContactRepository.TerminalContactToEdit(terminalContactFormViewModel.Id));
            }
        }


        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.DeleteContacts)]
        public ActionResult Delete(int? id)
        {
            
            try
            {
                if (id != null) return View(_terminalContactRepository.TerminalContactToEdit((int)id));
                ViewBag.Error = "Contact not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // POST: TerminalContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Terminals.DeleteContacts)]
        public ActionResult DeleteConfirmed(int? id, int? terminalId)
        {
            if (!User.IsInRole("Delete Terminal Contacts"))
            {
                ViewBag.Error = "Access Denied";
                return PartialView("../Shared/ErrorWithLayout");
            }
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Contact not found. ";
                    return View("Error");
                }
                _terminalContactRepository.DeleteTerminalContact((int)id);
                return RedirectToAction("Details", "Terminals", new { id = terminalId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleiting Terminal, " + ex.Message;
                return RedirectToAction("Details", "Terminals", new { id = terminalId });
            }
        }



    }
}
