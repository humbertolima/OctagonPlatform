using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalCassetteVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public bool IsAddCassette { get; set; }
        public bool IsSetCassette { get; set; }
        public bool IsDeleteCassette { get; set; }

        public List<Cassette> Cassettes { get; set; }

    }
}