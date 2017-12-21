using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    public class CommunicationType
    {
        public enum Communication
        {
            [Display(Name = "< All Conections>")]
            All = 0,
            [Display(Name = "Phone Line")]
            PhoneLine = 1,
            [Display(Name = "TCP/IP")]
            TcpIp = 2,
        }
      
    }
}