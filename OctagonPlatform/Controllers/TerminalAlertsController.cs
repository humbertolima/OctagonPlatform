using OctagonPlatform.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OctagonPlatform.Controllers
{
    public class TerminalAlertsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: TerminalAlerts
        public async Task<ActionResult> Index()
        {
            return View(await _db.TerminalAlerts.ToListAsync());
        }

        // GET: TerminalAlerts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Alert not found. ";
                    return View("Error");
                }
                var terminalAlert = await _db.TerminalAlerts.FindAsync(id);
                if (terminalAlert != null) return View(terminalAlert);
                ViewBag.Error = "Alert not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            
        }

        // GET: TerminalAlerts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerminalAlerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TerminalId,Notificated,CashAvailable,AlarmChestdooropen,AlarmTopdooropen,AlarmSupervisoractive,Receiptprinterpaperstatus,ReceiptPrinterRibbonStatus,JournalPrinterPaperStatus,JournalPrinterRibbonStatus,NoteStatusDispenser,ReceiptPrinter,JournalPrinter,Dispenser,CommunicationsSystem,CardReader")] TerminalAlert terminalAlert)
        {
            try
            {
                if (!ModelState.IsValid) return View(terminalAlert);

                _db.TerminalAlerts.Add(terminalAlert);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(terminalAlert);
            }
            
        }

        // GET: TerminalAlerts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Alert not found. ";
                    return View("Error");
                }
                var terminalAlert = await _db.TerminalAlerts.FindAsync(id);

                if (terminalAlert != null) return View(terminalAlert);

                ViewBag.Error = "Alert not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            
        }

        // POST: TerminalAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TerminalId,Notificated,CashAvailable,AlarmChestdooropen,AlarmTopdooropen,AlarmSupervisoractive,Receiptprinterpaperstatus,ReceiptPrinterRibbonStatus,JournalPrinterPaperStatus,JournalPrinterRibbonStatus,NoteStatusDispenser,ReceiptPrinter,JournalPrinter,Dispenser,CommunicationsSystem,CardReader")] TerminalAlert terminalAlert)
        {
            try
            {
                if (!ModelState.IsValid) return View(terminalAlert);

                _db.Entry(terminalAlert).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(terminalAlert);
            }
            
        }

        // GET: TerminalAlerts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Alert not found. ";
                    return View("Error");
                }
                var terminalAlert = await _db.TerminalAlerts.FindAsync(id);

                if (terminalAlert != null) return View(terminalAlert);

                ViewBag.Error = "Alert not found. ";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            
        }

        // POST: TerminalAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Error = "Alert not found. ";
                    return View("Error");
                }
                var terminalAlert = await _db.TerminalAlerts.FindAsync(id);

                if (terminalAlert != null) _db.TerminalAlerts.Remove(terminalAlert);

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
