using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class CassetteViewModel
    {
        [Required]
        public bool AutoRecord { get; set; }

        [Required]
        public int Denomination { get; set; }
         
        [Required]
        public int TerminalId { get; set; }
    }
}