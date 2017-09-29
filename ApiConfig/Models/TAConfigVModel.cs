using OctagonPlatform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiConfig.Models
{
    public class TAConfigVModel
    {
        public int Id { get; set; }
        
        [Required]
        public double LowCach1 { get; set; }

        public double? LowCash2 { get; set; }

        public double? LowCash3 { get; set; }

        public DateTime? InactivePeriod { get; set; }

        public ICollection<TerminalMViewModel> MessagesToIgnored { get; set; }

    }
}