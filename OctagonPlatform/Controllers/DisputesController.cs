using System.Web.Mvc;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.Models;
using System;
using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Controllers
{
    public class DisputesController : Controller
    {
        private readonly IDisputeRepository _repository;

        public DisputesController(IDisputeRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var disputes = _repository.GetAllDispute();
            return View(disputes);
        }


        //------------- CREATEEE

        public ActionResult Create(string terminalId)
        {
            try
            {
                var disputeVM = _repository.GetTerminalTransaction(terminalId);

                if (disputeVM != null) return View("Create", disputeVM);
                ViewBag.Error = "Terminal not found. ";
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
        public ActionResult Create(DisputeViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Please check the entered values. ";
                    return View(viewModel);
                }


                _repository.DisputeAdd(viewModel);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error editing Terminal, "
                                + ex.Message;
                return View(viewModel);
            }
        }

        //HASTA aqui el CREATTEEE
    }
}
