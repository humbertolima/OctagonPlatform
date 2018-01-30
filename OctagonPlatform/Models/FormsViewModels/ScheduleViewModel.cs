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
        public ScheduleViewModel()
        {
            
            List<SelectListItem> list = new List<SelectListItem>();
            #region AddSelectItem Time
            list.Add(new SelectListItem { Value = "12:00:00 AM", Text = "12:00:00 AM" });
            list.Add(new SelectListItem { Value = "12:30:00 AM", Text = "12:30:00 AM" });
            list.Add(new SelectListItem { Value = "1:00:00 AM", Text = "12:00:00 AM" });
            list.Add(new SelectListItem { Value = "1:30:00 AM", Text = "1:30:00 AM" });
            list.Add(new SelectListItem { Value = "2:00:00 AM", Text = "2:00:00 AM" });
            list.Add(new SelectListItem { Value = "2:30:00 AM", Text = "2:30:00 AM" });
            list.Add(new SelectListItem { Value = "3:00:00 AM", Text = "3:00:00 AM" });
            list.Add(new SelectListItem { Value = "3:30:00 AM", Text = "3:30:00 AM" });
            list.Add(new SelectListItem { Value = "4:00:00 AM", Text = "4:00:00 AM" });
            list.Add(new SelectListItem { Value = "4:30:00 AM", Text = "4:30:00 AM" });
            list.Add(new SelectListItem { Value = "5:00:00 AM", Text = "5:00:00 AM" });
            list.Add(new SelectListItem { Value = "5:30:00 AM", Text = "5:30:00 AM" });
            list.Add(new SelectListItem { Value = "6:00:00 AM", Text = "6:00:00 AM" });
            list.Add(new SelectListItem { Value = "6:30:00 AM", Text = "6:30:00 AM" });
            list.Add(new SelectListItem { Value = "7:00:00 AM", Text = "7:00:00 AM" });
            list.Add(new SelectListItem { Value = "7:30:00 AM", Text = "7:30:00 AM" });
            list.Add(new SelectListItem { Value = "8:00:00 AM", Text = "8:00:00 AM" });
            list.Add(new SelectListItem { Value = "8:30:00 AM", Text = "8:30:00 AM" });
            list.Add(new SelectListItem { Value = "9:00:00 AM", Text = "9:00:00 AM" });
            list.Add(new SelectListItem { Value = "9:30:00 AM", Text = "9:30:00 AM" });
            list.Add(new SelectListItem { Value = "10:00:00 AM", Text = "10:00:00 AM" });
            list.Add(new SelectListItem { Value = "10:30:00 AM", Text = "10:30:00 AM" });
            list.Add(new SelectListItem { Value = "11:00:00 AM", Text = "11:00:00 AM" });
            list.Add(new SelectListItem { Value = "11:30:00 AM", Text = "11:30:00 AM" });
            list.Add(new SelectListItem { Value = "12:00:00 PM", Text = "12:00:00 PM" });
            list.Add(new SelectListItem { Value = "12:30:00 PM", Text = "12:30:00 PM" });
            list.Add(new SelectListItem { Value = "1:00:00 PM", Text = "12:00:00 PM" });
            list.Add(new SelectListItem { Value = "1:30:00 PM", Text = "1:30:00 PM" });
            list.Add(new SelectListItem { Value = "2:00:00 PM", Text = "2:00:00 PM" });
            list.Add(new SelectListItem { Value = "2:30:00 PM", Text = "2:30:00 PM" });
            list.Add(new SelectListItem { Value = "3:00:00 PM", Text = "3:00:00 PM" });
            list.Add(new SelectListItem { Value = "3:30:00 PM", Text = "3:30:00 PM" });
            list.Add(new SelectListItem { Value = "4:00:00 PM", Text = "4:00:00 PM" });
            list.Add(new SelectListItem { Value = "4:30:00 PM", Text = "4:30:00 PM" });
            list.Add(new SelectListItem { Value = "5:00:00 PM", Text = "5:00:00 PM" });
            list.Add(new SelectListItem { Value = "5:30:00 PM", Text = "5:30:00 PM" });
            list.Add(new SelectListItem { Value = "6:00:00 PM", Text = "6:00:00 PM" });
            list.Add(new SelectListItem { Value = "6:30:00 PM", Text = "6:30:00 PM" });
            list.Add(new SelectListItem { Value = "7:00:00 PM", Text = "7:00:00 PM" });
            list.Add(new SelectListItem { Value = "7:30:00 PM", Text = "7:30:00 PM" });
            list.Add(new SelectListItem { Value = "8:00:00 PM", Text = "8:00:00 PM" });
            list.Add(new SelectListItem { Value = "8:30:00 PM", Text = "8:30:00 PM" });
            list.Add(new SelectListItem { Value = "9:00:00 PM", Text = "9:00:00 PM" });
            list.Add(new SelectListItem { Value = "9:30:00 PM", Text = "9:30:00 PM" });
            list.Add(new SelectListItem { Value = "10:00:00 PM", Text = "10:00:00 PM" });
            list.Add(new SelectListItem { Value = "10:30:00 PM", Text = "10:30:00 PM" });
            list.Add(new SelectListItem { Value = "11:00:00 PM", Text = "11:00:00 PM" });
            list.Add(new SelectListItem { Value = "11:30:00 PM", Text = "11:30:00 PM" });
            #endregion
            DropDownRepeatOn = new List<SelectListItem>();
            DropDownRepeatOn.Add(new SelectListItem { Value = "1", Text = "1" });
            DropDownRepeatOn.Add(new SelectListItem { Value = "2", Text = "2" });
            DropDownRepeatOn.Add(new SelectListItem { Value = "3", Text = "3" });
            DropDownRepeatOn.Add(new SelectListItem { Value = "4", Text = "4" });
            DropDownRepeatOn.Add(new SelectListItem { Value = "5", Text = "5" });
            DropDownRepeatOn.Add(new SelectListItem { Value = "6", Text = "6" });

            DropDownRepeatOnDay = new List<SelectListItem>();
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "1", Text = "1" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "2", Text = "2" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "3", Text = "3" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "4", Text = "4" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "5", Text = "5" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "6", Text = "6" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "7", Text = "7" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "8", Text = "8" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "9", Text = "9" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "10", Text = "10" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "11", Text = "11" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "12", Text = "12" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "13", Text = "13" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "14", Text = "14" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "15", Text = "15" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "16", Text = "16" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "17", Text = "17" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "18", Text = "18" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "19", Text = "19" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "20", Text = "20" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "21", Text = "21" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "22", Text = "22" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "23", Text = "23" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "24", Text = "24" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "25", Text = "25" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "26", Text = "26" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "27", Text = "27" });
            DropDownRepeatOnDay.Add(new SelectListItem { Value = "28", Text = "28" });
            DropDownTime = list;
            DropDownRepeatOnMonth = new List<SelectListItem>();
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "1", Text = "1" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "2", Text = "2" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "3", Text = "3" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "4", Text = "4" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "5", Text = "5" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "6", Text = "6" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "7", Text = "7" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "8", Text = "8" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "9", Text = "9" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "10", Text = "10" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "11", Text = "11" });
            DropDownRepeatOnMonth.Add(new SelectListItem { Value = "12", Text = "12" });
            DropDownRepeatOnWeeks = new List<SelectListItem>();
            DropDownRepeatOnWeeks.Add(new SelectListItem { Value = "1", Text = "1" });
            DropDownRepeatOnWeeks.Add(new SelectListItem { Value = "2", Text = "2" });
            DropDownRepeatOnWeeks.Add(new SelectListItem { Value = "3", Text = "3" });
            DropDownRepeatOnWeeks.Add(new SelectListItem { Value = "4", Text = "4" });
            DropDownRepeatOnWeeks.Add(new SelectListItem { Value = "5", Text = "5" });
            DropDownRepeatOnFirst = new List<SelectListItem>();
            DropDownRepeatOnFirst.Add(new SelectListItem { Value = "1", Text = "First" });
            DropDownRepeatOnFirst.Add(new SelectListItem { Value = "2", Text = "Second" });
            DropDownRepeatOnFirst.Add(new SelectListItem { Value = "3", Text = "Third" });
            DropDownRepeatOnFirst.Add(new SelectListItem { Value = "4", Text = "Fourth" });
            DropDownRepeatOnFirst.Add(new SelectListItem { Value = "0", Text = "Last" });
            DropDownRepeatOnDay2 = new List<SelectListItem>();
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Sun", Text = "Sunday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Mon", Text = "Monday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Tue", Text = "Tuesday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Wed", Text = "Wednesday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Thu", Text = "Thursday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Fri", Text = "Friday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Sat", Text = "Saturday" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "Day", Text = "Day" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "weekend_day", Text = "Weekend Day" });
            DropDownRepeatOnDay2.Add(new SelectListItem { Value = "week_day", Text = "Week Day" });
            DropDownRepeatOnMonth2 = new List<SelectListItem>();
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "1", Text = "1" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "2", Text = "2" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "3", Text = "3" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "4", Text = "4" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "5", Text = "5" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "6", Text = "6" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "7", Text = "7" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "8", Text = "8" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "9", Text = "9" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "10", Text = "10" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "11", Text = "11" });
            DropDownRepeatOnMonth2.Add(new SelectListItem { Value = "12", Text = "12" });

        }
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


        public List<SelectListItem> DropDownTime { get; set; }
        public string Time { get; set; }
        //Scheduledaily
        public List<SelectListItem> DropDownRepeatOn { get; set; }
        public int RepeatOn { get; set; }
        public string StopDate { get; set; }
        //ScheduleWeekly
        public List<SelectListItem> DropDownRepeatOnWeeks { get; set; }
        public int RepeatOnWeeks { get; set; }
        public string RepeatOnDaysWeeks { get; set; }

        //ScheduleMonthly
        public List<SelectListItem> DropDownRepeatOnDay { get; set; }
        public int RepeatOnDay { get; set; }
        public List<SelectListItem> DropDownRepeatOnMonth { get; set; }
        public int RepeatOnMonth { get; set; }

        //ScheduleMonthlyRelative
        public List<SelectListItem> DropDownRepeatOnFirst { get; set; }
        public string RepeatOnFirst { get; set; }
        public List<SelectListItem> DropDownRepeatOnDay2 { get; set; }
        public string RepeatOnDay2 { get; set; }
        public List<SelectListItem> DropDownRepeatOnMonth2 { get; set; }
        public int RepeatOnMonth2 { get; set; }
        public string UserId { get; set; }
       
    }
}