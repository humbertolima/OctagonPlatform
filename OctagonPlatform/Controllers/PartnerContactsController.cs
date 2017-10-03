using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
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
            try
            {
                return View(_partnerContactRepository.GetAllPartners(int.Parse(Session["partnerId"].ToString())));
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
                return View(_partnerContactRepository.RenderPartnerContactFormViewModel(partnerId));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PartnerContactFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
                }
                try
                {
                    _partnerContactRepository.SavePartner(viewModel, "Create");
                    return RedirectToAction("Details", "Partners", new {id = viewModel.PartnerId});
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
                return View(_partnerContactRepository.PartnerContactToEdit(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerContactFormViewModel viewModel)
        {
            try
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
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_partnerContactRepository.PartnerContactToEdit(id));
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
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_partnerContactRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
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
                return View(_partnerContactRepository.Details(id));
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message + ", Page Not Found!!!");
            }
        }
    }
}