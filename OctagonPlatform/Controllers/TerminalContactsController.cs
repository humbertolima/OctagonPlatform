using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
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
        // GET: TerminalContacts
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        return View(_terminalContactRepository.GetAllTerminalContacts(TODO));
        //    }
        //    catch (Exception ex)
        //    {
        //        return HttpNotFound(ex.Message + ", Page Not Found!!!");
        //    }
        //}

        public ActionResult Details(int id)
        {
            try
            {
                return View(_terminalContactRepository.Details(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
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
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
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
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
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
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
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
            try
            {
                return View(_terminalContactRepository.TerminalContactToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
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

        //[HttpPost]
        //public ActionResult Search(string search)
        //{
        //    try
        //    {
        //        return PartialView(_terminalContactRepository.Search(search, TODO));
        //    }
        //    catch (Exception ex)
        //    {
        //        return HttpNotFound(ex.Message + ", Page Not Found!!!");
        //    }
        //}


    }
}
