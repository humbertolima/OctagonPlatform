using GridMvc.DataAnnotations;
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
        public string TerminalId { get; set; }
        public DateTime? LastLoad { get; set; }
        public int? AmountPrevius { get; set; }
        public int? AmountLoad { get; set; }
        public int? AmountCurrent { get; set; }
        public int? CashBalance { get; set; }
        public int? Dayuntilcashload { get; set; }
    }
    public class JsonCashManagementReport
    {

        public string Locationname { get; set; }
        public DateTime Date { get; set; }
        public int AmountPrevius { get; set; }
        public int AmountLoad { get; set; }
        public int AmountCurrent { get; set; }
        public string TerminalId { get; set; }
      
      
    }
   
    
    public class JsonLoadCashChart
    {
        public string Date { get; set; }
        public int? AmountPrevius { get; set; }
        public int? AmountLoad { get; set; }
        
        public JsonLoadCashChart(string date, int? amountPrevius, int? amountLoad)
        {           
            AmountPrevius = amountPrevius;
            AmountLoad = amountLoad;          
            Date = date;
        }
    }

    public class JsonCashBalanceClose
    {
        public JsonCashBalanceClose(string terminalId, string time, int? cashBalance)
        {
            TerminalId = terminalId;
            Time = time;
            CashBalance = cashBalance;
        }

        public string TerminalId { get; set; }
        public string Time { get; set; }
        public int? CashBalance { get; set; }

    }

    public class JsonTerminalStatusReport
    {
        public JsonTerminalStatusReport()
        {
        }

        public JsonTerminalStatusReport(string terminalId, int? cashBalance, int? dayuntilcashload, DateTime? lastComunication, DateTime? lastTransaction11, DateTime? lastTransaction12, DateTime? lastTransaction15, int? hourInactive, int? aux)
        {
            TerminalId = terminalId;
            CashBalance = cashBalance;
            Dayuntilcashload = dayuntilcashload;
            LastComunication = lastComunication;
            LastTransaction11 = lastTransaction11;
            LastTransaction12 = lastTransaction12;
            LastTransaction15 = lastTransaction15;
            HourInactive = hourInactive;
            Aux = aux;
        }

        public string TerminalId { get; set; }
        public int? CashBalance { get; set; }
        public int? Dayuntilcashload { get; set; }
        public DateTime? LastComunication { get; set; }
        public DateTime? LastTransaction11 { get; set; }
        public DateTime? LastTransaction12 { get; set; }
        public DateTime? LastTransaction15 { get; set; }
        public int? HourInactive { get; set; }
        public int? Aux { get; set; } //promedio de amount mensual de transacciones por 30 dias
    }
    public class JsonTerminalStatusChart
    {
        public JsonTerminalStatusChart()
        {
        }

        public JsonTerminalStatusChart(string terminalId, int? cashBalance, string lastComunication)
        {
            TerminalId = terminalId;
            CashBalance = cashBalance;
            LastComunication = lastComunication;
        }

        public string TerminalId { get; set; }
        public int? CashBalance { get; set; }
        public string LastComunication { get; set; }


    }
    public class JsonDailyTransactionSummary
    {
        public string TerminalId { get; set; }
        public DateTime Date { get; set; }
        public int? ApprovedWithdrawals { get; set; }
        public int? Declined { get; set; }
        public int? SurchargableWithdrawals { get; set; }
        public int? OtherApproved { get; set; }
        public int? Reversed { get; set; }
        public double? SurchargeAmount { get; set; }
        public int? TotalTransaction { get; set; }
        public double? Surcharge { get; set; }
        public double? Dispensed { get; set; }

    }
    public class JsonMonthlyTransactionSummary
    {
        public string TerminalId { get; set; }
        public string Date { get; set; }
        public int? ApprovedWithdrawals { get; set; }
        public int? Declined { get; set; }
        public int? SurchargableWithdrawals { get; set; }
        public int? OtherApproved { get; set; }
        public int? Reversed { get; set; }
        public double? SurchargeAmount { get; set; }
        public int? TotalTransaction { get; set; }
        public double? Surcharge { get; set; }

    }
}