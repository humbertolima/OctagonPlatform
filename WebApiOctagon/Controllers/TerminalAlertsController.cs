using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiOctagon.Repository.InterfacesRepository;

namespace WebApiOctagon.Controllers
{
    [RoutePrefix("api/Terminals")]
    public class TerminalAlertsController : ApiController
    {
        private ITerminalAlertRepo _repository;
        public TerminalAlertsController(ITerminalAlertRepo repository)
        {
            _repository = repository;
        }

        [Route("prueba")]
        public string GetPrueba()
        {
            return "fff";
        }
    }
}
