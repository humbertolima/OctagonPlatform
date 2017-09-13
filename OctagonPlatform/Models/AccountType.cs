using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Account Type")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<BankAccount> BankAccount { get; set; }
    }
}