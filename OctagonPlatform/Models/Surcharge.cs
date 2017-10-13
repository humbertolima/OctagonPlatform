using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Surcharge: ISoftDeleted, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Terminal Associated")]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account Associated")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StopDate { get; set; }

        [Required]
        public double SplitAmount { get; set; }

        [Required]
        public double SplitAmountPercent { get; set; }

        [Required]
        public Settled.SettledType SettledType { get; set; }

        public bool Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public string DeletedByName { get; set; }

        public void Calc_SplitAmountPercent()
        {
            var surcharge = Terminal.SurchargeAmountFee;
        }

        public void Calc_SplitAmount()
        {

        }
    }
}