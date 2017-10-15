using System;
using System.Collections.Generic;
using System.Data;
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

namespace ApiConfig.Controllers
{
    public class TerminalAlertsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TerminalAlerts
        public IQueryable<TerminalAlert> GetTerminalAlerts()
        {
            return db.TerminalAlerts;
        }

        // GET: api/TerminalAlerts/5
        [ResponseType(typeof(TerminalAlert))]
        public async Task<IHttpActionResult> GetTerminalAlert(int id)
        {
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            if (terminalAlert == null)
            {
                return NotFound();
            }

            return Ok(terminalAlert);
        }

        // PUT: api/TerminalAlerts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTerminalAlert(int id, TerminalAlert terminalAlert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != terminalAlert.Id)
            {
                return BadRequest();
            }

            db.Entry(terminalAlert).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminalAlertExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TerminalAlerts
        //[ResponseType(typeof(TerminalAlert))]
        public void PostTerminalAlert(HttpRequestMessage alerts)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            ////}
            //List<KeyValuePair<string, string>> prueba = new List<KeyValuePair<string, string>>();
            //prueba.Add(new KeyValuePair<string, string>("TerminalId", "TR024019"));
            //prueba.Add(new KeyValuePair<string, string>("CashAvailable", "345"));


            var p = alerts.Content.ReadAsStringAsync().Result;
            List<KeyValuePair<string, string>> list = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(p);
            TerminalAlert terminalAlert = new TerminalAlert();

           // terminalAlert.TerminalId = list.FirstOrDefault(c => c.Key == "TerminalId").Value;
           // terminalAlert.CashAvailable = Convert.ToInt32(list.FirstOrDefault(c => c.Key == "CashAvailable").Value);
            foreach (var item in list)
            {
                terminalAlert.GetType().GetProperty(item.Key).SetValue(terminalAlert,item.Value);
            }

           



            db.TerminalAlerts.Add(terminalAlert);
            db.SaveChanges();
            
            //return CreatedAtRoute("DefaultApi", new { id = terminalAlert.Id }, terminalAlert);
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