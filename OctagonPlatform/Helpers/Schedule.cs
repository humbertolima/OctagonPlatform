using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    public class ScheduleType
    {
        public enum RepeatsEnum
        {
            [Display(Name = "-- Select --")]
            Select = 0,
            Once = 1,
            Daily = 2,
            Weekly = 3,
            Monthly = 4,
            MonthlyRelative = 5

        }
        

    

    }
}