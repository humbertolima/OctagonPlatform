using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    [Table("Subscriptions")]
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string EmailComment { get; set; }
        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public int PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public virtual ICollection<ReportFilter> ReportFilters { get; set; }
    }
}