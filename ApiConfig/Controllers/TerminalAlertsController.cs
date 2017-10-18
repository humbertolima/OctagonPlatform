using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OctagonPlatform.Models;
using System.Web.Helpers;
using System;
using ApiConfig.Models.InterfacesRepository;

namespace ApiConfig.Controllers
{
    public class TerminalAlertsController : ApiController
    {
        private ITerminalAlertRepo _repository;
        public TerminalAlertsController(ITerminalAlertRepo repository)
        {
            _repository = repository;
        }
        public TerminalAlertsController()
        {
            
        }
        public void PostSendSurcharge(HttpRequestMessage alerts)
        {
            var p = alerts.Content.ReadAsStringAsync().Result;
            //esto devuelve el surcharge de la terminal. tiene que conicidir con lo que me manda el ATM
        }

        // POST: api/TerminalAlerts
        //[ResponseType(typeof(TerminalAlert))]
        public void PostTerminalAlert(HttpRequestMessage alerts)
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
            //terminalAlert.TerminalId = list.FirstOrDefault(c => c.Key == "TerminalId").Value;
            //terminalAlert.CashAvailable = Convert.ToInt32(list.FirstOrDefault(c => c.Key == "CashAvailable").Value);
            
            //db.SaveChanges();
            

        }
        [Route("api/terminalAlerts/prueba")]
        public string GetPrueba()
        {
            return "fff";
        }
        // DELETE: api/TerminalAlerts/5
        [ResponseType(typeof(TerminalAlert))]
        public async Task<IHttpActionResult> DeleteTerminalAlert(int id)
        {
            throw new NotImplementedException();
            //TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            //if (terminalAlert == null)
            //{
            //    return NotFound();
            //}

            //db.TerminalAlerts.Remove(terminalAlert);
            //await db.SaveChangesAsync();

            //return Ok(terminalAlert);
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
            //    if (disposing)
            //    {
            //        db.Dispose();
            //    }
            //    base.Dispose(disposing);
        }

        private bool TerminalAlertExists(int id)
        {
            throw new NotImplementedException();
            //return db.TerminalAlerts.Count(e => e.Id == id) > 0;
        }
    }
}