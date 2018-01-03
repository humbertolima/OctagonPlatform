using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class VaultCashFormViewModel
    {
        [Key]
        [Required]
        [Display(Name = "Terminal")]
        public int Id { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Starts on")]
        [DisplayFormat(DataFormatString =
                "{0:d MMM yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Stops on")]
        [DisplayFormat(DataFormatString =
                "{0:d MMM yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime StopDate { get; set; }
    }
}