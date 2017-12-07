using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalListViewModel
    {
        [DisplayName("Terminal Id")]
        public string TerminalId { get; set; }

        public string Partner { get; set; }
        [HiddenInput]
        public int PartnerId { get; set; }
        public string Account { get; set; }
        [HiddenInput]
        public int AccountId { get; set; }
        public string Group { get; set; }
        [HiddenInput]
        public int GroupId { get; set; }
        [DisplayName("Create From")]
        public string StartDate { get; set; }
        [DisplayName("Create To")]
        public string EndDate { get; set; }
        public StatusType.Status Status { get; set; }
        public CommunicationType.Communication ConectionType { get; set; }
        public string State { get; set; }
        [HiddenInput]
        public int StateId { get; set; }
        public string City { get; set; }
        [HiddenInput]
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public TerminalListViewModel()
        {
            //TerminalId = "TR024019";// "NH061617";
            StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
            PartnerId = -1;
            GroupId = -1;
            AccountId = -1;
            StateId = -1;
            CityId = -1;
        }
    }
}