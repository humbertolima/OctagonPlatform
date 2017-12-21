using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            var disputeRepresents = _repository.GetAllDispute();
            return View(disputeRepresents);
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
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Error model validate. ";
                return View(viewModel);
            }

            //if (System.IO.File.Exists(Server.MapPath("~/App_Data/" + fname)))
            //{
            //    System.IO.File.Delete(Server.MapPath("~/App_Data/" + fname));
            //}
            viewModel.relativePath = Server.MapPath("~/App_Data/");
            //attaches.Add(uploadedFile);

            var result = _repository.AddRepresent(viewModel);

            Helpers.Email.SendNotification(
                "yasser.osuna@gmail.com",
                "Represents to Dispute",
                "Dispute: " + viewModel.disputeId + "Terminal: falta poner el terminalId",
               result.Image
                );

            //pendiente a ver donde se va a redireccionar.
            return RedirectToAction("Index", "Disputes");
        }


        [HttpPost]
        public ActionResult Upload(DisputeRepresentVM viewModel)
        {
            
            return View(viewModel);

        }

    }
}








