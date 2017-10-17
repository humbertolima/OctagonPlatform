using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OctagonPlatform.Models;
using Newtonsoft.Json;
using OctagonPlatform.Models.InterfacesRepository;
using System.Web.Helpers;
using System;

namespace ApiConfig.Controllers
{
    public class TerminalAlertsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

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


            var p = alerts.Content.ReadAsStringAsync().Result;
            List<KeyValuePair<string, string>> list = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(p);
            TerminalAlert terminalAlert = new TerminalAlert();

            //terminalAlert.TerminalId = list.FirstOrDefault(c => c.Key == "TerminalId").Value;
            //terminalAlert.CashAvailable = Convert.ToInt32(list.FirstOrDefault(c => c.Key == "CashAvailable").Value);
            foreach (var item in prueba)
            {
                terminalAlert.GetType().GetProperty(item.Key).SetValue(terminalAlert, item.Value);
            }

            db.TerminalAlerts.Add(terminalAlert);
            //db.SaveChanges();
            try
            {
                //enviar el correo a los que tengan configurado que les llegue las alertas.
                var query = db.Terminals
                    .Include(x => x.Users)
                    .Include(x => x.TerminalAlertConfigs)
                    .Include(x => x.TerminalAlerts)
                    .FirstOrDefault(c => c.TerminalId == terminalAlert.TerminalId);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // DELETE: api/TerminalAlerts/5
        [ResponseType(typeof(TerminalAlert))]
        public async Task<IHttpActionResult> DeleteTerminalAlert(int id)
        {
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            if (terminalAlert == null)
            {
                return NotFound();
            }

            db.TerminalAlerts.Remove(terminalAlert);
            await db.SaveChangesAsync();

            return Ok(terminalAlert);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerminalAlertExists(int id)
        {
            return db.TerminalAlerts.Count(e => e.Id == id) > 0;
        }
    }
}