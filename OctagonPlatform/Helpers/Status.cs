using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    public class StatusType
    {
        public enum Status
        {
            [Display(Name = "< All >")]
            All = 0,
            Active = 1,
            Inactive = 2,
            Incomplete = 3
        }
   
    }
}