using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models
{
    [Table("Schedules")]
    public abstract class Schedule
    {
        protected Schedule()
        {
        }

        protected Schedule(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate)
        {           
            Name = name;
            Repeats = repeats;
            StartDate = startDate;
        }

        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        public ScheduleType.RepeatsEnum Repeats { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

    }
   
    public class ScheduleOnce : Schedule
    {
        public ScheduleOnce()
        {
        }

        public ScheduleOnce(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate,string time) : base(name, repeats, startDate)
        {
            Time = time;
        }

        [Required]
        public string Time { get; set; }

    }
    public class ScheduleDaily : Schedule
    {
        public ScheduleDaily()
        {
        }

        public ScheduleDaily(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate, string time, int repeatOn, string stopDate) : base(name, repeats, startDate)
        {
            Time = time;
            RepeatOn = repeatOn;
            StopDate = stopDate;
        }
        public string Time { get; set; }
       
        public int RepeatOn { get; set; }
      
        public string StopDate { get; set; }
    }
    public class ScheduleWeekly : Schedule
    {
        public ScheduleWeekly()
        {
        }

        public ScheduleWeekly(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate, string time, int repeatOnWeeks,string repeatOnDaysWeeks, string stopDate) : base(name, repeats, startDate)
        {
            Time = time;
            RepeatOnWeeks = repeatOnWeeks;
            RepeatOnDaysWeeks = repeatOnDaysWeeks;
            StopDate = stopDate;
        }
        public string Time { get; set; }
       
        public int RepeatOnWeeks { get; set; }
        public string RepeatOnDaysWeeks { get; set; }

        public string StopDate { get; set; }
    }
    public class ScheduleMonthly : Schedule
    {
        public ScheduleMonthly()
        {
        }

        public ScheduleMonthly(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate, string time, int repeatOnDay, int repeatOnMonth, string stopDate) : base(name, repeats, startDate)
        {
            Time = time;
            RepeatOnDay = repeatOnDay;
            RepeatOnMonth = repeatOnMonth;
            StopDate = stopDate;
        }
        public string Time { get; set; }

        public int RepeatOnDay { get; set; }
        public int RepeatOnMonth { get; set; }

        public string StopDate { get; set; }
    }
    public class ScheduleMonthlyRelative : Schedule
    {
        public ScheduleMonthlyRelative()
        {
        }

        public ScheduleMonthlyRelative(string name, ScheduleType.RepeatsEnum repeats, DateTime startDate, string time, string repeatOnFirst, string repeatOnDay,int repeatOnMonth, string stopDate) : base(name, repeats, startDate)
        {
            Time = time;
            RepeatOnFirst = repeatOnFirst;
            RepeatOnDay = repeatOnDay;
            RepeatOnMonth = repeatOnMonth;
            StopDate = stopDate;
        }
        public string Time { get; set; }

        public string RepeatOnFirst { get; set; }
        public string RepeatOnDay { get; set; }
        public int RepeatOnMonth { get; set; }

        public string StopDate { get; set; }
    }
}