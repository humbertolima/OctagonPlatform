using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class TerminalAlertConfig
    {
        [Key]
        public int Id { get; set; }

        //pendiente
        //public int TerminalId { get; set; }      //este sera el enlace con emi.
        //public ICollection<Terminal> Terminals { get; set; }

        [Required]
        public double LowCach1 { get; set; }

        public double? LowCash2 { get; set; }

        public double? LowCash3 { get; set; }

        public DateTime? InactivePeriod { get; set; }

        [JsonIgnore]
        public ICollection<TerminalMessage> MessagesToIgnored { get; set; }
    }
}