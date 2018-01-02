using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class ScheduleViewModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        [StringValidation(ErrorMessage = "Select one option")]
        public ScheduleType.RepeatsEnum Repeats { get; set; }
        [HiddenInput]
        public int RepeatsSelected { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        //ScheduleOnce
        
        public string Time { get; set; }
        //Scheduledaily
      
        public int RepeatOn { get; set; }
        public string StopDate { get; set; }
        //ScheduleWeekly
      
        public int RepeatOnWeeks { get; set; }
        public string RepeatOnDaysWeeks { get; set; }
       
        //ScheduleMonthly
      
        public int RepeatOnDay { get; set; }
        public int RepeatOnMonth { get; set; }
        
        //ScheduleMonthlyRelative
        
        public string RepeatOnFirst { get; set; }
        public string RepeatOnDay2 { get; set; }
        public int RepeatOnMonth2 { get; set; }

    }
}