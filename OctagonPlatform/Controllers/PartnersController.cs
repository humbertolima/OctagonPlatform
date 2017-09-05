using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [AllowAnonymous]
    public class PartnersController : Controller
    {

        private readonly IPartnerRepository _partnerRepository;

        public PartnersController(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_partnerRepository.GetAllPartners());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(_partnerRepository.RenderPartnerFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PartnerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
            }
            try
            {
                _partnerRepository.SavePartner(viewModel, "Create");
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error in Data Base creating Partner " + exDb.Message;
                return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error in Data Base creating Partner " + ex.Message;
                return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
            }
           
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_partnerRepository.PartnerToEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(_partnerRepository.PartnerToEdit(viewModel.Id));
            }
            try
            {
                _partnerRepository.SavePartner(viewModel, "Edit");
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error in Data Base edditing Partner " + exDb.Message;
                return View(_partnerRepository.PartnerToEdit(viewModel.Id));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error edditing Partner " + ex.Message;
                return View(_partnerRepository.PartnerToEdit(viewModel.Id));
            }
        }

        public ActionResult Details(int id)
        {
            return View(_partnerRepository.PartnerDetails(id));
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _partnerRepository.DeletePartner(id);
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