using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
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
            try
            {
                return View(_partnerRepository.GetAllPartners());
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpGet]
        public ActionResult Create(int partnerId)
        {
            try
            {
                return View(_partnerRepository.RenderPartnerFormViewModel(partnerId));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PartnerFormViewModel viewModel)
        {
            try
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
                    ViewBag.Error = "Validation error creating Partner " + exDb.Message
                                    + " Business Name, email or mobile phone must be unique, make sure that they are not already in use";
                    return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Validation error creating Partner "
                                    + ex.Message +
                                    " Business Name, email or mobile phone must be unique, make sure that they are not already in use";
                    return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                return View(_partnerRepository.PartnerToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerFormViewModel viewModel)
        {
            try
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
                    ViewBag.Error = "Validation error creating Partner " + exDb.Message +
                                    " Business Name, email or mobile phone must be unique, make sure that they are not already in use";
                    return View(_partnerRepository.PartnerToEdit(viewModel.Id));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Validation error editing Partner " + ex.Message +
                                    " Business Name, email or mobile phone must be unique, make sure that they are not already in use";
                    return View(_partnerRepository.PartnerToEdit(viewModel.Id));
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(_partnerRepository.PartnerDetails(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                return View(_partnerRepository.PartnerToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _partnerRepository.DeletePartner(id);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException exDb)
            {
                ViewBag.Error = "Validation error deleting Partner" + exDb.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error deleting Partner" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_partnerRepository.Search(search));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        #region PartnerPartialViews

        [HttpPost]
        public PartialViewResult Contacts(int partnerId)
        {
            var contacts = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/Contacts", contacts);

        }

        [HttpPost]
        public PartialViewResult GeneralInfo(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/GeneralInfo", partner);

        }

        [HttpPost]
        public PartialViewResult BankAccounts(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/BankAccounts", partner);

        }

        [HttpPost]
        public PartialViewResult Partners(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/Partners", partner);

        }

        [HttpPost]
        public PartialViewResult Terminals(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/Terminals", partner);

        }

        [HttpPost]
        public PartialViewResult Users(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            return PartialView("Sections/Users", partner);

        }

        #endregion

    }
}