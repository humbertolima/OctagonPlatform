using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class PartnersController : BaseController
    {

        private readonly IPartnerRepository _partnerRepository;

        public PartnersController(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }


        [HttpGet]
        //[CustomAuthorize(Roles = Helpers.Permissions.Partner.GetAllPatners)]
        public ActionResult Index()
        {
            try
            {
                return View(_partnerRepository.GetAllPartners(int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddPartners)]
        public ActionResult Create(int? partnerId)
        {
            try
            {
                if (partnerId != null) return View(_partnerRepository.RenderPartnerFormViewModel((int)partnerId));
                ViewBag.Error = "Partner not found. ";
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.AddPartners)]
        public ActionResult Create(PartnerFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
                }

                _partnerRepository.SavePartner(viewModel, "Create");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_partnerRepository.InitializeNewFormViewModel(viewModel));
            }

        }

        [HttpGet]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditPartners)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id != null) return View(_partnerRepository.PartnerToEdit((int) id));
                ViewBag.Error = "Partner not found. ";
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.EditPartners)]
        public ActionResult Edit(PartnerFormViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Model not valid, please check the entered values. ";
                    return View(_partnerRepository.PartnerToEdit(viewModel.Id));
                }

                _partnerRepository.SavePartner(viewModel, "Edit");
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_partnerRepository.PartnerToEdit(viewModel.Id));
            }
        }
        //[CustomAuthorize(Roles = Helpers.Permissions.Partner.GetPatners)]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id != null) return View(_partnerRepository.PartnerDetails((int) id));
                ViewBag.Error = "Partner not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (id != null) return View(_partnerRepository.PartnerToEdit((int) id));
        //        ViewBag.Error = "Partner not found. ";
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
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.DeletePartners)]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _partnerRepository.DeletePartner(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.SearchPartners)]
        public ActionResult Search(string search)
        {
            try
            {
                return PartialView(_partnerRepository.Search(search, int.Parse(Session["partnerId"].ToString())));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        #region PartnerPartialViews

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetContacts)]
        public PartialViewResult GetContacts(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            ViewBag.PartnerId = partnerId;

            ViewBag.IsAddPartnerContact = User.IsInRole(Helpers.Permissions.Partner.AddContacts);
            ViewBag.IsEditPartnerContact = User.IsInRole(Helpers.Permissions.Partner.EditContacts);
            ViewBag.IsDeletePartnerContact = User.IsInRole(Helpers.Permissions.Partner.DeleteContacts);


            return PartialView("Sections/Contacts", partner.PartnerContacts);

        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetPartners)]
        public PartialViewResult GeneralInfo(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);

            ViewBag.IsEditPartner = User.IsInRole(Helpers.Permissions.Partner.EditPartners);

            return PartialView("Sections/GeneralInfo", new Partner()
            {
                Id = partnerId,
                BusinessName = partner.BusinessName,
                Parent = partner.Parent,
                Address1 = partner.Address1,
                Address2 = partner.Address2,
                Country = partner.Country,
                State = partner.State,
                City = partner.City,
                CreatedAt = partner.CreatedAt,
                CreatedByName = partner.CreatedByName,
                Status = partner.Status,
                Email = partner.Email,
                Mobile = partner.Mobile
            });

        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetBankAccounts)]
        public PartialViewResult BankAccounts(int partnerId)
        {
            ViewBag.IsAddBankAccount = User.IsInRole(Helpers.Permissions.Partner.AddtBankAccounts);
            ViewBag.IsEditBankAccount = User.IsInRole(Helpers.Permissions.Partner.EditBankAccounts);
            ViewBag.IsDeleteBankAccount = User.IsInRole(Helpers.Permissions.Partner.DeleteBankAccounts);

            var partner = _partnerRepository.PartnerDetails(partnerId);
            ViewBag.PartnerId = partnerId;
            return PartialView("Sections/BankAccounts", partner.BankAccounts);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetPartners)]
        public PartialViewResult Partners(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            ViewBag.PartnerId = partnerId;
            ViewBag.IsAddPartner = User.IsInRole(Helpers.Permissions.Partner.AddPartners);
            ViewBag.IsEditPartner = User.IsInRole(Helpers.Permissions.Partner.EditPartners);
            ViewBag.IsDeletePartner = User.IsInRole(Helpers.Permissions.Partner.DeletePartners);

            return PartialView("Sections/Partners", partner.Partners);

        }

        [HttpPost]
        [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetTerminals)]
        public PartialViewResult Terminals(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            ViewBag.PartnerId = partnerId;
            return PartialView("Sections/Terminals", partner.Terminals);

        }

        [HttpPost]
       // [CustomAuthorize(Roles = Helpers.Permissions.Partner.GetUsers)]
        public PartialViewResult Users(int partnerId)
        {
            var partner = _partnerRepository.PartnerDetails(partnerId);
            ViewBag.PartnerId = partnerId;
            return PartialView("Sections/Users", partner.Users);

        }

        #endregion

    }
}