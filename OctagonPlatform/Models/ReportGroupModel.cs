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
        public int PartnerId { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<Terminal> Terminals { get; set; }
        public ReportGroupModel()
        {
            Terminals = new List<Terminal>();
        }
    }
    //[Table("ReportGroupsTerminal")]
    //public class ReportGroupsTerminal
    //{
    //    public ReportGroupsTerminal(int reportGroupID, int terminalID)
    //    {
    //        ReportGroupID = reportGroupID;
    //        TerminalID = terminalID;
    //    }

    //    public virtual ReportGroupModel ReportGroup { get; set; }
    //    [Key, Column(Order = 0)]
    //    public int ReportGroupID { get; set; }

    //    public virtual Terminal Terminal { get; set; }
    //    [Key, Column(Order = 1)]
    //    public int TerminalID { get; set; }
    //}
}