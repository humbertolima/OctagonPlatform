using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class TerminalAlertConfig
    {

        [Key, ForeignKey("Terminal")]
        public int Id { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Low Cash 1")]
        public double LowCach1 { get; set; }

        [Display(Name = "Low Cash 2")]
        public double? LowCash2 { get; set; }

        [Display(Name = "Low Cash 3")]
        public double? LowCash3 { get; set; }

        [Display(Name = "Ignored Hours Inactive")]
        public int IgnoreHoursInactive { get; set; }

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