using OctagonPlatform.Helpers;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class PartnerContactsController : BaseController
    {
        private readonly IPartnerContactRepository _partnerContactRepository;

        public PartnerContactsController(IPartnerContactRepository partnerContactRepository)
        {
            _partnerContactRepository = partnerContactRepository;
        }

        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetContacts)]
        public ActionResult Index()
        {
            try
            {
                return View(_partnerContactRepository.GetAllPartners(int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddContacts)]
        public ActionResult Create(int? partnerId)
        {
            try
            {
                if (partnerId != null)
                    return View(_partnerContactRepository.RenderPartnerContactFormViewModel((int)partnerId));
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddContacts)]
        public ActionResult Create(PartnerContactFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_partnerContactRepository.InitializeNewFormViewModel(viewModel));
                }

                _partnerContactRepository.SavePartner(viewModel, "Create");
                return RedirectToAction("Details", "Partners", new {id = viewModel.PartnerId});


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_partnerContactRepository.RenderPartnerContactFormViewModel(viewModel.PartnerId));
            }
        }

        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditContacts)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_partnerContactRepository.PartnerContactToEdit((int)id));
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditContacts)]
        public ActionResult Edit(PartnerContactFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
                }

                _partnerContactRepository.SavePartner(viewModel, "Edit");
                return RedirectToAction("Details", "Partners", new {id = viewModel.PartnerId});

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_partnerContactRepository.PartnerContactToEdit(viewModel.Id));
            }
        }

        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (id != null) return View(_partnerContactRepository.PartnerContactToEdit((int)id));
        //        ViewBag.Error = "Contact not found. ";
        //        return View("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View("Error");
        //    }
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.DeleteContacts)]
        public ActionResult DeleteConfirmed(int? id, int? partnerId)
        {
            try
            {
                if (id == null || partnerId == null)
                {
                    ViewBag.Error = "Contact not found. ";
                    return View("Error");
                }

                _partnerContactRepository.DeletePartner((int)id);
                return RedirectToAction("Details", "Partners", new {id = partnerId});
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.SearchContacts)]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_partnerContactRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetContacts)]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id != null) return View(_partnerContactRepository.Details((int)id));
                ViewBag.Error = "Contact not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }
}