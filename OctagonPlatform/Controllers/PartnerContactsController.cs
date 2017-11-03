using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
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
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
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

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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