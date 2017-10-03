using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class TerminalAlertConfig
    {
        public TerminalAlertConfig()
        {
            this.MessagesToIgnored = new List<TerminalMessage>();
        }
        [Key]
        public int Id { get; set; }
        
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        [Required]
        public double LowCach1 { get; set; }

        public double? LowCash2 { get; set; }

        public double? LowCash3 { get; set; }

        public DateTime? InactivePeriod { get; set; }

        [JsonIgnore]
        public ICollection<TerminalMessage> MessagesToIgnored { get; set; }
    }
}