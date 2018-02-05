using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalContactVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public bool IsAddContact { get; set; }
        public bool IsEditContact { get; set; }
        public bool IsDeleteContact { get; set; }


        public List<TerminalContact> Contacts { get; set; }
    }
}