using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Controllers.Reports.JSON
{

    public class JsonLoadCash
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
    }
    public class JsonCashManagement
    {     
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
    }
    public class JsonCashManagementReport
    {

        public string Locationname { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
      
        public JsonCashManagementReport(string locationname, DateTime date, int amountPrevius, int amountLoad, int amountCurrent, string terminalId)
        {

            Locationname = locationname;
            Date = date;
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;
            AmountCurrent = amountCurrent;
            TerminalId = terminalId;
            
        }
    }
    public class JsonLoadCashReport
    {
       
        public string Locationname { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
        public JsonLoadCashReport( string locationname, DateTime date, int amountPrevius, int amountLoad, int amountCurrent, string terminalId)
        {
           
            Locationname = locationname;
            Date = date;
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;
            AmountCurrent = amountCurrent;
            TerminalId = terminalId;
        }
    }
    public class CashManagementReport
    {
        public CashManagementReport(string terminalId, string locationname, int currentCashBalance, int daysUntilCashLoad, DateTime lastLoadDate, int previousBalance, int cashLoadAmount, int newBalance)
        {
            TerminalId = terminalId;
            Locationname = locationname;
            CurrentCashBalance = currentCashBalance;
            DaysUntilCashLoad = daysUntilCashLoad;
            LastLoadDate = lastLoadDate;
            PreviousBalance = previousBalance;
            CashLoadAmount = cashLoadAmount;
            NewBalance = newBalance;
        }

        public string TerminalId { get; set; }
        public string Locationname { get; set; }
        public int CurrentCashBalance { get; set; }
        public int DaysUntilCashLoad { get; set; } 
        public DateTime LastLoadDate { get; set; }
        public int PreviousBalance { get; set; }
        public int CashLoadAmount { get; set; }
        public int NewBalance { get; set; }
       
     
    }
    public class JsonLoadCashChart
    {
        public string Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        
        public JsonLoadCashChart(string date, int amountPrevius, int amountLoad)
        {           
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;          
            Date = date;
        }
    }
}