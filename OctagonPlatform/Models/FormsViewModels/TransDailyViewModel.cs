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
    public class TransDailyViewModel
    {
        [DisplayName("Terminal Id")]
        public string TerminalId { get; set; }

        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
        [DisplayName("From")]
        public string StartDate { get; set; }
        [DisplayName("To")]
        public string EndDate { get; set; }
       
        public TransDailyViewModel()
        {
            //TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
            PartnerId = -1;
            GroupId = -1;
        }
    }
}