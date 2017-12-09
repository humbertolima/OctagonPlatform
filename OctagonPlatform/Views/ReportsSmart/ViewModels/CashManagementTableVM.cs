using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
    
    public class CashManagementTableVM
    {
        public CashManagementTableVM(string terminalID, string locationName, string currentCashBalance, string daysUntilCashLoad, string lastLoadDate, string previousBalance, string cashLoadAmount, string newBalance)
        {
            TerminalID = terminalID;
            LocationName = locationName;
            CurrentCashBalance = currentCashBalance;
            DaysUntilCashLoad = daysUntilCashLoad;
            LastLoadDate = lastLoadDate;
            PreviousBalance = previousBalance;
            CashLoadAmount = cashLoadAmount;
            NewBalance = newBalance;
        }

        public CashManagementTableVM()
        {
        }

        [GridColumn(Title = "Terminal Id")]
        public string TerminalID { get; set; }
        [GridColumn(Title = "Location Name")]
        public string LocationName { get; set; }
        [GridColumn(Title = "Current Cash Balance")]
        public string CurrentCashBalance { get; set; }
        [GridColumn(Title = "Days Until Cash Load")]
        public string DaysUntilCashLoad { get; set; }
        [GridColumn(Title = "Last Load Date")]
        public string LastLoadDate { get; set; }
        [GridColumn(Title = "Previous Balance")]
        public string PreviousBalance { get; set; }
        [GridColumn(Title = "Cash Load Amount")]
        public string CashLoadAmount { get; set; }
        [GridColumn(Title = "New Balance")]
        public string NewBalance { get; set; }
    }
   
}