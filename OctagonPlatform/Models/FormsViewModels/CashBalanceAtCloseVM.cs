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
    public class CashBalanceAtCloseVM
    {

        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
       
        public DateTime? StartDate { get; set; }
        public CashBalanceAtCloseVM()
        {
            //TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Today;
            PartnerId = -1;
            GroupId = -1;
        }
    }
}