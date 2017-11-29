using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class LoadCash:ISoftDeleted, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Previous Balance")]
        public int PreviousBalance { get; set; }

        [Required]
        [Display(Name = "Ammount Loaded")]
        public int AmmountLoaded { get; set; }

        [Required]
        [Display(Name = "Current Balance")]
        public int CurrentBalance { get; set; }

        [Required]
        [Display(Name = "Terminal")]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

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