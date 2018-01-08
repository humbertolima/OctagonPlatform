using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class SubscriptionVM
    {
        public IEnumerable<SubscriptionModel> List { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string EmailComment { get; set; }

        public string User { get; set; }
        [HiddenInput]
        public int UserId { get; set; }

        public SelectList ReportId { get; set; }
        public SelectList ScheduledId { get; set; }
    
    }
}