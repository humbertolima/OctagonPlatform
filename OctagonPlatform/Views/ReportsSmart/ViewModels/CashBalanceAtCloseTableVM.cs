using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
    
    public class CashBalanceAtCloseTableVM
    {
        public CashBalanceAtCloseTableVM()
        {
        }

        public CashBalanceAtCloseTableVM(string terminalID, string locationName, string time, string cashBalance)
        {
            TerminalID = terminalID;
            LocationName = locationName;
            Time = time+" 02:00 PM";
            CashBalance = cashBalance;
        }

        [GridColumn(Title = "Terminal Id")]
        public string TerminalID { get; set; }
        [GridColumn(Title = "Location Name")]
        public string LocationName { get; set; }
        [GridColumn(Title = "Time(CST)")]
        public string Time { get; set; }
        [GridColumn(Title = "Balance")]
        public string CashBalance { get; set; }
    }
   
}