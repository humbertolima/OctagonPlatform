using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
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
           
            return View(_repository.GetAllTerminals());
        }

        // GET: Terminals/Details/5
        public ActionResult Details(int id)
        {
            var terminal = _repository.TerminalDetails(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // GET: Terminals/Create
        public ActionResult Create(int partnerId)
        {
            
            return View(_repository.RenderTerminalFormViewModel(partnerId));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TerminalFormViewModel terminalFormViewModel)
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

        // GET: Terminals/Edit/5
        public ActionResult Edit(int id)
        {

            return View(_repository.TerminalToEdit(id));
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
            
            return View(_repository.TerminalToEdit(id));
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
            return PartialView(_repository.Search(search));
        }

    }
}
