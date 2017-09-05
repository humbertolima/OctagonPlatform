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
        public ActionResult Create()
        {
            return View(_partnerContactRepository.RenderPartnerContactFormViewModel());
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
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error in Data Base creating Partner " + exDb.Message;
                return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error in Data Base creating Partner " + ex.Message;
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
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error in Data Base edditing Partner " + exDb.Message;
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error edditing Partner " + ex.Message;
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
        }

        //public ActionResult Details(int id)
        //{
        //    return View(_partnerContactRepository.PartnerContactDetails(id));
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _partnerContactRepository.DeletePartner(id);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error in Data Base edditing Partner" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error edditing Partner" + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}