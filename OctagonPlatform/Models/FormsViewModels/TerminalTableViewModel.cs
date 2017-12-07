using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalTableViewModel
    {
        public string TerminalID { get; set; }

        public string LocationName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ATMType { get; set; }

        public string Connection { get; set; }

        public string EMVStatus { get; set; }

        public string DCCStatus { get; set; }

        public string SurchargeAmount { get; set; }

        public string CreationDate { get; set; }
    }
}