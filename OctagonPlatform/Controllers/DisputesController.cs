using System.Web.Mvc;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.Models;
using System;
using OctagonPlatform.Models.FormsViewModels;

namespace OctagonPlatform.Controllers
{
    public class DisputesController : Controller
    {
        private readonly IDisputeRepository _DisputeRepository;
        private readonly ITerminalRepository _TerminalRepository;

        public DisputesController(IDisputeRepository repository, ITerminalRepository terminalRepository)
        {
            _DisputeRepository = repository;
            _TerminalRepository = terminalRepository;
        }

        public ActionResult Index()
        {
            var disputes = _DisputeRepository.GetAllDispute();
            return View(disputes);
        }


        //------------- CREATEEE

        public ActionResult Create(string terminalId)
        {
            try
            {
                var disputeVM = _DisputeRepository.GetTerminalTransaction(terminalId);

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

                viewModel.Terminal = _TerminalRepository.GetTerminal(viewModel.TerminalId);
                _DisputeRepository.DisputeAdd(viewModel);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Validation error Create Dispute, "
                                + ex.Message;
                return View(viewModel);
            }
        }

        //HASTA aqui el CREATTEEE



        public ActionResult Details(int id)
        {
            try
            {
                var viewModel = _DisputeRepository.GetDispute(id);

                return View(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var viewModel = _DisputeRepository.GetDispute(id);
                //viewModel.Terminal = _TerminalRepository.GetTerminal(viewModel.TerminalId);

                return View(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DisputeViewModel viewModel)
        {
            try
            {
                //    viewModel.Terminal.TerminalAlertConfigs = null;
                //        viewModel.Terminal = _TerminalRepository.GetTerminal(viewModel.Terminal.TerminalId);
                _DisputeRepository.DisputeUpdate(viewModel);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}











