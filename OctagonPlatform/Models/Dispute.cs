using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Dispute : ISoftDeleted, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        public bool Viewed { get; set; }

        public string MessageNumber { get; set; }

        public string NetworkAdjustmentId { get; set; }

        [Required]
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        [Required]
        public int TransactionId { get; set; }
        public TransactionStatistic Transaction { get; set; }

        public string Network { get; set; }

        public DisputeType.Disputes DisputeType { get; set; }

        public string SecuenceNumber { get; set; }

        public decimal AmountRequested { get; set; }

        public decimal DisputedAmount { get; set; }

        public DateTime LastDayToRepresent { get; set; }

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