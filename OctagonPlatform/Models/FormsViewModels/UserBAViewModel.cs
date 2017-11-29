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
        public int UserId { get; set; }
        
        public bool IsLocked { get; set; }

        public List<BankAccount> BankAccounts { get; set; }

        public string Error { get; set; }
    }
}