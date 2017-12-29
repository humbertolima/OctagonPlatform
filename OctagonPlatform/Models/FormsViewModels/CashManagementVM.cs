using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class CashManagementVM 
    {
        [Required]
        public int Id { get; set; }

        
        [DisplayName("Terminal Id")]
        public string TerminalId { get; set; }

        public string Partner { get; set; }

        [HiddenInput]
        public int PartnerId { get; set; }

        public string Group { get; set; }

        [HiddenInput]
        public int GroupId { get; set; }

        public StatusType.Status Status { get; set; }


        public CashManagementVM()
        {           
            PartnerId = -1;
            GroupId = -1;
        }
    }
}