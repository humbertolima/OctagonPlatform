using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class InterChange: ISoftDeleted, IAuditEntity
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

        [Required]
        [Display(Name = "Inter Change Ammount")]
        public double Ammount { get; set; }

        public string CalculationMethod { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StopDate { get; set; }

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
    }
}