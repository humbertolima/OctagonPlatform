using ApiConfig.Models;
using OctagonPlatform.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiConfig.Controllers
{
    public class TerminalAlertConfigsController : ApiController
    {
        private OctagonPlatform.Models.ApplicationDbContext db = new OctagonPlatform.Models.ApplicationDbContext();

        // GET: api/TerminalAlertConfigs
        public List<TAConfigVModel> GetTerminalAlertConfigs()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var result = db.TerminalAlertConfigs.Include(c => c.MessagesToIgnored).ToList();
            List<TAConfigVModel> tACViewModel = new List<TAConfigVModel>();
            List<TerminalMViewModel> tMViewModel = new List<TerminalMViewModel>();

            foreach (var tAConfig in result)
            {
                foreach (var message in tAConfig.MessagesToIgnored)
                {
                    tMViewModel.Add(new TerminalMViewModel
                    {
                        Id = message.Id, Id_8583 = message.Id_8583, Name = message.Name, TerminalAlertConfigId = message.TerminalAlertConfigId
                    });
                }
                tACViewModel.Add(new TAConfigVModel
                {
                    Id = tAConfig.Id,
                    InactivePeriod = tAConfig.InactivePeriod,
                    LowCach1 = tAConfig.LowCach1,
                    LowCash2 = tAConfig.LowCash2,
                    LowCash3 = tAConfig.LowCash3,
                    MessagesToIgnored = tMViewModel,        //pendiente cambiar al mapper
                });
            }
            return tACViewModel;
        }

        // GET: api/TerminalAlertConfigs/5
        [ResponseType(typeof(TerminalAlertConfig))]
        public async Task<IHttpActionResult> GetTerminalAlertConfig(int id)
        {
            TerminalAlertConfig terminalAlertConfig = await db.TerminalAlertConfigs.FindAsync(id);
            if (terminalAlertConfig == null)
            {
                return NotFound();
            }

            return Ok(terminalAlertConfig);
        }

        // PUT: api/TerminalAlertConfigs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTerminalAlertConfig(int id, TerminalAlertConfig terminalAlertConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != terminalAlertConfig.Id)
            {
                return BadRequest();
            }

            db.Entry(terminalAlertConfig).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminalAlertConfigExists(id))
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

        // POST: api/TerminalAlertConfigs
        [ResponseType(typeof(TerminalAlertConfig))]
        public async Task<IHttpActionResult> PostTerminalAlertConfig(TerminalAlertConfig terminalAlertConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TerminalAlertConfigs.Add(terminalAlertConfig);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = terminalAlertConfig.Id }, terminalAlertConfig);
        }

        // DELETE: api/TerminalAlertConfigs/5
        [ResponseType(typeof(TerminalAlertConfig))]
        public async Task<IHttpActionResult> DeleteTerminalAlertConfig(int id)
        {
            TerminalAlertConfig terminalAlertConfig = await db.TerminalAlertConfigs.FindAsync(id);
            if (terminalAlertConfig == null)
            {
                return NotFound();
            }

            db.TerminalAlertConfigs.Remove(terminalAlertConfig);
            await db.SaveChangesAsync();

            return Ok(terminalAlertConfig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerminalAlertConfigExists(int id)
        {
            return db.TerminalAlertConfigs.Count(e => e.Id == id) > 0;
        }
    }
}