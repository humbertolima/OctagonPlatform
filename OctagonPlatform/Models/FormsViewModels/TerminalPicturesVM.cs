using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalPicturesVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }
        
        public List<Picture> Pictures { get; set; }
    }
}