using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalAlertIngnoredViewModel
    {
       [Required]
        public string TerminalId { get; set; }

        [Required]
        public double LowCach1 { get; set; }

        public double? LowCash2 { get; set; }

        public double? LowCash3 { get; set; }

        public DateTime? InactivePeriod { get; set; }

        public bool IgnoreChestDoorOpen { get; set; }
        public bool IgnoreTopDoorOpen { get; set; }
        public bool IgnoreReceiptPaper { get; set; }
        public bool IgnoreReceiptRibbon { get; set; }
        public bool IgnoreJournalPaper { get; set; }
        public bool IgnoreJournalRibbon { get; set; }
        public bool IgnoreCassetteNotes { get; set; }
        public bool IgnoreReceiptNeedAttention { get; set; }
        public bool IgnoreJournalNeedAttention { get; set; }
        public bool IgnoreBillDispenserNeedAttention { get; set; }
        public bool IgnoreCommNeedAttention { get; set; }
        public bool IgnoreCardReaderNeedAttention { get; set; }
    }
}