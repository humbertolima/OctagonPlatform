using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class SurchargeFormViewModel
    { 
        public int Id { get; set; }

        [Required]
        [Display(Name = "Terminal Associated")]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account Associated")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Starts on")]
        [DisplayFormat(DataFormatString =
                "{0:d MMM yyyy}",
            ApplyFormatInEditMode = true)]
        [DateTimeValidationLesserThan]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Stops on")]
        [DisplayFormat(DataFormatString =
                "{0:d MMM yyyy}",
            ApplyFormatInEditMode = true)]
        [DateTimeValidationLesserThan]
        public DateTime StopDate { get; set; }

        [Required]
        public double SplitAmount { get; set; }

        [Required]
        public double SplitAmountPercent { get; set; }

        [Required]
        [Display(Name = "Settled...")]
        public SurchargeSettled.SettledType SettledType { get; set; }
    }
}