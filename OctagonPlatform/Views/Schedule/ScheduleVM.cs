using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Views.Schedule
{
    public class ScheduleVM
    {
        public IEnumerable<OctagonPlatform.Models.Schedule> List { get; set; }
        //Autocomplete
        public string User { get; set; }
        [HiddenInput]
        public int UserId { get; set; }
    }
}