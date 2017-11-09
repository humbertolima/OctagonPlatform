using System.Web.Mvc;
using OctagonPlatform.Models.InterfacesRepository;
using OctagonPlatform.Models;
using System;

namespace OctagonPlatform.Controllers
{
    public class DisputesController : Controller
    {
        private readonly IDisputeRepository _disputeRepository;

        public DisputesController(IDisputeRepository repository)
        {
            _disputeRepository = repository;
        }

        public ActionResult Index()
        {
            var disputes = _disputeRepository.GetAllDispute();
            return View(disputes);
        }




        public ActionResult Create(string terminalId)
        {
            try
            {
                Transaction transaction = _disputeRepository.GetTerminalTransaction(terminalId);

                Dispute dispute = new Dispute { Transaction = transaction };

                if (transaction != null) return View();
                ViewBag.Error = "Terminal not found. ";
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
