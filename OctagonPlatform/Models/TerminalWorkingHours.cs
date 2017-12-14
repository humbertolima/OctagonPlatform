using OctagonPlatform.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class TerminalWorkingHours
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        [Required]
        public Week.Day Day { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}