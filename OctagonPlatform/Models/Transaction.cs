using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Transaction: ISoftDeleted, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        public string TransactionType { get; set; }

        public bool Normal { get; set; }

        public bool Reversal { get; set; }

        public string Dcc { get; set; }

        public string Pan { get; set; }

        public string CardBrand { get; set; }

        public string Input { get; set; }

        [DataType(DataType.CreditCard)]
        public string CardSequence { get; set; }

        public string Response { get; set; }

        [DataType(DataType.Currency)]
        public int AmmountRequested { get; set; }

        [DataType(DataType.Currency)]
        public int AmmountAproved { get; set; }

        public double AmmountSurcharge { get; set; }

        public double AmmountReversed { get; set; }

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

        public ICollection<Dispute> Disputes { get; set; }

        public Transaction()
        {
            Disputes = new Collection<Dispute>();
        }
    }
}