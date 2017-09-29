using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiConfig.Models
{
    public class TerminalMViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Id_8583 { get; set; }     //enlace con Api de Emi
        
        public int TerminalAlertConfigId { get; set; }
    }
}