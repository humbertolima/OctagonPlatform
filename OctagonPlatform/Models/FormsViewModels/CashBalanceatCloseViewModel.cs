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
    public class CashBalanceatCloseViewModel
    {

        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
       
        public string StartDate { get; set; }
        public CashBalanceatCloseViewModel()
        {
            //TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Now.ToShortDateString();
            PartnerId = -1;
            GroupId = -1;
        }
    }
}