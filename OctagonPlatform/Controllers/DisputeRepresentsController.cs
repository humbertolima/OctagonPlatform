using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class DisputeRepresentsController : Controller
    {
        private readonly IDisputeRepresentRepository _repository;

        public DisputeRepresentsController(IDisputeRepresentRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create(int id)
        {
            var viewModel = new DisputeRepresentVM() { disputeId = id };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DisputeRepresentVM viewModel)
        {
            //validar si el modelo no es valido. pendiente
            _repository.AddRepresent(viewModel);


            //pendiente a ver donde se va a redireccionar.
            return RedirectToAction("Index", "Dispute");
        }
        [HttpPost]
        public ActionResult Upload(DisputeRepresentVM viewModel)
        {


            return View(viewModel);

        }

    }
}








