using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class CashLoadViewModel
    {
        [DisplayName("Terminal Id")]
        public string TerminalId { get; set; }

        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
        [DisplayName("From")]
        public string StartDate { get; set; }
        [DisplayName("To")]
        public string EndDate { get; set; }
        public StatusType.Status Status { get; set; }
        public CashLoadViewModel()
        {
            //TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Now.ToShortDateString();
            EndDate = DateTime.Now.ToShortDateString();
            PartnerId = -1;
            GroupId = -1;
        }

        public CashLoadViewModel(string terminalId, string partner, int partnerId, string group, int groupId, string startDate, string endDate, StatusType.Status status)
        {
            TerminalId = terminalId;
            Partner = partner;
            PartnerId = partnerId;
            Group = group;
            GroupId = groupId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}