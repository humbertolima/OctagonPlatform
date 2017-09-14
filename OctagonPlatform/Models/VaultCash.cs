using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class VaultCash: ISoftDeleted, IAuditEntity
    {
        
        [Key, ForeignKey("Terminal")]
        public int Id { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        [Display(Name = "Bank Account Associated")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        [Required]
        public int TotalAmmount { get; set; }

        [Required]
        public Settled.SettledType SettledType { get; set; }

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