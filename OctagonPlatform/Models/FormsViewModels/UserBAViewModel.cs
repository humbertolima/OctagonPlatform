using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class UserBAViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Partner is required")]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }
        public IEnumerable<Partner> Partners { get; set; }

        public bool IsLocked { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public string Error { get; set; }
    }
}