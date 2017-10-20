using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctagonPlatform.Models;

namespace OctagonPlatform.Controllers
{
    public class TerminalAlertsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TerminalAlerts
        public async Task<ActionResult> Index()
        {
            return View(await db.TerminalAlerts.ToListAsync());
        }

        // GET: TerminalAlerts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            if (terminalAlert == null)
            {
                return HttpNotFound();
            }
            return View(terminalAlert);
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
            if (ModelState.IsValid)
            {
                db.TerminalAlerts.Add(terminalAlert);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(terminalAlert);
        }

        // GET: TerminalAlerts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            if (terminalAlert == null)
            {
                return HttpNotFound();
            }
            return View(terminalAlert);
        }

        // POST: TerminalAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TerminalId,Notificated,CashAvailable,AlarmChestdooropen,AlarmTopdooropen,AlarmSupervisoractive,Receiptprinterpaperstatus,ReceiptPrinterRibbonStatus,JournalPrinterPaperStatus,JournalPrinterRibbonStatus,NoteStatusDispenser,ReceiptPrinter,JournalPrinter,Dispenser,CommunicationsSystem,CardReader")] TerminalAlert terminalAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terminalAlert).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(terminalAlert);
        }

        // GET: TerminalAlerts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            if (terminalAlert == null)
            {
                return HttpNotFound();
            }
            return View(terminalAlert);
        }

        // POST: TerminalAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TerminalAlert terminalAlert = await db.TerminalAlerts.FindAsync(id);
            db.TerminalAlerts.Remove(terminalAlert);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
