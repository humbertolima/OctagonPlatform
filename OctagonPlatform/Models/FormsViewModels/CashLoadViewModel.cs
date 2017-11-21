using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class CashLoadViewModel
    {
        public string TerminalId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public CashLoadViewModel()
        {
            TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
        }
    }
}