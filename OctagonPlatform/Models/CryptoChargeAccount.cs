using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class CryptoChargeAccount    
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Terminal Associated")]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account Azossiated")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StopDate { get; set; }

        [Required]
        public double SplitAmmount { get; set; }

        [Required]
        public Settled.SettledType SettledType { get; set; }
    }
}