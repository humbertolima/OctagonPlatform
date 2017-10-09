using OctagonPlatform.Helpers;
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
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        [Required]
        [Display(Name = "Settled...")]
        public Settled.SettledType SettledType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Starts on")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Stops on")]
        public DateTime StopDate { get; set; }
    }
}