using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    [NotMapped]
    public class TerminalMessage
    {
        
        [Required]
        public int Id { get; set; }

        [Required]
        public Terminal Terminal { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Direction { get; set; }

    }
}