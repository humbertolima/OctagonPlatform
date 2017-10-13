using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    public class TerminalAlert
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        public bool Notificated { get; set; }

        public string CashAvailable { get; set; }
        public string AlarmChestdooropen { get; set; }
        public string AlarmTopdooropen { get; set; }
        public string AlarmSupervisoractive { get; set; }
        public string Receiptprinterpaperstatus { get; set; }
        public string ReceiptPrinterRibbonStatus { get; set; }
        public string JournalPrinterPaperStatus { get; set; }
        public string JournalPrinterRibbonStatus { get; set; }
        public string NoteStatusDispenser { get; set; }
        public string ReceiptPrinter { get; set; }
        public string JournalPrinter { get; set; }
        public string Dispenser { get; set; }
        public string CommunicationsSystem { get; set; }
        public string CardReader { get; set; }

    }
}