using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    [Table("Reports")]
    public class ReportModel
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsShowDashboard { get; set; }
        public virtual ICollection<ReportFilter> ReportFilters { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
    [Table("ReportFilters")]
    public class ReportFilter
    {
        [Key, Column(Order = 0)]
        public int ReportID { get; set; }
        [Key, Column(Order = 1)]
        public int FilterID { get; set; }

        public virtual ReportModel Report { get; set; }
        public virtual FilterModel Filter { get; set; }

        public int SubscriptionID { get; set; }
        public virtual SubscriptionModel Subscription { get; set; }      

        public string Value { get; set; }
    }
}