using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
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
                return View(_partnerRepository.RenderPartnerFormViewModel());
            }
            _partnerRepository.SavePartner(viewModel, "Create");
            return RedirectToAction("Index");
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
            _partnerRepository.SavePartner(viewModel, "Edit");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _partnerRepository.DeletePartner(id);
            return RedirectToAction("Index");
        }

    }
}