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

        [Route("PostAlert")]
        public HttpResponseMessage PostTerminalAlert(HttpRequestMessage alerts)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            List<KeyValuePair<string, string>> prueba = new List<KeyValuePair<string, string>>();
            prueba.Add(new KeyValuePair<string, string>("TerminalId", "TR024019"));
            prueba.Add(new KeyValuePair<string, string>("CashAvailable", "345"));


            //var p = alerts.Content.ReadAsStringAsync().Result;
            //List<KeyValuePair<string, string>> list = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(p);

            _repository.SaveAlerts(prueba);
            HttpResponseMessage respo = new HttpResponseMessage(HttpStatusCode.OK);

            return respo;

        }
        [Route("CompareSurcharge")]
        public void PostSendSurcharge(HttpRequestMessage alerts)
        {
            var p = alerts.Content.ReadAsStringAsync().Result;
            //esto devuelve el surcharge de la terminal. tiene que conicidir con lo que me manda el ATM
        }


        [Route("prueba")]
        public string GetPrueba()
        {
            return "fff";
        }
    }
}
