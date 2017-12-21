using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
   
    public class TransDailyTableVM
    {
        public TransDailyTableVM(string terminalId, string locationName, DateTime date, int? approvedWithdrawals, int? declined, int? surchargableWithdrawals, int? otherApproved, int? reversed, double? surchargeAmount, int? totalTransaction, double? surcharge, double? dispensed)
        {
            TerminalId = terminalId;
            LocationName = locationName;
            Date = date.ToString("MM/dd/yyyy");
            ApprovedWithdrawals = approvedWithdrawals;
            Declined = declined;
            SurchargableWithdrawals = surchargableWithdrawals;
            OtherApproved = otherApproved;
            Reversed = reversed;
            SurchargeAmount = surchargeAmount;
            TotalTransaction = totalTransaction;
            Surcharge = surcharge;
            Dispensed = dispensed;
        }

        [GridColumn(Title = "Terminal Id")]
        public string TerminalId { get; set; }
        [GridColumn(Title = "Location Name")]
        public string LocationName { get; set; }
        [GridColumn(Title = "Date")]
        public string Date { get; set; }
        [GridColumn(Title = "Approved Withdrawals")]
        public int? ApprovedWithdrawals { get; set; }
        [GridColumn(Title = "Declined")]
        public int? Declined { get; set; }
        [GridColumn(Title = "Surchargable Withdrawals")]
        public int? SurchargableWithdrawals { get; set; }
        [GridColumn(Title = "Other Approved")]
        public int? OtherApproved { get; set; }
        [GridColumn(Title = "Reversed")]
        public int? Reversed { get; set; }
        [GridColumn(Title = "Surcharge Amount")]
        public double? SurchargeAmount { get; set; }
        [GridColumn(Title = "Total Transaction")]
        public int? TotalTransaction { get; set; }
        [GridColumn(Title = "Surcharge")]
        public double? Surcharge { get; set; }
        [GridColumn(Title = "Dispensed")]
        public double? Dispensed { get; set; }


    }
   
}