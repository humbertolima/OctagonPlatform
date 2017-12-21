using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models
{
    [Table("ReportGroups")]
    public class ReportGroupModel
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]      
        public string Name { get; set; }       
        public ICollection<Terminal> Terminals { get; set; }
       
    }
}