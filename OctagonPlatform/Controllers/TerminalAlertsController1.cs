using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    [Authorize]
    public class TerminalAlertsController1 : Controller
    {
        private readonly ITerminalAlertRepositoryAPI _repository;

        public TerminalAlertsController1(ITerminalAlertRepositoryAPI repository)
        {
            _repository = repository;
        }
        // GET: TerminalAlerts
        public ActionResult Index()
        {
            var alerts = _repository.GetAllAlerts();
            return View(alerts);
        }
    }
}