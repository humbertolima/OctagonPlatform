using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class InterChangeFormViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Terminal Azossiated")]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account Azossiated")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        [Required]
        [Display(Name = "Split Ammount")]
        [DataType(DataType.Currency)]
        public double Ammount { get; set; }

        [Required]
        public CalculationMethod.Method CalculationMethod { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StopDate { get; set; }
    }
}