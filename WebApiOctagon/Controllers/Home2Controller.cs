using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiOctagon.Repository.InterfacesRepository;

namespace WebApiOctagon.Controllers
{
    [System.Web.Http.Route("api/home")]
    public class Home2Controller : ApiController
    {
        private ITerminalAlertRepo _repository;
        public Home2Controller(ITerminalAlertRepo repository)
        {
            _repository = repository;
        }

        [System.Web.Http.Route("prueba")]
        public string GetPrueba()
        {
            return "fff";
        }
        
    }
}
