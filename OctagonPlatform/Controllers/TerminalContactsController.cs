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

        // GET: TerminalContacts/Create
        [HttpGet]
        public ActionResult Create(int? terminalId)
        {
            try
            {
                if (terminalId != null)
                {
                    var model = _terminalContactRepository.RenderTerminalContactFormViewModel((int)terminalId);
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
        public ActionResult Create(TerminalContactFormViewModel terminalContactFormViewModel)
        {
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

        // GET: TerminalContacts/Edit/5
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
        public ActionResult DeleteConfirmed(int? id, int? terminalId)
        {
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
