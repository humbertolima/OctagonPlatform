using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class PartnerContactsController : Controller
    {
        private readonly IPartnerContactRepository _partnerContactRepository;

        public PartnerContactsController(IPartnerContactRepository partnerContactRepository)
        {
            _partnerContactRepository = partnerContactRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_partnerContactRepository.GetAllPartners());
        }

        [HttpGet]
        public ActionResult Create(int partnerId)
        {
            return View(_partnerContactRepository.RenderPartnerContactFormViewModel(partnerId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PartnerContactFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
            }
            try
            {
                _partnerContactRepository.SavePartner(viewModel, "Create");
                return RedirectToAction("Details", "Partners", new { id = viewModel.PartnerId });
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error creating Contact " + exDb.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error creating Contact " + ex.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_partnerContactRepository.PartnerContactToEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerContactFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
            try
            {
                _partnerContactRepository.SavePartner(viewModel, "Edit");
                return RedirectToAction("Details", "Partners", new {id = viewModel.PartnerId});
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error editing Contact " + exDb.Message +
                                " The email phone must be unique, make sure that is not already in use";
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Contact " + ex.Message +
                                " The email must be unique, make sure that is not already in use";
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
        }

        public ActionResult Delete(int id)
        {

            return View(_partnerContactRepository.PartnerContactToEdit(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _partnerContactRepository.DeletePartner(id);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error deleting Contact" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Contact" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            return PartialView(_partnerContactRepository.Search(search));
        }

        public ActionResult Details(int id)
        {
            return View(_partnerContactRepository.Details(id));
        }
    }
}