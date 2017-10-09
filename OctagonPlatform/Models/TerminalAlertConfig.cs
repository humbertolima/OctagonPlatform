using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class TerminalAlertConfig
    {

        [Key, ForeignKey("Terminal")]
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

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