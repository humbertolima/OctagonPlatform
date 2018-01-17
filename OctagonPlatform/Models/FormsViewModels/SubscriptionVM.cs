using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class SubscriptionVM
    {
        public List<SubsTableViewModel> List { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string EmailComment { get; set; }

        public string User { get; set; }
        [HiddenInput]
        public int UserId { get; set; }

        public SelectList ReportId { get; set; }
        public SelectList ScheduledId { get; set; }
    
    }
    public class SubsTableViewModel
    {
        public string ReportName { get; set; } 
        public string Description { get; set; }
        public string ScheduleName { get; set; }
        public string Username { get; set; }
        public string NextRunDate { get; set; }
        public string LastRunDate { get; set; }
        public int Id { get; set; }

    }
}