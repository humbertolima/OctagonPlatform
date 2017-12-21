using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
   
    public class TerminalStatusTableVM
    {
        public TerminalStatusTableVM()
        {
        }

        public TerminalStatusTableVM(string terminalId, string locationname, int? cashBalance, int? dayUntilCashLoad, string contactName, string contactPhone, string lastKnowError, DateTime? lastCommunication, DateTime? lastTransaction, int? hourInactive)
        {
            TerminalId = terminalId;
            Locationname = locationname;
            CashBalance = cashBalance;
            DayUntilCashLoad = dayUntilCashLoad;
            ContactName = contactName;
            ContactPhone = contactPhone;
            LastKnowError = lastKnowError;
            LastCommunication = lastCommunication;
            LastTransaction = lastTransaction;
            HourInactive = hourInactive;
        }
        [GridColumn(Title = "Terminal Id")]
        public string TerminalId { get; set; }
        [GridColumn(Title = "Location Name")]
        public string Locationname { get; set; }
        [GridColumn(Title = "Cash Balance")]
        public int? CashBalance { get; set; }
        [GridColumn(Title = "Day Until Cash Load")]
        public int? DayUntilCashLoad { get; set; }
        [GridColumn(Title = "Contact Name")]
        public string ContactName { get; set; }
        [GridColumn(Title = "Contact Phone")]
        public string ContactPhone { get; set; }
        [GridColumn(Title = "Last Know Error")]
        public string LastKnowError { get; set; }
        [GridColumn(Title = "Last Communication")]
        public DateTime? LastCommunication { get; set; }
        [GridColumn(Title = "Last Transaction")]
        public DateTime? LastTransaction { get; set; }
        [GridColumn(Title = "Hour Inactive")]
        public int? HourInactive { get; set; }



    }
}