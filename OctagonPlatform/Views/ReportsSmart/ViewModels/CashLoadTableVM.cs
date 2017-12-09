using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
   
    public class CashLoadTableVM
    {
        public CashLoadTableVM(string terminalID, string locationName, string previousBalance, string cashLoadAmount, string newBalance, DateTime date)
        {
            TerminalID = terminalID;
            LocationName = locationName;
            PreviousBalance = previousBalance;
            CashLoadAmount = cashLoadAmount;
            NewBalance = newBalance;
            Date = date;
        }

        public CashLoadTableVM()
        {
        }

        [GridColumn(Title = "Terminal Id")]
        public string TerminalID { get; set; }
        [GridColumn(Title = "Location Name")]
        public string LocationName { get; set; }
        [GridColumn(Title = "Previous Balance")]
        public string PreviousBalance { get; set; }
        [GridColumn(Title = "Cash Load Amount")]
        public string CashLoadAmount { get; set; }
        [GridColumn(Title = "New Balance")]
        public string NewBalance { get; set; }
        [GridColumn(Title = "Date")]
        public DateTime Date { get; set; }
    }
   
}