using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;

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
            var disputes = _disputeRepository.GetAllDispute().ToList();
            return View(disputes);
        }


    }
}
