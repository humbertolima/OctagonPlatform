using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class TerminalContactsController : Controller
    {
        private readonly ITerminalContactRepository _terminalContactRepository;

        public TerminalContactsController(ITerminalContactRepository repository)
        {
            _terminalContactRepository = repository;
        }
        // GET: TerminalContacts
        public ActionResult Index()
        {
            
            return View(_terminalContactRepository.GetAllTerminalContacts());
        }

        public ActionResult Details(int id)
        {
            return View(_terminalContactRepository.Details(id));
        }

        // GET: TerminalContacts/Create
        public ActionResult Create(int terminalId)
        {
            
            return View(_terminalContactRepository.RenderTerminalContactFormViewModel(terminalId));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TerminalContactFormViewModel terminalContactFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
            try
            {
                _terminalContactRepository.SaveTerminalContact(terminalContactFormViewModel, "Save");
                return RedirectToAction("Details", "Terminals", new {id = terminalContactFormViewModel.TerminalId});
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error creating Contact " + exDb.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error creating Contact " + ex.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
        }

        // GET: TerminalContacts/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View(_terminalContactRepository.TerminalContactToEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TerminalContactFormViewModel terminalContactFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
            try
            {
                _terminalContactRepository.SaveTerminalContact(terminalContactFormViewModel, "Edit");
                return RedirectToAction("Details", "Terminals", new { id = terminalContactFormViewModel.TerminalId });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing Contact " + exDb.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Contact " + ex.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_terminalContactRepository.InitializeNewFormViewModel(terminalContactFormViewModel));
            }
        }

        // GET: TerminalContacts/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_terminalContactRepository.TerminalContactToEdit(id));
        }

        // POST: TerminalContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int terminalId)
        {
            try
            {
                _terminalContactRepository.DeleteTerminalContact(id);
                return RedirectToAction("Details", "Partners", new { id = terminalId });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error deleting Contact" + exDb.Message;
                return RedirectToAction("Details", "Partners", new { id = terminalId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleiting Terminal" + ex.Message;
                return RedirectToAction("Details", "Partners", new { id = terminalId });
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            return PartialView(_terminalContactRepository.Search(search));
        }


    }
}
