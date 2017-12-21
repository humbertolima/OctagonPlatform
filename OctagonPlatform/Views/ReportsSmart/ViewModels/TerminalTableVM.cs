using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
    
    public class TerminalTableVM
    {
        public TerminalTableVM()
        {
        }

        public TerminalTableVM(string terminalID, string locationName, string address, string city, string state, string postalCode, string contactName, string contactPhone, string aTMType, string connection, string eMVStatus, string dCCStatus, string surchargeAmount, string creationDate)
        {
            TerminalID = terminalID;
            LocationName = locationName;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ATMType = aTMType;
            Connection = connection;
            EMVStatus = eMVStatus;
            DCCStatus = dCCStatus;
            SurchargeAmount = surchargeAmount;
            CreationDate = creationDate;
        }

        [GridColumn(Title = "Terminal Id")]
        public string TerminalID { get; set; }
        [GridColumn(Title = "Location Name")]
        public string LocationName { get; set; }
        [GridColumn(Title = "Address")]
        public string Address { get; set; }
        [GridColumn(Title = "City")]
        public string City { get; set; }
        [GridColumn(Title = "State")]
        public string State { get; set; }
        [GridColumn(Title = "Postal Code")]
        public string PostalCode { get; set; }
        [GridColumn(Title = "Contact Name")]
        public string ContactName { get; set; }
        [GridColumn(Title = "Contact Phone")]
        public string ContactPhone { get; set; }
        [GridColumn(Title = "ATM Type")]
        public string ATMType { get; set; }
        [GridColumn(Title = "Connection")]
        public string Connection { get; set; }
        [GridColumn(Title = "EMV Status")]
        public string EMVStatus { get; set; }
        [GridColumn(Title = "DCC Status")]
        public string DCCStatus { get; set; }
        [GridColumn(Title = "Surcharge Amount")]
        public string SurchargeAmount { get; set; }
        [GridColumn(Title = "Creation Date")]
        public string CreationDate { get; set; }
    }
}