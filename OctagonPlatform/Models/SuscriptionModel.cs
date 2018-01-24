using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    [Table("Subscriptions")]
    public class SubscriptionModel : AuditEntity
    {
        public SubscriptionModel()
        {
        }

        public SubscriptionModel(string email, string description, string emailComment, int scheduleId, int userId, string format)
        {
            Email = email;
            Description = description;
            EmailComment = emailComment;
            ScheduleId = scheduleId;
            UserId = userId;
            Format = format;

        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string EmailComment { get; set; }
        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [StringLength(1)]
        public string Format { get; set; }
        public virtual ICollection<ReportFilter> ReportFilters { get; set; }
       
    }
}