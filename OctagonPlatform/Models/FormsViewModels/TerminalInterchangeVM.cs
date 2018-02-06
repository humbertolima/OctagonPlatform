using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalInterchangeVM
    {
        [Required]
        public int Id { get; set; }

        public bool IsAddInterchange { get; set; }
        public bool IsEditInterchange { get; set; }
        public bool IsDeleteInterchange { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public List<InterChange> Interchanges{ get; set; }
    }
}