using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class ReportsViewModel
    {
        public IList<string> SelectedReports { get; set; }
        public IList<SelectListItem> AvailableReports { get; set; }

        public ReportsViewModel()
        {
            SelectedReports = new List<string>();
            AvailableReports = new List<SelectListItem>();
        }
    }
}