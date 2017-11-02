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
        

        public ActionResult Details(int id)
        {
            try
            {
                return View(_terminalContactRepository.Details(id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: TerminalContacts/Create
        public ActionResult Create(int terminalId)
        {
            try
            {
                return View(_terminalContactRepository.RenderTerminalContactFormViewModel(terminalId));
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
                return RedirectToAction("Details", "Terminals", new {id = terminalContactFormViewModel.TerminalId});


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        // GET: TerminalContacts/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_terminalContactRepository.TerminalContactToEdit(id));
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
                    return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
                }
                _terminalContactRepository.SaveTerminalContact(terminalContactFormViewModel, "Edit");
                return RedirectToAction("Details", "Terminals", new {id = terminalContactFormViewModel.TerminalId});
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_terminalContactRepository.TerminalContactToEdit(id));
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
        public ActionResult DeleteConfirmed(int id, int terminalId)
        {
            try
            {
                _terminalContactRepository.DeleteTerminalContact(id);
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
