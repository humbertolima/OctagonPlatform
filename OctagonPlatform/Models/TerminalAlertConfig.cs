using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    public class TerminalAlertConfig
    {
        [Key]
        public int Id { get; set; }

        //pendiente
        //public Terminal TerminalId { get; set; }      //este sera el enlace con emi.
        
        [Required]
        public double LowCach1 { get; set; }

        public double? LowCash2 { get; set; }

        public double? LowCash3 { get; set; }

        public DateTime? InactivePeriod { get; set; }

        public ICollection<TerminalMessage> MessagesToIgnored { get; set; }
    }
}