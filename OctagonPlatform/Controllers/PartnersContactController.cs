using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
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
                return View(new PartnerFormViewModel());
            }
            _partnerContactRepository.SavePartner(viewModel, "Create");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_partnerContactRepository.PartnerToEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerContactFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(_partnerContactRepository.PartnerToEdit(viewModel.Id));
            }
            _partnerContactRepository.SavePartner(viewModel, "Edit");
            return RedirectToAction("Index");
        }

        //public ActionResult Details(int id)
        //{
        //    return View(_partnerContactRepository.PartnerContactDetails(id));
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _partnerContactRepository.DeletePartner(id);
            return RedirectToAction("Index");
        }
    }
}